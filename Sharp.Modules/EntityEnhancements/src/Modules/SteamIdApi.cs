/*
 * ModSharp
 * Copyright (C) 2023-2026 Kxnrl. All Rights Reserved.
 *
 * This file is part of ModSharp.
 * ModSharp is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 *
 * ModSharp is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with ModSharp. If not, see <https://www.gnu.org/licenses/>.
 */

using Microsoft.Extensions.Logging;
using Sharp.Shared;
using Sharp.Shared.GameEntities;
using Sharp.Shared.HookParams;
using Sharp.Shared.Listeners;
using Sharp.Shared.Managers;
using Sharp.Shared.Objects;
using Sharp.Shared.Units;

namespace Sharp.Modules.EntityEnhancements.Modules;

internal sealed class SteamIdApi : IEnhancement, IClientListener
{
    private readonly ILogger<SteamIdApi> _logger;
    private readonly IModSharp           _modSharp;
    private readonly IClientManager      _clientManager;
    private readonly IHookManager        _hookManager;
    private readonly IEntityManager      _entityManager;

    public SteamIdApi(ISharedSystem sharedSystem)
    {
        _logger        = sharedSystem.GetLoggerFactory().CreateLogger<SteamIdApi>();
        _modSharp      = sharedSystem.GetModSharp();
        _clientManager = sharedSystem.GetClientManager();
        _hookManager   = sharedSystem.GetHookManager();
        _entityManager = sharedSystem.GetEntityManager();
    }

    public void Init()
    {
        _hookManager.PlayerSpawnPost.InstallForward(OnPlayerSpawned);

        _clientManager.InstallClientListener(this);
    }

    public void Shutdown()
    {
        _hookManager.PlayerSpawnPost.RemoveForward(OnPlayerSpawned);

        _clientManager.RemoveClientListener(this);
    }

    int IClientListener.ListenerPriority => 0;
    int IClientListener.ListenerVersion  => IClientListener.ApiVersion;

    public void OnClientPostAdminCheck(IGameClient client)
    {
        if (_entityManager.FindPlayerControllerBySlot(client.Slot)?.GetPlayerPawn() is not { } pawn)
        {
            return;
        }

        var steamId = client.SteamId;

        _modSharp.InvokeFrameAction(() => SetPlayerAttribute(pawn, steamId));
    }

    private void OnPlayerSpawned(IPlayerSpawnForwardParams args)
    {
        var pawn    = args.Pawn;
        var steamId = args.Controller.SteamId;

        _modSharp.InvokeFrameAction(() => SetPlayerAttribute(pawn, steamId));
    }

    private void SetPlayerAttribute(IPlayerPawn pawn, SteamID steamId)
    {
        if (pawn.IsValid() && steamId.IsValidUserId())
        {
            pawn.AcceptInput("AddAttribute", null, null, steamId.ToString());
        }
    }
}

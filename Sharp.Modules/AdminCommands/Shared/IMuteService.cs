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

using Sharp.Shared.Objects;
using Sharp.Shared.Units;

namespace Sharp.Modules.AdminCommands.Shared;

/// <summary>
///     Mute/unmute operations. Provided for callers; not intended for external implementations.
///     Side effects: updates cache, may notify players. Duplicate checks are the caller's responsibility (command handlers
///     already do this).
/// </summary>
public interface IMuteService
{
    /// <summary>
    ///     Mute an online target (updates cache/storage, may notify).
    /// </summary>
    void Mute(IGameClient? admin, IGameClient target, TimeSpan? duration, string reason);

    /// <summary>
    ///     Mute by SteamID (offline path; updates cache/storage when player joins).
    /// </summary>
    void Mute(IGameClient? admin, SteamID steamId, TimeSpan? duration, string reason);

    /// <summary>
    ///     Unmute an online target (updates cache/storage).
    /// </summary>
    void Unmute(IGameClient? admin, IGameClient target, string reason);
}

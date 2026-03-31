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
///     Gag/ungag operations. Provided for callers; not intended for external implementations.
///     Side effects: updates cache, may notify players. Duplicate checks are the caller's responsibility (command handlers
///     already do this).
/// </summary>
public interface IGagService
{
    /// <summary>
    ///     Gag an online target (updates cache/storage, may notify).
    /// </summary>
    void Gag(IGameClient? admin, IGameClient target, TimeSpan? duration, string reason);

    /// <summary>
    ///     Gag by SteamID (offline path; updates cache/storage when player joins).
    /// </summary>
    void Gag(IGameClient? admin, SteamID steamId, TimeSpan? duration, string reason);

    /// <summary>
    ///     Remove gag on an online target (updates cache/storage).
    /// </summary>
    void Ungag(IGameClient? admin, IGameClient target, string reason);
}

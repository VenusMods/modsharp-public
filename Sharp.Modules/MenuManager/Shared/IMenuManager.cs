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

namespace Sharp.Modules.MenuManager.Shared;

public interface IMenuManager
{
    const string Identity = nameof(IMenuManager);

    void DisplayMenu(IGameClient client, Menu menu);

    void DisplayMenu(IGameClient client, Menu menu, out ulong sessionId);

    void QuitMenu(IGameClient client);

    /// <summary>
    /// Returns whether the client currently has an active menu session.
    /// </summary>
    bool IsInMenu(IGameClient client);

    /// <summary>
    /// Returns whether the client is currently in the specified menu instance,
    /// including parent menus in the current menu chain.
    /// </summary>
    bool IsInMenu(IGameClient client, Menu menuInstance);

    /// <summary>
    ///     Returns whether the client is currently in the specified menu session.
    /// </summary>
    bool IsInMenu(IGameClient client, ulong sessionId);

    /// <summary>
    /// Returns whether the current top menu is the specified menu instance.
    /// </summary>
    bool IsInCurrentMenu(IGameClient client, Menu menuInstance);

    /// <summary>
    /// Returns the current menu session identifier when the client is in a menu.
    /// </summary>
    bool TryGetCurrentMenuSessionId(IGameClient client, out ulong sessionId);
}

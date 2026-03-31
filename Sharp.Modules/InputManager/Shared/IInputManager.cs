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

using System;
using Sharp.Shared.Objects;

namespace Sharp.Modules.InputManager.Shared;

public interface IInputManager
{
    const string Identity = nameof(IInputManager);

    /// <summary>
    ///     Get or create an input listener registry for a module
    /// </summary>
    /// <param name="moduleIdentity">The module identity</param>
    /// <returns>The input listener registry for the module</returns>
    public IInputListenerRegistry GetInputListenerRegistry(string moduleIdentity);
}

public interface IInputListenerRegistry
{
    /// <summary>
    ///     Add a single key input listener
    /// </summary>
    /// <param name="key">The input key to listen for</param>
    /// <param name="action">Callback function to invoke</param>
    /// <param name="state">The key state to listen for</param>
    /// <param name="holdDuration">
    ///     Hold duration in seconds, only valid for KeyHold state. This means that how long the key
    ///     must be held down before the action is triggered.
    /// </param>
    void AddInputListener(InputKey key,
        Action<IGameClient>        action,
        InputState                 state        = InputState.KeyDown,
        float                      holdDuration = 0f);

    /// <summary>
    ///     Add a combination key listener (all keys must be pressed simultaneously)
    /// </summary>
    /// <param name="keys">Array of keys for the combination</param>
    /// <param name="action">Callback function to invoke</param>
    /// <param name="state">The key state to listen for, defaults to KeyDown</param>
    void AddCombinationListener(InputKey[] keys, Action<IGameClient> action, InputState state = InputState.KeyDown);
}

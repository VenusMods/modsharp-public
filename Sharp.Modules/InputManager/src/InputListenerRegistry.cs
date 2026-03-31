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
using System.Collections.Generic;
using Sharp.Modules.InputManager.Shared;
using Sharp.Shared.Objects;

namespace Sharp.Modules.InputManager;

public class InputListenerRegistry : IInputListenerRegistry
{
    private readonly InputManager                                                            _manager;
    private readonly List<(InputKey Key, Action<IGameClient> Callback, InputState State)>    _inputListeners;
    private readonly List<(InputKey[] Keys, Action<IGameClient> Callback, InputState State)> _combinationListeners;

    internal InputListenerRegistry(InputManager manager)
    {
        _manager = manager;

        _inputListeners       = [];
        _combinationListeners = [];
    }

    public void AddInputListener(InputKey key,
        Action<IGameClient>               action,
        InputState                        state        = InputState.KeyDown,
        float                             holdDuration = 0)
    {
        _manager.AddInputListener(key, action, state, holdDuration);
        _inputListeners.Add((key, action, state));
    }

    public void AddCombinationListener(InputKey[] keys, Action<IGameClient> action, InputState state = InputState.KeyDown)
    {
        _manager.AddCombinationListener(keys, action, state);
        _combinationListeners.Add((keys, action, state));
    }

    internal void Cleanup()
    {
        foreach (var (key, callback, state) in _inputListeners)
        {
            _manager.RemoveInputListener(key, callback, state);
        }

        foreach (var (keys, callback, state) in _combinationListeners)
        {
            _manager.RemoveCombinationListener(keys, callback, state);
        }

        _inputListeners.Clear();
        _combinationListeners.Clear();
    }
}

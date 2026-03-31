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
using Sharp.Modules.LocalizerManager.Shared;
using Sharp.Shared.Objects;

namespace Sharp.Modules.LocalizerManager;

internal sealed class MultiLocale : IMultiLocale
{
    private readonly IReadOnlyList<IGameClient> _clients;
    private readonly ILocalizerManager          _localizerManager;
    private readonly string?                    _defaultPrefix;

    public MultiLocale(IReadOnlyList<IGameClient> clients, ILocalizerManager localizerManager, string? defaultPrefix)
    {
        _clients          = clients;
        _localizerManager = localizerManager;
        _defaultPrefix    = defaultPrefix;
    }

    public ILocalizedMessageMany Message()
        => new MultiLocalizedMessageBuilder(_clients, _localizerManager, _defaultPrefix);

    public ILocalizedMessageMany Localized(string key, params ReadOnlySpan<object?> args)
        => Message().Text(key, args);

    public ILocalizedMessageMany Literal(string text)
        => Message().Literal(text);
}

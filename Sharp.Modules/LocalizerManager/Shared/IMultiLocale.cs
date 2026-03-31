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

namespace Sharp.Modules.LocalizerManager.Shared;

/// <summary>
/// Locale view for a captured set of clients; intended only for multi-send builders.
/// </summary>
public interface IMultiLocale
{
    /// <summary>
    ///     Create a fluent message builder for the captured clients.
    /// </summary>
    ILocalizedMessageMany Message();

    /// <summary>
    ///     Convenience for Message().Text(key, args).
    /// </summary>
    ILocalizedMessageMany Localized(string key, params ReadOnlySpan<object?> args);

    /// <summary>
    ///     Convenience for Message().Literal(text).
    /// </summary>
    ILocalizedMessageMany Literal(string text);
}

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
using Sharp.Shared.Enums;

namespace Sharp.Modules.LocalizerManager.Shared;

/// <summary>
/// Fluent localized message builder (new ergonomic API).
/// </summary>
public interface ILocalizedMessage
{
    /// <summary>
    ///     Sets the prefix.
    ///     Pass <c>null</c> to disable the prefix entirely.
    /// </summary>
    ILocalizedMessage Prefix(string? prefix);

    /// <summary>
    ///     Append literal text.
    /// </summary>
    ILocalizedMessage Literal(string text);

    /// <summary>
    ///     Append localized text.
    /// </summary>
    ILocalizedMessage Text(string key, params ReadOnlySpan<object?> args);

    /// <summary>
    ///     Append localized text or fallback if missing/format fails.
    /// </summary>
    ILocalizedMessage TextOrFallback(string key, string fallback, params ReadOnlySpan<object?> args);

    /// <summary>
    ///     Append a raw value.
    /// </summary>
    ILocalizedMessage Value(object? value);

    /// <summary>
    ///     Register a post-processor for the rendered string (applied before Build/Print return/send).
    ///     Multiple calls compose in order of invocation.
    /// </summary>
    ILocalizedMessage Transform(Func<string, string> processor);

    /// <summary>
    ///     Build the final string (applies prefix/colors and any processors).
    /// </summary>
    string Build();

    /// <summary>
    ///     Print to the bound client (if any) on the specified channel.
    ///     Do not cache builders across unload/reload.
    /// </summary>
    void Print(HudPrintChannel channel = HudPrintChannel.Chat);
}

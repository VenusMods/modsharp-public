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

using System.Globalization;
using Sharp.Modules.LocalizerManager.Shared;

namespace Sharp.Modules.AdminCommands.Common;

internal readonly struct LocalizedDuration : IFormattable
{
    private readonly TimeSpan?          _duration;
    private readonly ILocalizerManager? _localizer;

    public LocalizedDuration(TimeSpan? duration, ILocalizerManager? localizer)
    {
        _duration  = duration;
        _localizer = localizer;
    }

    public override string ToString()
        => ToString(null, CultureInfo.CurrentCulture);

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        var culture = formatProvider as CultureInfo ?? CultureInfo.CurrentCulture;

        if (!_duration.HasValue)
        {
            return _localizer?.Format(culture, "Admin.Duration.Permanent")
                   ?? "permanently";
        }

        var duration = _duration.Value;

        if (duration.TotalDays >= 1)
        {
            var days = (int) duration.TotalDays;

            return _localizer?.Format(culture, "Admin.Duration.Days", days)
                   ?? $"for {days} day(s)";
        }

        if (duration.TotalHours >= 1)
        {
            var hours = (int) duration.TotalHours;

            return _localizer?.Format(culture, "Admin.Duration.Hours", hours)
                   ?? $"for {hours} hour(s)";
        }

        var minutes = Math.Max((int) duration.TotalMinutes, 1);

        return _localizer?.Format(culture, "Admin.Duration.Minutes", minutes)
               ?? $"for {minutes} minute(s)";
    }
}

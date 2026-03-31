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
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Sharp.Shared.Helpers;

public sealed class ConfigurationWatcher
{
    private readonly IConfiguration    _configuration;
    private readonly Action            _onReload;
    private readonly CancellationToken _cancellationToken;
        
    public ConfigurationWatcher(IConfiguration configuration, Action onReload, CancellationToken cancellationToken)
    {
        _configuration     = configuration;
        _onReload          = onReload;
        _cancellationToken = cancellationToken;

        configuration.GetReloadToken().RegisterChangeCallback(OnChanged, null);
    }

    private void OnChanged(object? obj)
    {
        try
        {
            _onReload.Invoke();
        }
        finally
        {
            Task.Run(SubscribeConfigReload, _cancellationToken);
        }
    }

    private void SubscribeConfigReload()
        => Task.Run(async () =>
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(500), _cancellationToken);
                        _configuration.GetReloadToken().RegisterChangeCallback(OnChanged, null);
                    },
                    _cancellationToken);
}
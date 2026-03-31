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

using Microsoft.Extensions.DependencyInjection;
using Sharp.Modules.AdminCommands.Shared;

namespace Sharp.Modules.AdminCommands.Extensions;

internal static class ServiceCollectionExtension
{
    public static void AddCommandService<TImpl, TInterface>(this IServiceCollection services)
        where TImpl : class, TInterface, ICommandCategory
        where TInterface : class
    {
        services.AddSingleton<TImpl>();
        services.AddSingleton<TInterface>(sp => sp.GetRequiredService<TImpl>());
        services.AddSingleton<ICommandCategory>(sp => sp.GetRequiredService<TImpl>());
    }

    public static void AddOperationHandler<TH>(this IServiceCollection services) where TH : class, IAdminOperationHandler
    {
        services.AddSingleton<TH>();
        services.AddSingleton<IAdminOperationHandler>(sp => sp.GetRequiredService<TH>());
    }
}

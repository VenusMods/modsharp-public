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

using Sharp.Shared.Enums;
using Sharp.Shared.GameEntities;
using Sharp.Shared.Types;

namespace Sharp.Extensions.EntityHookManager;

public interface IEntityHookManager
{
    delegate void EventDelegate(string classname, IBaseEntity entity);

    delegate void WeaponEventDelegate(string classname, IBaseWeapon weapon);

    delegate void OutputDelegate(string classname,
        string                          output,
        IBaseEntity                     entity,
        IBaseEntity?                    activator,
        float                           delay,
        ref EHookAction                 result);

    delegate void InputDelegate(string classname,
        string                         input,
        in EntityVariant               value,
        IBaseEntity                    entity,
        IBaseEntity?                   activator,
        IBaseEntity?                   caller,
        ref EHookAction                result);

    /// <summary>
    ///     监听实体创建事件
    /// </summary>
    void ListenEntityCreate(string classname, EventDelegate callback);

    /// <summary>
    ///     监听实体删除事件
    /// </summary>
    void ListenEntityDelete(string classname, EventDelegate callback);

    /// <summary>
    ///     监听实体生成事件
    /// </summary>
    void ListenEntitySpawn(string classname, EventDelegate callback);

    /// <summary>
    ///     监听武器生成事件
    /// </summary>
    void ListenWeaponSpawn(WeaponEventDelegate callback);

    /// <summary>
    ///     Hook 实体IO中的 Input
    /// </summary>
    void HookEntityInput(string classname, string input, InputDelegate callback);

    /// <summary>
    ///     Hook 实体IO中的 Output
    /// </summary>
    void HookEntityOutput(string classname, string output, OutputDelegate callback);
}

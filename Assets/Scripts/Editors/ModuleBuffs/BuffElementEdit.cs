using InGame.Boards.Modules.ModuleBuffs;
using UnityEngine;

namespace Editors.ModuleBuffs
{
    public abstract class BuffElementEdit: MonoBehaviour
    {
        public abstract ModuleBuff CreateBuff();

    }
}
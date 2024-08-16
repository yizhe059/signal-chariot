using System.Collections.Generic;
using InGame.Boards.Modules;
using InGame.Boards.Modules.ModuleBuffs;
using InGame.Effects;
using UnityEngine;

namespace InGame.Boards
{
    public enum SlotStatus
    {
        Hidden,
        Empty,
        Occupied,
        Selectable,
        Extra
    }

    [System.Serializable]
    public class Slot
    {
        public SlotStatus status { get; set; } = SlotStatus.Hidden;

        private BoardPosition m_position = new()
        {
            x = 0,
            y = 0
        };

        private Dictionary<ModuleBuffType, ModuleBuff> m_buffs = new();

        public BoardPosition pos => new BoardPosition(m_position);

        public ModuleSlot moduleSlot { get; set; }

        public Slot()
        {
        }

        public Slot(Slot other)
        {
            this.status = other.status;
        }

        public void SetPosition(int x, int y)
        {
            m_position = new BoardPosition
            {
                x = x,
                y = y
            };
        }

        public void TriggerEffect(EffectBlackBoard blackBoard)
        {
            moduleSlot?.TriggerEffect(blackBoard);
        }

        #region Buff

        public void AddBuff(ModuleBuff buff)
        {
            if (!m_buffs.ContainsKey(buff.type))
            {
                m_buffs.Add(buff.type, ModuleBuff.CreateEmptyBuff(buff.type));
            }

            m_buffs[buff.type].Add(buff);
            if (moduleSlot != null)
            {
                moduleSlot.AddBuff(buff);
            }
        }

        public void RemoveBuff(ModuleBuff buff)
        {
            if (!m_buffs.ContainsKey(buff.type))
            {
                Debug.LogError("This should not happen");
                return;
            }

            m_buffs[buff.type].Minus(buff);
            if (moduleSlot != null)
            {
                moduleSlot.RemoveBuff(buff);
            }
        }

        public void TellModuleYourBuff()
        {
            if (moduleSlot == null)
            {
                Debug.LogError("This should not happen");
                return;
            }

            foreach (var buffPair in m_buffs)
            {
                moduleSlot.AddBuff(buffPair.Value);
            }
        }

        #endregion
        
        public static Slot GenerateSlot(int x, int y, SlotStatus status)
        {
            return new Slot
            {
                m_position = new BoardPosition{x = x, y=y},
                status = status
            };
        }

        public static Slot GenerateSlot(int x, int y)
        {
            return new Slot
            {
                m_position = new BoardPosition{x = x, y=y},
                status = SlotStatus.Hidden
            };
        }
    }
}
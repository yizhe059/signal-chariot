using System.Collections.Generic;
using InGame.Boards;
using InGame.Boards.Modules;
using InGame.Boards.Modules.ModuleBuffs;
using InGame.Cores;
using UnityEngine;

namespace InGame.Effects.EffectElement.ApplyModuleBuffEffects
{
    public enum BuffRange {
        Pointing,
        Surrounding
    }
    public abstract class ApplyModuleBuff: Effect
    {
        public BuffRange range;
        public abstract ModuleBuff GetBuff();

        private List<BoardPosition> GetBuffRanges()
        {
            var list = new List<BoardPosition>();
            if (range == BuffRange.Pointing)
            {
                var modulePos = m_module.GetPivotBoardPosition();
                var orientation = m_module.orientation;
                modulePos += Module.OrientationToBoardPositionOffset(orientation);
                list.Add(modulePos);
            }else if (range == BuffRange.Surrounding)
            {
                var modulePosList = m_module.GetBoardPositionList();
                foreach (var pos in modulePosList)
                {
                    for (int deltaX = -1; deltaX <= 1; deltaX++)
                    {
                        for (int deltaY = -1; deltaY <= 1; deltaY++)
                        {
                            if(Mathf.Abs(deltaX + deltaY) != 1) continue;

                            var newPos = pos + new BoardPosition(deltaX, deltaY);

                            if (modulePosList.Contains(newPos)) continue;
                            if (list.Contains(newPos)) continue;
                            list.Add(newPos);
                        }
                    }
                }
            }

            return list;
        }
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            var buff = GetBuff();
            if (buff == null) return;
            var range = GetBuffRanges();

            foreach (var pos in range)
            {
                GameManager.Instance.GetBoard().AddBuffToSlot(buff, pos.x, pos.y);
            }
        }

        public override void OnUnTrigger(EffectBlackBoard blackBoard)
        {
            var buff = GetBuff();
            if (buff == null) return;
            var range = GetBuffRanges();

            foreach (var pos in range)
            {
                GameManager.Instance.GetBoard().RemoveBuffFromSlot(buff, pos.x, pos.y);
            }
        }

        public override Effect CreateCopy()
        {
            var buffEffect = OnCopy();
            buffEffect.range = range;
            return buffEffect;
        }

        public abstract ApplyModuleBuff OnCopy();

    }
}
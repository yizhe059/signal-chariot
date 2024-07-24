using System;
using System.Collections.Generic;
using UnityEngine;
using World.SetUps;
using World.Views;
using Object = UnityEngine.Object;

namespace World.Modules
{

    [System.Serializable]
    public struct ModulePosition
    {
        public int x;
        public int y;

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
    
    
    public class ModuleSlot
    {
        private Module m_module;
        private ModulePosition m_position;

        public ModulePosition position => m_position;
        public Module module => m_module;

        public BoardPosition GetBoardPosition(BoardPosition pivotPos)
        {
            if (m_module == null) return default(BoardPosition);

            return m_module.GetBoardPosition(this, pivotPos);
        }
        public static ModuleSlot CreateModuleSlot(Module module, ModulePosition position)
        {
            var slot = new ModuleSlot
            {
                m_module = module,
                m_position = position
            };
            return slot;
        }
    }

    public class Module
    {
        public enum Orientation
        {
            Up,
            Right,
            Down,
            Left
        }


        
        private Orientation m_orientation = Orientation.Up;
        private ModuleView m_prefab;
        private ModuleView m_moduleView;
        
        private ModulePosition m_pivot;
        private bool[,] m_slotMap =null; 
        private ModuleSlot[,] m_moduleSlots=null;
        private int m_width, m_height;
        public string name { get; protected set; }
        public ModuleView moduleView => m_moduleView;
        public Orientation orientation => m_orientation;

        private void CreateSlotMap(List<ModulePosition> positions)
        {
            ModulePosition min = new ModulePosition
            {
                x = 0,
                y = 0
            };
            ModulePosition max = new ModulePosition
            {
                x = 0,
                y = 0
            };
            foreach (var pos in positions)
            {
                if (pos.x > max.x) max.x = pos.x;
                if (pos.y > max.y) max.y = pos.y;
                if (pos.x < min.x) min.x = pos.x;
                if (pos.y < min.y) min.y = pos.y;
            }

            m_width = max.x - min.x + 1;
            m_height = max.y - min.y + 1;
            
            // the default value of bool is false, so every element is false right now
            m_slotMap = new bool[m_width, m_height];
            
                
            foreach (var pos in positions)
            {
                if (pos.x > max.x) max.x = pos.x;
                if (pos.y > max.y) max.y = pos.y;
                if (pos.x < min.x) min.x = pos.x;
                if (pos.y < min.y) min.y = pos.y;
            }

            m_pivot = new ModulePosition{x = 0 - min.x, y = 0 - min.y};
            m_slotMap[m_pivot.x, m_pivot.y] = true;

            foreach (var pos in positions)
            {
                m_slotMap[pos.x - min.x, pos.y - min.y] = true;
            }
        }
        
        #region Offset
        private ModulePosition GetOffset(int x, int y)
        {
            return GetRotatedPos(new ModulePosition
            {
                x = x - m_pivot.x,
                y = y - m_pivot.y
            }, m_orientation);
        }
        
        private ModulePosition GetOffset(ModulePosition pos)
        {
            return GetOffset(pos.x, pos.y);
        }
        
        public ModulePosition GetOffset(ModuleSlot slot)
        {
            return GetOffset(slot.position.x, slot.position.y);
        }
        #endregion
        
        public List<ModuleSlot> GetModuleSlots()
        {
            List<ModuleSlot> slots = new List<ModuleSlot>();
            if (m_moduleSlots == null)
            {
                m_moduleSlots = new ModuleSlot[m_width, m_height];
                for (int x = 0; x < m_width; x++)
                {
                    for (int y = 0; y < m_height; y++)
                    {
                        if (m_slotMap[x, y])
                        {
                            var offset = new ModulePosition
                            {
                                x = x,
                                y = y
                            };
                            m_moduleSlots[x, y] =
                                ModuleSlot.CreateModuleSlot(this, offset);
                        }
                        else
                        {
                            m_moduleSlots[x, y] = null;
                        }
                    }
                }
            }
            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_height; y++)
                {
                    if (m_slotMap[x, y])
                    {
                        slots.Add(m_moduleSlots[x,y]);
                    }
                }
            }

            
            return slots;
        }

        #region BoardPosition
        public BoardPosition GetBoardPosition(ModuleSlot slot, BoardPosition pivotPos)
        {
            var offset = GetOffset(slot.position.x, slot.position.y);
            return new BoardPosition
            {
                x = pivotPos.x + offset.x,
                y = pivotPos.y + offset.y
            };
        }

        public BoardPosition GetPivotBoardPosition(ModuleSlot slot, BoardPosition slotBoardPosition)
        {
            var offset = GetOffset(slot);
            return new BoardPosition
            {
                x = slotBoardPosition.x - offset.x,
                y = slotBoardPosition.y - offset.y
            };
        }
        public List<BoardPosition> GetBoardPositionList(BoardPosition pivotPos)
        {
            var list = new List<BoardPosition>();
            list.Add(pivotPos);

            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_height; y++)
                {
                    if (m_slotMap[x, y])
                    {
                        var offset = GetOffset(x,y);
                        list.Add(new BoardPosition
                        {
                            x = pivotPos.x + offset.x,
                            y = pivotPos.y + offset.y
                        });
                    }
                }
            }

            return list;
        }
        #endregion
        
        public void Rotate()
        {
            m_orientation = RotateClockwise(m_orientation);
        }
        
        public float GetRotationDegree()
        {
            return GetRotationDegree(m_orientation);
        }
        
        #region static method
        public static Orientation RotateClockwise(Orientation current)
        {
            return current switch
            {
                Orientation.Up => Orientation.Right,
                Orientation.Right => Orientation.Down,
                Orientation.Down => Orientation.Left,
                Orientation.Left => Orientation.Up,
                _ => Orientation.Up
            };
        }
        
        public static Orientation RotateCounterClockwise(Orientation current)
        {
            return current switch
            {
                Orientation.Up => Orientation.Left,
                Orientation.Right => Orientation.Up,
                Orientation.Down => Orientation.Right,
                Orientation.Left => Orientation.Down,
                _ => Orientation.Up
            };
        }
        
        public static ModulePosition GetRotatedPos(ModulePosition pos, Orientation orientation)
        {
            return orientation switch
            {
                Orientation.Up => new ModulePosition{x=pos.x, y=pos.y},
                Orientation.Down => new ModulePosition{x=-pos.x, y=-pos.y},
                Orientation.Right => new ModulePosition{x = pos.y, y= -pos.x },
                Orientation.Left => new ModulePosition{x=-pos.y, y = pos.x},
                _ => default(ModulePosition)
            };
        }
        public static float GetRotationDegree(Orientation orientation)
        {
            return orientation switch
            {
                Orientation.Up => 0,
                Orientation.Right => -90,
                Orientation.Down => 180,
                Orientation.Left => -270,
                _=>0
            };
        }

        public static Module CreateModule(ModuleSetUp setUp)
        {
            var newModule = new Module
            {
                name = setUp.name,
                m_prefab = setUp.prefab
            };
            
            newModule.CreateSlotMap(setUp.otherPositions);

            return newModule;
        }

        public static Module CreateModule(Module other)
        {
            var newModule = new Module
            {
                m_height = other.m_height,
                m_width = other.m_width,
                m_orientation = Orientation.Up,
                m_pivot = other.m_pivot,
                m_prefab = other.m_prefab,
                m_slotMap = other.m_slotMap,
                name = other.name
            };

            return newModule;
        }

        public ModuleView CreateModuleView(Transform parent)
        {
            if (m_moduleView != null) return m_moduleView;
            if (m_prefab == null) return null;

            m_moduleView = ModuleView.CreateModuleView(m_prefab, parent, this,
                Quaternion.Euler(0, GetRotationDegree(m_orientation), 0));
            return m_moduleView;
        }
        
        #endregion
        public override string ToString()
        {
            string result = "";
            result = $"{name}: ";
            result += $"slot Map size: {m_width}, {m_height}, ";
            result += $"pivot: {m_pivot}, ";
            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_height; y++)
                {
                    result += $"({x}, {y}): {m_slotMap[x, y]}, ";
                }
            }
            
            
            return result;
        }
    }
}
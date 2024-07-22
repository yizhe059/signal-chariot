using System;
using System.Collections.Generic;
using UnityEngine;
using World.SetUps;

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
        private Orientation m_orientation = Orientation.Up;
        
        private ModulePosition m_pivot;
        private bool[,] m_slotMap =null;
        private int m_width, m_height;
        public string name { get; protected set; }

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

        public static ModulePosition GetRotatedPos(ModulePosition pos, Orientation orientation)
        {
            return orientation switch
            {
                Orientation.Up => pos,
                Orientation.Down => new ModulePosition{x=-pos.x, y=-pos.y},
                Orientation.Right => new ModulePosition{x = pos.y, y= -pos.x },
                Orientation.Left => new ModulePosition{x=-pos.y, y = pos.x}
            };
        }
        
        public static Module CreateModule(ModuleSetUp setUp)
        {
            var newModule = new Module
            {
                name = setUp.name
            };
            
            newModule.CreateSlotMap(setUp.otherPositions);


            return newModule;
        }

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
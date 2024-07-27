using System;
using UnityEngine;
using World.Views;

namespace World.Signals
{
    public class Signal
    {
        public enum Direction
        {
            Up, 
            Down,
            Right,
            Left
        }

        private int m_id;
        public int id => m_id;
        
        private Direction m_dir;
        public Direction dir => m_dir;

        public void SetDirection(Direction newDir)
        {
            m_dir = newDir;
            view?.UpdateDir(newDir);
        }
        
        private int m_energy;
        public int energy => m_energy;

        private BoardPosition m_pos;
        public BoardPosition pos => new BoardPosition(m_pos);

        public void SetPos(BoardPosition newPos)
        {
            m_pos = newPos;
            view?.SetPos(newPos);
        }

        private SignalView m_view;

        public SignalView view => m_view;
        public void SetView(SignalView newView) => m_view = newView;

        public void Move(int deltaX, int deltaY)
        {
            m_pos.x += deltaX;
            m_pos.y += deltaY;
        }

        public void ConsumeEnergy(int amount)
        {
            m_energy -= amount;
        }

        public void Start()
        {
            m_view?.StartMoving();
        }

        public void Stop()
        {
            m_view?.StopMoving();
        }

        public static Signal CreateSignal(SignalSetUp setUp)
        {
            return new Signal
            {
                m_id = setUp.id,
                m_energy = setUp.energy,
                m_pos = new BoardPosition(setUp.pos),
                m_dir = setUp.dir
            };
        }

        public void SelfDestroy()
        {
            m_view?.SelfDestroy();
            m_view = null;
            
            
            
        }

        public static BoardPosition GetDirVector(Direction dir)
        {
            return dir switch
            {
                Direction.Up => new BoardPosition(0, 1),
                Direction.Left => new BoardPosition(-1, 0),
                Direction.Down => new BoardPosition(0, -1),
                Direction.Right => new BoardPosition(1, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
            };
        }
        public static Vector3 GetDirWorldVector(Direction dir)
        {
            return dir switch
            {
                Direction.Up => new Vector3(0, 1),
                Direction.Left => new Vector3(-1, 0),
                Direction.Down => new Vector3(0, -1),
                Direction.Right => new Vector3(1, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
            };
        }
    }
}
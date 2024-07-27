using System;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using World.Signals;

namespace World.Views
{
    public class SignalView : MonoBehaviour
    {
        private BoardView m_boardView;
        private Signal m_signal;
        private bool m_isMoving = false;
        private Vector3 m_direciton;

        public void SelfDestroy()
        {
            m_signal.SetView(null);
            Destroy(this.gameObject);
        }

        public void SetPos(BoardPosition pos)
        {
            var worldPos = m_boardView.GetSlotCenterWorldPosition(pos);
            worldPos.z = Constants.SIGNAL_DEPTH;
            transform.localPosition = worldPos;
            
        }

        public void StartMoving()
        {
            m_isMoving = true;
        }

        public void StopMoving()
        {
            m_isMoving = false;
        }

        public void UpdateDir(Signal.Direction dir)
        {
            m_direciton = Signal.GetDirWorldVector(dir);
        }

        public static SignalView CreateSignalView(SignalView prefab, Transform parent, BoardView boardView, Signal signal)
        {
            var view = Instantiate(prefab, parent);
            view.m_boardView = boardView;
            view.m_signal = signal;

            view.SetPos(signal.pos);
            view.UpdateDir(signal.dir);
            
            signal.SetView(view);
            
            return view;
        }

        public void Update()
        {
            if (!m_isMoving) return;

            float cellSize = m_boardView.cellSize;
            var speed = cellSize / Constants.SIGNAL_MOVING_DURATION;

            transform.localPosition += m_direciton * (speed * UnityEngine.Time.deltaTime);
        }
    }
}
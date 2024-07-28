using System;
using System.Collections.Generic;

using UnityEngine;

using InGame.BattleFields.Chariots;

namespace InGame.Views
{
    public class ChariotView : MonoBehaviour
    {
        private Vector3 m_moveDirection = new Vector3(0,0,0);
        private Chariot m_chariot;

        public void Init(Chariot chariot)
        {
            m_chariot = chariot;
        }

        private void Update()
        {
            MoveChariot();
        }

        private void MoveChariot()
        {
            if (m_moveDirection == Vector3.zero) return;
            this.transform.Translate(m_moveDirection, Space.World);
        }

        public void SetMoveDirection(Vector2 inputDirection)
        {
            float x = inputDirection.x;
            float y = inputDirection.y;
            m_moveDirection = new Vector3(
                x * Mathf.Sqrt(1 - y * y * 0.5f), 
                y * Mathf.Sqrt(1 - x * x * 0.5f), 
                0
            ) * Time.deltaTime * m_chariot.GetSpeed();
        }
    }
}
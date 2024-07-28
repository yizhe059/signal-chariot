using System;
using System.Collections.Generic;

using UnityEngine;

namespace InGame.Views
{
    public class ChariotView : MonoBehaviour
    {
        private Vector3 m_moveDirection = new Vector3(0,0,0);

        public Vector3 moveDirection { 
            get { return m_moveDirection;} 
            set { m_moveDirection = value;}
        }

        private void Update(){
            MoveChariot();
        }

        private void MoveChariot(){
            if (m_moveDirection == Vector3.zero) return;
            this.transform.Translate(moveDirection, Space.World);
        }
    }
}
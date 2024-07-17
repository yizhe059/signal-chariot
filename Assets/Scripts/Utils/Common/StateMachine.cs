using UnityEngine;

namespace Utils.Common
{
    public interface IState<T> where T : class, IState<T>
    {
        public void Enter(T last);

        public void Exit();
    }

    public class StateMachine<T> where T : class, IState<T>
    {
        private T m_current;
        private T m_next;
        private bool m_nextChanged = false;
        private bool m_isLocked;
    
        public T current => m_current;

        public T next
        {
            get => m_next;
            set
            {
                if (m_next != value)
                {
                    m_next = value;
                    m_nextChanged = true;
                    if (!isLocked)
                    {
                        CheckNext();
                    }
                }
            }
        }

        public bool isLocked
        {
            get => m_isLocked;
            set
            {
                if (m_isLocked != value)
                {
                    m_isLocked = value;
                    if (!m_isLocked)
                    {
                        CheckNext();
                    }
                }
            }
        }

        private void CheckNext()
        {
            while (m_nextChanged)
            {
                m_nextChanged = false;
                var old = m_current;
                m_current = m_next;
                old?.Exit();
                m_current?.Enter(old);
            }
        }
    }

}

using UnityEngine;
using UnityEngine.Events;

namespace World.Interacts
{
    public class Clickable: MonoBehaviour
    {
        public UnityEvent onClickEvent = new UnityEvent();


        public void OnClick()
        {
            onClickEvent.Invoke();
        }
    }
}
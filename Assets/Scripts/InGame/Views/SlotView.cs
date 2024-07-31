using InGame.Boards;
using UnityEngine;
using Utils;

namespace InGame.Views
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_renderer;
        private Sprite m_selectableSprite;
        private Sprite m_emptySprite;
        
        private BoardPosition m_position;
        private BoardView m_boardView;
        

        public void OnStatusChanged(SlotStatus status)
        {
            if (status is SlotStatus.Empty or SlotStatus.Occupied)
            {
                gameObject.SetActive(true);
                m_renderer.sprite = m_emptySprite;
            }
            else if (status == SlotStatus.Hidden)
            {
                gameObject.SetActive(false);
            }else if (status == SlotStatus.Selectable)
            {
                gameObject.SetActive(true);
                m_renderer.sprite = m_selectableSprite;
            }
        }
        
        public void Init(BoardView boardView, BoardPosition position, Vector3 worldPosition, SlotStatus status)
        {
            m_boardView = boardView;
            m_position = position;
            transform.localPosition = worldPosition;

            m_selectableSprite = Resources.Load<Sprite>(Constants.SPRITE_SELECTABLE_SLOT_PATH);
            m_emptySprite = Resources.Load<Sprite>(Constants.SPRITE_EMPTY_SLOT_PATH);

            OnStatusChanged(status);
        }

        public void OnSlotClicked()
        {
            Debug.Log($"Slot {m_position} is clicked");
        }
    }
}
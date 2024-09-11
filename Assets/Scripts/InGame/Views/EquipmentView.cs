using System.Collections;

using UnityEngine;

using Utils;
using InGame.BattleFields.Androids;
using InGame.Boards.Modules.ModuleBuffs;

using DG.Tweening;
using InGame.BattleFields.Bullets;

namespace InGame.Views
{
    public class EquipmentView : MonoBehaviour
    {
        private Equipment m_equipment;
        private Vector3 m_target;

        #region Life Cycle
        public void Init(Equipment equipment)
        {
            m_equipment = equipment;
            SetSprite();
        }

        public void SetSprite()
        {
            Transform model = transform.Find(Constants.MODEL);
            if(model == null)
            {
                Debug.LogError("No model under this game object!");
                return;
            }

            SpriteRenderer spriteRenderer = model.GetComponent<SpriteRenderer>();
            if(spriteRenderer == null)
            {
                Debug.LogError("No sprite renderer under model!");
                return;
            }

            spriteRenderer.sprite = m_equipment.sprite;
        }

        private void Update()
        {
            Aim();
        }

        public void Die()
        {
            Destroy(gameObject);
        }
        #endregion

        public void SetTarget(Vector3 value)
        {
            m_target = value;
        }

        #region Action

        public void Aim()
        {
            StartCoroutine(RotateTowards());
        }

        public void Shoot(WeaponBuff buff, BulletType type, int level)
        {
            StartCoroutine(m_equipment.ShootBullet(buff, type, level));
        }

        public IEnumerator RotateTowards()
        {   
            Vector3 parentPosition = transform.parent.position;

            Vector3 direction = m_target - parentPosition;
            direction.z = 0; // Keep it 2D

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
            float currentAngle = Mathf.Atan2(transform.position.y - parentPosition.y, 
                                            transform.position.x - parentPosition.x) * Mathf.Rad2Deg;
            
            Tween moveTween = DOTween.To(
                () => currentAngle,
                angle => {
                    currentAngle = angle;

                    // Calculate the new position around the parent
                    float radian = currentAngle * Mathf.Deg2Rad;
                    Vector3 offset = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * Constants.EQUIPMENT_RADIUS;

                    Vector3 newPosition = parentPosition + offset;
                    transform.position = newPosition;

                    // Rotate to face outward along the direction of the radius
                    Vector3 lookDirection = (newPosition - parentPosition).normalized;
                    float rotationAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, 0, rotationAngle - 90);
                },
                targetAngle, 
                m_equipment.seekInterval.value
            );

            yield return moveTween.WaitForCompletion();
        }

        #endregion
    }
}
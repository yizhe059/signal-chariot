using UnityEngine.Events;

using InGame.BattleFields.Common;

namespace Utils.Common
{
    public interface IHidable
    {
        public void Hide();
        public void Show();
    }

    public interface IDamageable
    {
        public void TakeDamage(float dmg);
    }

    public interface IDamager
    {
        public void DealDamage(IDamageable target, float dmg);
    }

    public interface IPickable
    {
        public void PickUp();
    }

    public interface IPropertyRegisterable
    {
        public void RegisterPropertyEvent(LimitedPropertyType type,         
                                        UnityAction<float, float> call);
        
        public void UnregisterPropertyEvent(LimitedPropertyType type, 
                                    UnityAction<float, float> call);

        public void RegisterPropertyEvent(UnlimitedPropertyType type,     
                                        UnityAction<float> call);
        
        public void UnregisterPropertyEvent(UnlimitedPropertyType type, 
                                        UnityAction<float> call);
    }

    public interface IDieable
    {
        public void Die();
    }
}
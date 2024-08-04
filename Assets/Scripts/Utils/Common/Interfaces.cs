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
}
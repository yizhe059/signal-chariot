using SetUps;
using InGame.Effects;
using System.Reflection;

namespace InGame.BattleFields.Chariots
{
    public enum SeekMode
    {
        None,
        Nearest,
    }

    public class Tower
    {
        private Bullet m_bullet;
        private UnlimitedProperty m_attack;
        private UnlimitedProperty m_bulletCount;
        private SeekMode m_seekMode;

        private Module m_module;

        public static Tower CreateTower(TowerSetUp towerSetUp, Module module)
        {
            return new Tower();
        }

        public static void DestroyTower(Tower tower)
        {

        }

        public void Effect()
        {
            
        }

        public void GenerateBullet()
        {

        }
    }


}
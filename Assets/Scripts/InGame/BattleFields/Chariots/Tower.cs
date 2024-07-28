using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Tower()
        {

        }

        public void GenerateBullet()
        {
            
        }
    }


}
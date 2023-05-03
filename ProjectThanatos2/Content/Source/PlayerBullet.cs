using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos2.Content.Source
{
    class PlayerBullet : Bullet
    {
        public PlayerBullet(BulletType bulletType) : base(bulletType) { }

        public int damage;

        public override void Update()
        {
            
        }
    }
}

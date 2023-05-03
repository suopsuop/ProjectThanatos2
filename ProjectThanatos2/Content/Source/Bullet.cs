using ProjectThanatos.Content.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos2.Content.Source
{
    class Bullet : Entity
    {

        public enum BulletType
        {
            pellet,
            laser,
            knife
        }


        private static Bullet instance;

        private int bulletSpeed;

        public BulletType bulletType;

        public Bullet(BulletType bulletType) 
        {
            switch(bulletType)
            {
                case BulletType.pellet:
                    bulletSpeed = 8;
                    break;

                    case BulletType.laser:
                    bulletSpeed = 16;
                    break;

                    case BulletType.knife:
                    bulletSpeed = 32;
                    break;
            }
        }
        

        public override void Update()
        {
            
        }
    }
}

using Microsoft.Xna.Framework;
using ProjectThanatos;
using ProjectThanatos.Content.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos2.Content.Source 
{
    internal class Timer : Entity
    {
        public Action trigger; // Whatever function is being held to call
        public double interval;
        double elapsed = 0;

        GameTime gameTime = ProjectThanatos.ProjectThanatos.GameTime;

        public Timer(float interval, Action trigger)
        {
            this.interval = interval;
            this.trigger = trigger;
        }

        public override void Update()
        {
            elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= interval)
            {
                if (trigger is not null)
                {
                    trigger.Invoke();
                }
                Destroy();
            }
        }

        public void Destroy()
        {
            TimerMan.Remove(this);
        }
    }
}

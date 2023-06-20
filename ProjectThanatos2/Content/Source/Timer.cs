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
        public Action trigger;
        public double interval;
        double elapsed = 0;
        public readonly int ID;



        GameTime gameTime = ProjectThanatos.ProjectThanatos.GameTime;

        public Timer(int ID, float interval, Action trigger)
        {
            this.ID = ID;
            this.interval = interval;
            this.trigger = trigger;
        }

        //public void Wait(double delayTime, Action action)
        //{
        //    if (timerTime <= gameTime.TotalGameTime.TotalMilliseconds)
        //    {
        //        timerTime = gameTime.TotalGameTime.TotalMilliseconds + delayTime;
        //        action.Invoke();
        //    }
        //}

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

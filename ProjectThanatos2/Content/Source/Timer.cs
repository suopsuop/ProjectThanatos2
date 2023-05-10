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
        double elapsed;

        GameTime gameTime = ProjectThanatos.ProjectThanatos.GameTime;

        public Timer()
        {
            //this.delayTime = delayTime;
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
                trigger.Invoke();
                Destroy();
            }
        }

        public void Destroy()
        {
            TimerMan.Remove(this);
        }

        public static void Create(float interval, Action trigger)
        {
            Timer timer = new Timer() { interval = interval, trigger = trigger };
            TimerMan.Add(timer);
        }
    }
}

using Microsoft.Xna.Framework;
using ProjectThanatos.Content.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos2.Content.Source
{
    internal class TimerMan 
    {
        GameTime gameTime = ProjectThanatos.ProjectThanatos.GameTime;


        static List<Timer> toRemove = new List<Timer>();
        static List<Timer> timers = new List<Timer>();

        //public static TimerMan TimerManInstance { get; private set; }
        
        public static void Add(Timer timer)
        {
            timers.Add(timer);
        }
        public static void Remove(Timer timer)
        {
            toRemove.Remove(timer);
        }

        public void Update()
        {
            foreach (Timer timer in toRemove)
            {
                timers.Remove(timer);
            }
            toRemove.Clear();
            foreach (Timer timer in timers)
            {
                timer.Update();
            }
        }
    }
}

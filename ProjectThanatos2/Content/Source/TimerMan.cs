using Microsoft.Xna.Framework;
using ProjectThanatos.Content.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos2.Content.Source
{
    static class TimerMan 
    {
        static GameTime gameTime = ProjectThanatos.ProjectThanatos.GameTime;

        static List<Timer> toRemove = new List<Timer>();
        //static List<Timer> toAdd = new List<Timer>();
        static List<Timer> timers = new List<Timer>();

        static int ID = 0;

        //public static TimerMan TimerManInstance { get; private set; }

        static TimerMan() { }

        public static void Add(Timer timer)
        {
            timers.Add(timer);
        }
        public static void Remove(Timer timer)
        {
            toRemove.Add(timer);
        }

        public static void Create(float interval, Action trigger)
        {
            ID += 1;
            Timer timer = new Timer(ID, interval, trigger) {};
            Add(timer);
        }

        public static void Update()
        {
            foreach (Timer timer in toRemove)
            {
                timers.Remove(timer);
            }
            toRemove.Clear();

            //foreach (Timer timer in toAdd)
            //{
            //    timers.Add(timer);
            //}
            //toAdd.Clear();

            foreach (Timer timer in timers.ToList())
            {
                timer.Update();
            }

        }
    }
}

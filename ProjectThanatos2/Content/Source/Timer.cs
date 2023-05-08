using Microsoft.Xna.Framework;
using ProjectThanatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos2.Content.Source
{
    internal class Timer
    {
        public double timerTime = 0;
        public double delayTime = 0;

        GameTime gameTime = ProjectThanatos.ProjectThanatos.GameTime;

        public Timer(double delayTime)
        {
            this.delayTime = delayTime;
        }

        public void Wait(double delayTime, Action action)
        {

        }
    }
}

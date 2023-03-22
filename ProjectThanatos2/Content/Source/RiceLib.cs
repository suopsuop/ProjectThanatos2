using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos.Content.Source
{
    public class RiceLib
    {
        public dynamic normaliseBetweenTwoRanges(dynamic value, dynamic valMin, dynamic valMax, dynamic newMin, dynamic newMax)
        {
            return newMin + (value - valMin) * (newMax - newMin) / (valMax - valMin);
        }
    }
}

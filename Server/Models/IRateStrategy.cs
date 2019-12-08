using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    /// <summary>
    /// Weapon RATE OF FIRE strategy interface
    /// Defined by RPS (rounds per second)
    /// </summary>
    public interface IRateStrategy
    {
        double Rate();
    }

    class LowRate : IRateStrategy
    {
        public double Rate()
        {
            return 1.50;
        }
    }

    class MediumRate : IRateStrategy
    {
        public double Rate()
        {
            return 6.50;
        }
    }

    class HighRate : IRateStrategy
    {
        public double Rate()
        {
            return 12.00;
        }
    }

    class UltraRate : IRateStrategy
    {
        public double Rate()
        {
            return 16.00;
        }
    }
}

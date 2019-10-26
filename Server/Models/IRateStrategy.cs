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
        double rate();
    }

    class LowRate : IRateStrategy
    {
        public double rate()
        {
            return 1.50;
        }
    }

    class MediumRate : IRateStrategy
    {
        public double rate()
        {
            return 6.50;
        }
    }

    class HighRate : IRateStrategy
    {
        public double rate()
        {
            return 12.00;
        }
    }

    class UltraRate : IRateStrategy
    {
        public double rate()
        {
            return 16.00;
        }
    }
}

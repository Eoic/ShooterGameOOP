using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interpretor
{
    class NumberExpression : IExpression
    {
        private int value;

        public NumberExpression(int newValue)
        {
            value = newValue;
        }

        public int execute()
        {
            return value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interpretor
{
    class SumExpression : IExpression
    {
        List<IExpression> e;

        public SumExpression(List<IExpression> e)
        {
            this.e = e;
        }
        public int execute()
        {
            int sum = 0;

            foreach (var expression in this.e)
                sum += expression.execute();

            return sum;
        }
    }
}

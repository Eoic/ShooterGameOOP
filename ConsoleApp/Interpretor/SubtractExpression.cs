using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interpretor
{
    class SubtractExpression : IExpression
    {
        IExpression e;
        int damage;

        public SubtractExpression(IExpression e, int d)
        {
            this.e = e;
            damage = d;
        }
        public int execute()
        {


            return e.execute() - damage;
        }
    }
}

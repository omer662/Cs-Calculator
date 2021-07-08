﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OOProject
{
    class MinusOp : IOp
    {
        private IOp op1;
        private IOp op2;
        private bool normal;

        public MinusOp(IOp op1, IOp op2)
        {
            this.op1 = op1;
            this.op2 = op2;
            this.normal = false;
        }

        public MinusOp(IOp op1, IOp op2, bool normal)
        {
            this.op1 = op1;
            this.op2 = op2;
            this.normal = normal;
        }

        public double Calculate(double x)
        {
            return this.op1.Calculate(x) - this.op2.Calculate(x);
        }

        public IOp GetDerivative()
        {
            return new MinusOp(op1.GetDerivative(), op2.GetDerivative()).Clean();
        }

        public IOp Clean()
        {
            IOp newOp1 = op1.Clean();
            IOp newOp2 = op2.Clean();
            if (newOp1 is NumOp && newOp2 is NumOp)
                return new NumOp((int)(newOp1.Calculate(0) - newOp2.Calculate(0)));
            else if (newOp2 is NumOp && newOp2.Calculate(0) == 0)
                return newOp1;
            else if (newOp1 is XOp && newOp2 is XOp)
                return new NumOp(0);
            else
                return new MinusOp(newOp1, newOp2, true);
        }

        public string GetSign() { return "- "; }

        public override string ToString()
        {
            if (normal)
                return op1.ToString() + this.GetSign() + op2.ToString();
            return this.GetSign() + op1.ToString() + op2.ToString();
        }

        public string GetFinalResult(double x)
        {
            return ToString() + "= " + this.Calculate(x).ToString() + " ( x = " + x + " )";
        }
    }
}

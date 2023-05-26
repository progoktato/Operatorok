using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConOperatorok.DataSource
{
    public class Expression
    {
        static string[] UseableOperations = { "+", "-", "*", "/", "mod", "div" };

        int operandLeft;
        int operandRight;
        string operation;

        public Expression(string strLine)
        {
            string[] fields = strLine.Split();
            if (fields.Length != 3)
            {
                throw new InvalidOperationException("Túl sok, vagy túl kevés tag a sorban!");
            }
            try
            {
                this.operandLeft = int.Parse(fields[0]);
                this.operandRight = int.Parse(fields[2]);
            }
            catch (Exception)
            {

                throw new ArgumentException("Az egyik operandus nem egész szám formátumú!");
            }
            fields[1] = fields[1].ToLower();
            this.operation = fields[1].ToLower();
        }

        public Expression(int operandLeft, int operandRight, string operation)
        {
            this.operandLeft = operandLeft;
            this.operandRight = operandRight;
            this.operation = operation;
        }

        public double OperandLeft { get => operandLeft; }
        public double OperandRight { get => operandRight; }
        public string Operation { get => this.operation; }
        public bool IsValidOperation { get => UseableOperations.Any(x => x == this.Operation); }

        public string Result
        {
            get
            {
                double? localResult = null;
                try
                {
                    switch (operation)
                    {
                        case "+":
                            localResult = operandLeft + operandRight;
                            break;
                        case "-":
                            localResult = operandLeft - operandRight;
                            break;
                        case "*":
                            localResult = operandLeft * operandRight;
                            break;
                        case "/":
                            if (operandRight == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            localResult = (double)operandLeft / operandRight;
                            break;
                        case "mod":
                            localResult = operandLeft % operandRight;
                            break;
                        case "div":
                            localResult = operandLeft / operandRight;
                            break;
                    }
                    return $"{operandLeft} {operation} {operandRight} = " +
                        $"{(localResult == null ? "Hibás operátor!" : localResult)}";
                }
                catch (Exception)
                {
                    return $"{operandLeft} {operation} {operandRight} = Egyéb hiba!";
                }
            }
        }
    }
}

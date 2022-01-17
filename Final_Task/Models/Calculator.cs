using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Task.Models
{
    public class Calculator
    {
        private string result;

        # region Конструкторы

        public Calculator(string firstOperation, string secondOperation, string operation)
        {
            FirstOperation = firstOperation;
            SecondOperation = secondOperation;
            Operation = operation;
            result = string.Empty;
        }

        public Calculator(string firstOperation, string operation)
        {
            FirstOperation = firstOperation;
            SecondOperation = string.Empty;
            Operation = operation;
            result = string.Empty;
        }

        public Calculator()
        {
            FirstOperation = string.Empty;
            SecondOperation = string.Empty;
            Operation = string.Empty;
            result = string.Empty;
        }
        #endregion


        # region Свойства

        public string FirstOperation { get; set; }
        public string SecondOperation { get; set; }
        public string Operation { get; set; }
        public string Result { get { return result; } }
        #endregion


        # region Методы
        public void CalculateResult()
        {
            double Factorial(double firstOperation)
            {
                if (firstOperation == 1) return 1;

                return firstOperation * Factorial((firstOperation) - 1);
            }
            switch (Operation)
            {
                case ("+"):
                    result = (Convert.ToDouble(FirstOperation) + Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("-"):
                    result = (Convert.ToDouble(FirstOperation) - Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("x"):
                    result = (Convert.ToDouble(FirstOperation) * Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("/"):
                    result = (Convert.ToDouble(FirstOperation) / Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("%"):
                    result = (Convert.ToDouble(FirstOperation)* Convert.ToDouble(SecondOperation)/ 100).ToString();
                    break;

                case ("√х"):
                    result = Convert.ToDouble(FirstOperation) < 0 ? "Ошибка" : Math.Sqrt(Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("х²"):
                    result = Math.Pow(Convert.ToDouble(FirstOperation), 2).ToString();
                    break;

                case ("1/x"):
                    result = Convert.ToDouble(FirstOperation) == 0 ? "Ошибка" : (1 / Convert.ToDouble(FirstOperation)).ToString();
                    break;

                case ("cos"):
                    result = Math.Cos(Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("sin"):
                    result = Math.Sin(Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("tan"):
                    result = Math.Tan(Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("exp"):
                    result = Math.Exp(Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("lg"):
                    result = Math.Log10(Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("In"):
                    result = Math.Log(Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("xᵞ"):
                    result = Math.Pow(Convert.ToDouble(FirstOperation), Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("10ᵡ"):
                    result = Math.Pow(10,Convert.ToDouble(SecondOperation)).ToString();
                    break;

                case ("n!"):
                   
                    result = Factorial(Convert.ToDouble(SecondOperation)).ToString();
                    break;

            }
        }
        #endregion
    }
}

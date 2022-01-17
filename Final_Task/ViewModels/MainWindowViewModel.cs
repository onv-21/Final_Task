using Final_Task.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Final_Task.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Событие изменение свойств
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
        #endregion

        //Коллекция для сохранения вычислений в файл
        private Collection<string> computationHistory;

        #region Поля

        private Calculator calculation;

        private string lastOperation;
        private bool newDisplayRequired = false;
        private string fullExpression;
        private string display;
        #endregion

        #region Конструктор

        public MainWindowViewModel()
        {
            calculation = new Calculator();

            //Коллекция для сохранения вычислений в файл
            computationHistory = new Collection<string>();

            //Поля
            display = "0";
            FirstOperation = string.Empty;
            SecondOperation = string.Empty;
            Operation = string.Empty;
            lastOperation = string.Empty;
            fullExpression = string.Empty;

            //Команды
            CopyCommand = new RelayCommand(OnCopyCommandExecute, CanCopyCommandExecuted);
            OperationButtonPressCommand = new RelayCommand(OnOperationButtonPressCommandExecute, CanOperationButtonPressCommandExecuted);
            SingularOperationButtonPressCommand = new RelayCommand(OnSingularOperationButtonPressCommandExecute, CanSingularOperationButtonPressCommandExecuted);
            DigitButtonPressCommand = new RelayCommand(OnDigitButtonPressCommandExecute, CanDigitButtonPressCommandExecuted);
        }
        #endregion

        #region Свойства

       
        public string FirstOperation
        {
            get => calculation.FirstOperation;
            set { calculation.FirstOperation = value; }
        }

       
        public string SecondOperation
        {
            get => calculation.SecondOperation;
            set { calculation.SecondOperation = value; }
        }

        
        public string Operation
        {
            get => calculation.Operation;
            set { calculation.Operation = value; }
        }

        
        public string LastOperation
        {
            get => lastOperation;
            set { lastOperation = value; }
        }

        
        public string Result
        {
            get => calculation.Result;
        }

       
        public string Display
        {
            get => display;
            set => Set(ref display, value);
        }

       
        public string FullExpression
        {
            get => fullExpression;
            set => Set(ref fullExpression, value);
        }
        #endregion

        #region Команды

        //Копирование результата вычислений с дисплея в буфер обмена
        public ICommand CopyCommand { get; }

        private void OnCopyCommandExecute(object p)
        {
            Clipboard.SetText(Display);
        }

        private bool CanCopyCommandExecuted(object p) => true;

        //Команда обработчик нажатия на кнопки оперций с двумя операциями
        public ICommand OperationButtonPressCommand { get; }

        private void OnOperationButtonPressCommandExecute(object p)
        {
            if (FirstOperation == string.Empty || LastOperation == "=")
            {
                FirstOperation = display;
                LastOperation = p.ToString();
            }
            else
            {
                SecondOperation = display;
                Operation = lastOperation;
                calculation.CalculateResult();

                if (Operation == "/" && SecondOperation == "0")
                {
                    Display = "Ошибка";
                    newDisplayRequired = true;
                    return;
                }
                else
                {
                    FullExpression = Math.Round(Convert.ToDouble(FirstOperation), 10) + Operation
                        + Math.Round(Convert.ToDouble(SecondOperation), 10) + "="
                        + Math.Round(Convert.ToDouble(Result), 10);

                    computationHistory.Add(FullExpression);
                    LastOperation = p.ToString();
                    Display = Result;
                    FirstOperation = display;
                }
            }
            newDisplayRequired = true;
        }

        private bool CanOperationButtonPressCommandExecuted(object p) => true;

        /// <summary>Команда обработчик нажатия на кнопки оперций с одним операндом</summary>
        public ICommand SingularOperationButtonPressCommand { get; }

        private void OnSingularOperationButtonPressCommandExecute(object p)
        {
            FirstOperation = Display;
            Operation = p.ToString();
            calculation.CalculateResult();

            if (Operation == "1/x" && FirstOperation == "0" || Operation == "√х" && Convert.ToDouble(FirstOperation) < 0)
            {
                Display = "Ошибка";
                newDisplayRequired = true;
                return;
            }
            else
            {
                FullExpression = Operation + "(" + Math.Round(Convert.ToDouble(FirstOperation), 10) + ")="
                    + Math.Round(Convert.ToDouble(Result), 10);

                computationHistory.Add(FullExpression);
                LastOperation = "=";
                Display = Result;
                FirstOperation = display;
                newDisplayRequired = true;
            }
        }

        private bool CanSingularOperationButtonPressCommandExecuted(object p) => true;

        /// <summary>Команда обработчик нажатия на цифровые кнопки</summary>
        public ICommand DigitButtonPressCommand { get; }

        private void OnDigitButtonPressCommandExecute(object p)
        {
            switch (p)
            {
                case "C":
                    Display = "0";
                    FirstOperation = string.Empty;
                    SecondOperation = string.Empty;
                    Operation = string.Empty;
                    LastOperation = string.Empty;
                    FullExpression = string.Empty;
                    break;

                case "←":
                    Display = display.Length > 1 ? display.Substring(0, display.Length - 1) : "0";
                    break;
                case "+/-":
                    Display = display.Contains("-") ? display.Remove(display.IndexOf("-"), 1) : "-" + display;
                    break;
                case ",":
                    if (newDisplayRequired)
                    {
                        Display = "0,";
                    }
                    else
                    {
                        if (!display.Contains(","))
                        {
                            Display = display + ",";
                        }
                    }
                    break;
                default:
                    Display = display == "0" || newDisplayRequired ? p.ToString() : display + p.ToString();
                    break;

                case "(":
                    Display = display + "(";
                    break;

                case ")":
                    Display = display + ")";
                    break;

                case "π":
                    Display = display + Math.PI;
                    break;

                case "e":
                    Display = display + Math.E;
                    break;
            }
            newDisplayRequired = false;
        }

        private bool CanDigitButtonPressCommandExecuted(object p) => true;
        #endregion
    }
}

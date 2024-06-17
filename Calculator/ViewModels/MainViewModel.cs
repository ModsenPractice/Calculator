using Calculator.Models;
using Calculator.Services.Data.Interfaces;
using Calculator.Services.Interfaces;
using Calculator.ViewModels.Interfaces;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ViewModels
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
        private readonly IValidatorManager _validator;
        private readonly IAlertService _alert;
        private readonly ICalculator _calculator;
        private readonly IDataService<Function> _functions;
        private readonly IDataService<Variable> _variables;

        //Not sure if it should be here but whatever
        private static readonly Dictionary<string, string> _actionReplacements = new Dictionary<string, string>()
        {
            { "sqrt", "sqrt(" },
            { "sin", "sin(" },
            { "cos", "cos(" },
            { "exp", "exp(" },
            { "ln", "ln(" },
            { "^2", "^(2)" },
            { "^n", "^(" },
        };

        
        private string _result = "";
        private int _currentPos = 0;

        public string Result
        {
            get => _result;
            set
            {
                if (_result != value)
                {
                    _result = value;
                    OnPropertyChanged();
                }
            }
        }

        public int CurrentPos
        {
            get => _currentPos;
            set
            {
                if (_currentPos != value)
                {
                    _currentPos = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> Functions { get; set; }
        public ObservableCollection<string> Variables { get; set; }

        public MainViewModel(IValidatorManager validator, IAlertService alert,
            ICalculator calculator, IDataService<Function> functions,
            IDataService<Variable> variables)
        {
            _validator = validator;
            _alert = alert;
            _calculator = calculator;
            _functions = functions;
            _variables = variables;

            Functions = new ObservableCollection<string>();
            Variables = new ObservableCollection<string>();

            LoadData();
        }

        private async void LoadData()
        {
            foreach(var function in await _functions.GetAsync())
            {
                Functions.Add(function);
            }
            foreach (var variable in await _variables.GetAsync())
            {
                Variables.Add(variable);
            }
        }

        #region Input commands

        [RelayCommand]
        void AddSymbol(string symbol)
        {
            if(_actionReplacements.ContainsKey(symbol))
            {
                symbol = _actionReplacements[symbol];
            }

            Result += symbol;
        }

        [RelayCommand]
        async Task OnCalculate()
        {
            var validationResult = _validator.ValidateExpression(Result);
            if (validationResult.Status == Models.Enum.ResultStatus.Error)
            {
                await _alert.DisplayErrorAsync(validationResult.ErrorMessages);
                return;
            }

            try
            {
                Result = await _calculator.CalculateAsync(Result);
            }
            catch (Exception ex)
            {
                await _alert.DisplayErrorAsync(ex.Message);
            }
        }

        [RelayCommand]
        void Clear()
        {
            Result = "";
        }

        [RelayCommand]
        void RemoveLast()
        {
            if(Result.Length != 0)
            {
                Result = Result.Remove(Result.Length - 1);
            }
        }

        #endregion

        #region Function commands

        [RelayCommand]
        async Task AddFunction(string value)
        {
            var validationResult = _validator.ValidateFunction(value);
            if (validationResult.Status == Models.Enum.ResultStatus.Error)
            {
                await _alert.DisplayErrorAsync(validationResult.ErrorMessages);
                return;
            }

            try
            {
                await _functions.AddAsync(value);
                Functions.Add(value);
            }
            catch (Exception ex)
            {
                await _alert.DisplayErrorAsync(ex.Message);
            }
        }

        /// <summary>
        /// Needed to use object instead of string thanks to MAUI ListView bug
        /// https://github.com/dotnet/maui/issues/20883
        /// </summary>
        /// <param name="obj">Function expression to delete</param>
        [RelayCommand]
        async Task DeleteFunction(object obj)
        {
            if (obj is not string value) return;

            try
            {
                await _functions.DeleteAsync(value);
                var res = Functions.Remove(value);
            }
            catch (Exception ex)
            {
                await _alert.DisplayErrorAsync(ex.Message);
            }
        }

        /// <summary>
        /// Needed to use object instead of string thanks to MAUI ListView bug
        /// https://github.com/dotnet/maui/issues/20883
        /// </summary>
        /// <param name="obj">Variable expression to use</param>
        [RelayCommand]
        void UseFunction(object obj)
        {
            if (obj is not string expression) return;

            var prevPos = CurrentPos;
            var name = expression.Substring(0, expression.IndexOf('='));

            Result = Result
                .Insert(CurrentPos, name);

            CurrentPos = prevPos + name.Length;
        }

        #endregion

        #region Variable commands

        [RelayCommand]
        async Task OnAddVariable(string value)
        {
            var validationResult = _validator.ValidateVariable(value);
            if (validationResult.Status == Models.Enum.ResultStatus.Error)
            {
                await _alert.DisplayErrorAsync(validationResult.ErrorMessages);
                return;
            }

            try
            {
                await _variables.AddAsync(value);
                Variables.Add(value);
            }
            catch (Exception ex)
            {
                await _alert.DisplayErrorAsync(ex.Message);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Needed to use object instead of string thanks to MAUI ListView bug
        /// https://github.com/dotnet/maui/issues/20883
        /// </summary>
        /// <param name="obj">Variable expression to delete</param>
        [RelayCommand]
        async Task OnDeleteVariable(object obj)
        {
            if (obj is not string value) return;

            //Add repo
            try
            {
                await _variables.DeleteAsync(value);
                var res = Variables.Remove(value);
            }
            catch (Exception ex)
            {
                await _alert.DisplayErrorAsync(ex.Message);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Needed to use object instead of string thanks to MAUI ListView bug
        /// https://github.com/dotnet/maui/issues/20883
        /// </summary>
        /// <param name="obj">Variable expression to use</param>
        [RelayCommand]
        void UseVariable(object obj)
        {
            if (obj is not string expression) return;

            var prevPos = CurrentPos;
            var name = expression.Split('=')[0];

            Result = Result.Insert(prevPos, name);

            CurrentPos = prevPos + name.Length;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}

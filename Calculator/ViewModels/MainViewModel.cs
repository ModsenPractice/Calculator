using Calculator.Services.Interfaces;
using Calculator.ViewModels.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
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

        public MainViewModel(IValidatorManager validator, IAlertService alert)
        {
            _validator = validator;
            _alert = alert;

            Functions = new ObservableCollection<string>();
            Variables = new ObservableCollection<string>();
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
            // else if(Validate(this.result.Text) = Ok);
            // this.result.Text = Calculate
            // else
            //await this.DisplayAlert("Validation error",( Validate(this.result.Text).Error), "Exite");
            Result = "Non implemented";

            await Task.CompletedTask;
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

            //Add repo
            Functions.Add(value);

            await Task.CompletedTask;
        }

        /// <summary>
        /// Needed to use object instead of string thanks to MAUI ListView bug
        /// https://github.com/dotnet/maui/issues/20883
        /// </summary>
        /// <param name="obj">Function expression to delete</param>
        [RelayCommand]
        async Task DeleteFunction(object obj)
        {
            if (obj is not string expression) return;

            var res = Functions.Remove(expression);

            await Task.CompletedTask;
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

            //Add repo
            Variables.Add(value);

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
            if (obj is not string name) return;

            //Add repo
            var res = Variables.Remove(name);

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

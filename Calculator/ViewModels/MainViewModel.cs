using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ViewModels
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
        private string _result = "";

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

        public ObservableCollection<string> Functions { get; set; }
        public ObservableCollection<string> Variables { get; set; }

        public MainViewModel()
        {
            Functions = new ObservableCollection<string>();
            Variables = new ObservableCollection<string>();
        }

        [RelayCommand]
        void AddSymbol(string symbol)
        {
            Result += symbol;
        }

        [RelayCommand]
        void AddSpecificSymbol(string symbol)
        {
            switch (symbol)
            {
                case "sqrt":
                    symbol = "sqrt(";
                    break;
                case "^2":
                    symbol = "^(2)";
                    break;
                case "^n":
                    symbol = "^(";
                    break;
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
        async Task AddFunction(string value)
        {
            //Button button = (Button)sender;
            //if (this.entryFunction.Text != null)
            //{
            //    //functionsUser.Add(this.entryFunction.Text);
            //    // if(Validate(this.entryFunction.Text) = Ok);
            //    // Add function
            //    // else
            //    //await this.DisplayAlert("Validation error",( Validate(this.entryFunction.Text).Error), "Exite"); 
            //}

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
            //    Button button = (Button)sender;
            //    if (this.entryFunction.Text != null)
            //        functionsUser.Remove(this.entryFunction.Text);
            //Delete function 
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
            //if (this.entryVariable.Text != null)
            //    this.result.Text += this.entryVariable.Text.Substring(0, this.entryVariable.Text.IndexOf('='));
            if (obj is not string expression) return;

            Result += expression.Substring(0, expression.IndexOf('='));
        }



        [RelayCommand]
        async Task OnAddVariable(string value)
        {
            //Button button = (Button)sender;
            //if (this.entryVariable.Text != null)
            //{
            //    variables.Add(this.entryVariable.Text);

                // if(Validate(this.entryVariable.Text) = Ok);
                // Add function
                // else
                //await this.DisplayAlert("Validation error",( Validate(this.entryVariable.Text).Error), "Exite"); 
            //}

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
            //Button button = (Button)sender;
            //if (this.entryVariable.Text != null)
            //    variables.Remove(this.entryVariable.Text);
            //Delete variable 
            if (obj is not string name) return;

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
            //if (this.entryVariable.Text != null)
            //    this.result.Text += this.entryVariable.Text.Substring(0, this.entryVariable.Text.IndexOf('='));
            if (obj is not string expression) return;

            Result += expression.Split('=')[0];
        }


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}

using System.Globalization;
using System;
using System.Text;
using Calculator.Services.Interfaces;
using System.Collections;
using System.Collections.ObjectModel;
namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        
        
        ObservableCollection<string> functionsUser;
        ObservableCollection<string> variables;
        public MainPage()
        {
            
            InitializeComponent();

            OnClear(this, null);

            this.functionsUser = new ObservableCollection<string> ();
            functionsUserList.ItemsSource = this.functionsUser;

            functionsBuiltList.ItemsSource = new List<string> { "sin(x)", "cos(x)", "tg(x)", "ln(x)", "log(y,x)", "exp(x)" };

            this.variables = new ObservableCollection<string> ();
            variablesList.ItemsSource = this.variables; 

            BindingContext = this;
            
        }

        private void variablesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.entryVariable.Text = e.SelectedItem.ToString();
        }

        private void functionsUserList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.entryFunction.Text = e.SelectedItem.ToString();
        }

        private void functionsBuiltList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.entryFunction.Text = e.SelectedItem.ToString();
        }

        private void OnClear(object sender, EventArgs e)
        {
            this.result.Text = ""; 
        }

        private void OnMove(object sender, EventArgs e)
        {
        }

        private void OnSpecificSymbol(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string btnPressed = button.Text;
             
            switch(btnPressed)
            {
                case "sqrt":
                    btnPressed = "sqrt(";
                    break;
                case "^2":
                    btnPressed = "^(2)";
                    break;
                case "^n":
                    btnPressed = "^(";
                    break;
            }

            this.result.Text += btnPressed;

        }

        private void OnSymbol(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string btnPressed = button.Text;

            this.result.Text += btnPressed;
        }

        async private void OnCalculate(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (this.result.Text == "")
                this.result.Text = "0";
            // else if(Validate(this.result.Text) = Ok);
            // this.result.Text = Calculate
            // else
            //await this.DisplayAlert("Validation error",( Validate(this.result.Text).Error), "Exite");
        }

        async private void OnAddFunction(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (this.entryFunction.Text != null)
            {
                functionsUser.Add(this.entryFunction.Text);
                // if(Validate(this.entryFunction.Text) = Ok);
                // Add function
                // else
                //await this.DisplayAlert("Validation error",( Validate(this.entryFunction.Text).Error), "Exite"); 
            }
        }

        async private void OnAddVariable(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (this.entryVariable.Text != null) 
            {
                variables.Add(this.entryVariable.Text);
                
                // if(Validate(this.entryVariable.Text) = Ok);
                // Add function
                // else
                //await this.DisplayAlert("Validation error",( Validate(this.entryVariable.Text).Error), "Exite"); 
            }
        }

        private void OnDeleteFunction(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (this.entryFunction.Text != null)
                functionsUser.Remove(this.entryFunction.Text);
            //Delete function 
        }

        private void OnDeleteVariable(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (this.entryVariable.Text != null)
                variables.Remove(this.entryVariable.Text);
            //Delete variable 
        }

        private void OnUseVariable(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (this.entryVariable.Text != null)
                this.result.Text += this.entryVariable.Text.Substring(0, this.entryVariable.Text.IndexOf('='));
        }

        private void OnUserFunction(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (this.entryFunction.Text != null && this.entryFunction.Text.Contains('='))
                this.result.Text += this.entryFunction.Text.Substring(0, this.entryFunction.Text.IndexOf('='));
        }

        private void OnBuiltFunction(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (this.entryFunction.Text != null && !this.entryFunction.Text.Contains('='))
                this.result.Text += this.entryFunction.Text;
        }
    }

}

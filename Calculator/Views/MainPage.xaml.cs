using System.Globalization;
using System;
using System.Text;
using Calculator.Services.Interfaces;
using System.Collections;
using System.Collections.ObjectModel;
using Calculator.ViewModels;
namespace Calculator.Views
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage(MainViewModel viewModel)
        {
            
            InitializeComponent();

            //functionsBuiltList.ItemsSource = new List<string> { "sin(x)", "cos(x)", "tg(x)", "ln(x)", "log(y,x)", "exp(x)" };

            BindingContext = viewModel;
            
        }

        private void VariableSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.entryVariable.Text = e.SelectedItem.ToString();
        }

        private void UserFunctionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.entryFunction.Text = e.SelectedItem.ToString();
        }

        private void BuiltFunctionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.entryFunction.Text = e.SelectedItem.ToString();
        }

        //private void OnBuiltFunction(object sender, EventArgs e)
        //{
        //    Button button = (Button)sender;
        //    if (this.entryFunction.Text != null && !this.entryFunction.Text.Contains('='))
        //        this.result.Text += this.entryFunction.Text;
        //}
    }

}

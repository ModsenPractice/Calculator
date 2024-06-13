using System.Globalization;
using System;
using System.Text;
using Calculator.Services.Interfaces;
namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        string expression;
        string calculationResult;
        public List<string> Functions { get; set; }
        public List<string> Variables { get; set; }
        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);
            Functions = new List<string> { "f(x,y)=sqrt(2)+2-4+8", "f1(x,y)=x^(2)+15-7", "f2(x,y)=(pi*2/4)*180"};
            //Variables = new List<string> { "pi=3.14", "x=5", "y=8" };
            variablesList.ItemsSource = new List<string> { "pi=3.14", "x=5", "y=8" };
            BindingContext = this;
        }
        private void variablesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selected.Text = $"Выбрано: {e.SelectedItem}";
        }
        private void OnClear(object sender, EventArgs e)
        {
            this.result.Text = "0"; 
        }

        private void OnMove(object sender, EventArgs e)
        {
        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (this.result.Text != string.Empty)
                this.result.Text = this.result.Text.Substring(0, this.result.Text.Length - 1);
            else
                OnClear(this, null);

        }

        private void OnSpecificSymbol(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string btnPressed = button.Text;

            if (this.result.Text == "0")
                this.result.Text = string.Empty;
             
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

            if (this.result.Text == "0")
                this.result.Text = string.Empty;

            this.result.Text += btnPressed;
        }

        private void OnCalculate(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            expression = this.result.Text;
            this.result.Text = calculationResult;
        }

        async void OnVariable(object sender, EventArgs e)
        {

        }

        async void OnFunction(object sender, EventArgs e)
        {

        }
    }

}

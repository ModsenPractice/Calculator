using System.Globalization;
using System;
using System.Text;
using Calculator.Services.Interfaces;
namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        int currentState = 1;
        string operatorMath;
        string expression;
        string calculationResult;

        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);
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
            this.result.Text = this.result.Text.Substring(0, this.result.Text.Length -1);
        }

        private void OnSquareRoot(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            string btnPressed = ((char)0x221A).ToString(); 
            if (this.result.Text == "0" || currentState < 0)
            {
                this.result.Text = string.Empty;
                if (currentState < 0)
                    currentState *= -1;
            }

            this.result.Text += btnPressed;
        }

        private void OnDegree(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string btnPressed = button.Text;

            if (this.result.Text == "0" || currentState < 0)
            {
                this.result.Text = string.Empty;
                if (currentState < 0)
                    currentState *= -1;
            }

            if(this.result.Text == "^2")
                btnPressed = "^(2)";
            if (this.result.Text == "^n")
                btnPressed = "^(";

            this.result.Text += btnPressed;

        }

        private void OnToken(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string btnPressed = button.Text;

            if (this.result.Text == "0" || currentState < 0)
            {
                this.result.Text = string.Empty;
                if (currentState < 0)
                    currentState *= -1;
            }

            this.result.Text += btnPressed;
        }

        private void OnNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string btnPressed = button.Text;

            if(this.result.Text == "0" || currentState < 0 )
            {
                this.result.Text = string.Empty;
                if (currentState < 0)
                    currentState *= -1;
            }

            this.result.Text += btnPressed;
        }

        private void OnEqual(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            expression = this.result.Text;
            this.result.Text = calculationResult;
        }

        async void OnVariable(object sender, EventArgs e)
        {

        }
    }

}

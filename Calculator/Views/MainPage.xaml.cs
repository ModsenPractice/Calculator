﻿using System.Globalization;
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


        private void OnMove(object sender, EventArgs e)
        {
        }

        //private void OnBuiltFunction(object sender, EventArgs e)
        //{
        //    Button button = (Button)sender;
        //    if (this.entryFunction.Text != null && !this.entryFunction.Text.Contains('='))
        //        this.result.Text += this.entryFunction.Text;
        //}
    }

}

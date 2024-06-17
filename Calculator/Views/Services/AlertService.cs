using Calculator.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Views.Services
{
    public class AlertService : IAlertService
    {
        #region sync
        public void DisplayMessage(string title, string message)
        {
            Shell.Current.DisplayAlert(title, message, "Ok");
        }
        
        public void DisplayError(string errorMessage)
        {
            Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
        }
        
        public void DisplayError(IEnumerable<string> errorMessages)
        {
            var errorMessage = "";

            foreach (var error in errorMessages)
            {
                errorMessage += error;
                errorMessage += '\n';
            }

            Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
        }
        #endregion

        #region async
        public async Task DisplayMessageAsync(string title, string message)
        {
            await Shell.Current.DisplayAlert(title, message, "Ok");
        }

        public async Task DisplayErrorAsync(string errorMessage)
        {
            await Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
        }

        public async Task DisplayErrorAsync(IEnumerable<string> errorMessages)
        {
            var errorMessage = "";

            foreach (var error in errorMessages)
            {
                errorMessage += error;
                errorMessage += '\n';
            }

            await Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
        }
        #endregion
    }
}

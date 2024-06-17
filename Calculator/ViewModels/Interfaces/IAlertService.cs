using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ViewModels.Interfaces
{
    public interface IAlertService
    {
        Task DisplayMessageAsync(string title, string message);
        Task DisplayErrorAsync(string errorMessage);
        Task DisplayErrorAsync(IEnumerable<string> errorMessages);

        void DisplayMessage(string title, string message);
        void DisplayError(string errorMessage);
        void DisplayError(IEnumerable<string> errorMessages);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Helper
{
    public class MessageService
    {
        public static async Task ShowAsync(string message)
        {
            await App.Current.MainPage.DisplayAlert("Tarefa", message, "Ok");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Todo.Helper;
using Todo.Model;
using Todo.Service;
using Xamarin.Forms;

namespace Todo.ViewModel
{
    public class AddTodoItemViewModel : BaseObservable
    {

        public Command SalvarCommand { get; set; }
        public Command CancelarCommand { get; set; }
        public TodoItem TodoItem { get; set; }

        public AddTodoItemViewModel(TodoItem TodoItem)
        {
            SalvarCommand = new Command(HandleSave);
            CancelarCommand = new Command(HandleCancel);
            this.TodoItem = TodoItem;
        }

        public void HandleSave()
        {
            if (checkItens())
            {
                MessagingCenter.Send<TodoItem>(TodoItem, Constants.SALVAR_ITEM_COMMAND);
            }
            else
            {
                MessagingCenter.Send<string>("Por favor. Preencha a descrição da tarefa.", Constants.EXIBIR_MENSAGEM);
            }

        }

        private bool checkItens()
        {
            if (string.IsNullOrEmpty(TodoItem.Title))
            {
                return false;
            }
            return true;
        }

        public void HandleCancel()
        {
            MessagingCenter.Send<TodoItem>(new TodoItem { }, Constants.CANCELAR_ITEM_COMMAND);
        }


    }
}

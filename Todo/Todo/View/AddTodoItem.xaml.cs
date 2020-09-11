using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Helper;
using Todo.Model;
using Todo.Service;
using Todo.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTodoItem : ContentPage
    {
        private TodoItemService todoItemService;

        public AddTodoItem()
        {
            InitializeComponent();
            todoItemService = new TodoItemService();
            BindingContext = new AddTodoItemViewModel(new TodoItem());
        }

        public AddTodoItem(TodoItem TodoItem)
        {
            InitializeComponent();
            todoItemService = new TodoItemService();
            BindingContext = new AddTodoItemViewModel(TodoItem);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<TodoItem>(this, Constants.SALVAR_ITEM_COMMAND, async (todoItem) => {
                if (todoItem.Id <= 0)
                    _ = await todoItemService.Save(new TodoItem { Title = todoItem.Title });
                else
                    _ = await todoItemService.Update(todoItem);

                await Navigation.PopModalAsync();
            });

            MessagingCenter.Subscribe<TodoItem>(this, Constants.CANCELAR_ITEM_COMMAND, async (todoItem) => {
                await Navigation.PopModalAsync();
            });

            MessagingCenter.Subscribe<string>(this, Constants.EXIBIR_MENSAGEM, async (msg) => {
                await DisplayAlert("Salvar Tarefa", msg, "OK");
            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<TodoItem>(this, Constants.SALVAR_ITEM_COMMAND);
            MessagingCenter.Unsubscribe<TodoItem>(this, Constants.CANCELAR_ITEM_COMMAND);
            MessagingCenter.Unsubscribe<string>(this, Constants.EXIBIR_MENSAGEM);
        }

    }
}
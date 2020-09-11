using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Todo.Helper;
using Todo.Model;
using Todo.Service;
using Todo.View;
using Xamarin.Forms;

namespace Todo.ViewModel
{
    class TodoListViewModel : BaseObservable
    {
        private TodoItemService todoItemService;

        public Command AddItem { get; set; }
        public Command CompletarItemCommand { get; set; }
        public Command DesativarItemCommand { get; set; }
        public Command RemoverItemCommand { get; set; }


        public TodoListViewModel()
        {
            todoItemService = new TodoItemService();
            _ = RefreshTaskList();

            AddItem = new Command(HandleAddItem);
            CompletarItemCommand = new Command<object>((object parameter) =>
            {
                TodoItem itemSelecionado = (parameter as TodoItem);
                _ = HandleCompletarItemCommandAsync(itemSelecionado);
            });

            DesativarItemCommand = new Command<object>((object parameter) =>
            {
                TodoItem itemSelecionado = (parameter as TodoItem);
                _ = HandleDesativarItemCommandAsync(itemSelecionado);
            });

            RemoverItemCommand = new Command<object>((object parameter) =>
            {
                TodoItem itemSelecionado = (parameter as TodoItem);
                _ = HandleRemoverItemCommandAsync(itemSelecionado);
            });
        }

        public string Title => Constants.TITLE_TODO_LIST_VIEW;

        private ILookup<string, TodoItem> _GroupedTodoList;

        public ILookup<string, TodoItem> GroupedTodoList 
        {
            get { return _GroupedTodoList; }

            set { _GroupedTodoList = value; OnPropertyChanged(); }
        }

        private async Task<ILookup<string, TodoItem>> GetGroupedTodoListAsync()
        {
            return (await todoItemService.FindAllAsync())
                        .OrderBy(t => t.IsCompleted)
                        .ThenByDescending(t => t.Id)
                        .ToLookup(t => t.IsCompleted ? "FINALIZADO" : "ATIVO");
        }

        public void HandleAddItem()
        {
            MessagingCenter.Send<TodoItem>(new TodoItem(), Constants.ADD_ITEM_COMMAND);
        }

        public async Task HandleCompletarItemCommandAsync(TodoItem item)
        {
            item.IsCompleted = true;
            _ = await todoItemService.Update(item);
            this.GroupedTodoList = await GetGroupedTodoListAsync();
            MessagingCenter.Send<TodoItem>(new TodoItem(), Constants.ATUALIZAR_LISTAGEM);
        }

        public async Task HandleDesativarItemCommandAsync(TodoItem item)
        {
            item.IsCompleted = false;
            _ = await todoItemService.Update(item);
            this.GroupedTodoList = await GetGroupedTodoListAsync();
            MessagingCenter.Send<TodoItem>(new TodoItem(), Constants.ATUALIZAR_LISTAGEM);
        }

        public async Task HandleRemoverItemCommandAsync(TodoItem item)
        {
            _ = await todoItemService.RemoveAsync(item);
            this.GroupedTodoList = await GetGroupedTodoListAsync();
            MessagingCenter.Send<TodoItem>(new TodoItem(), Constants.ATUALIZAR_LISTAGEM);
        }

        public async Task RefreshTaskList()
        {
            this.GroupedTodoList = await GetGroupedTodoListAsync();
        }

    }
}

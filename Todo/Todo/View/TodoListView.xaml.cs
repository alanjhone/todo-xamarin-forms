using System.Threading.Tasks;
using Todo.Helper;
using Todo.Model;
using Todo.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListView : ContentPage
    {
        public TodoListView()
        {
            InitializeComponent();
            this.BindingContext = new TodoListViewModel();
        }

        private async Task RefreshTaskListAsync()
        {
            listItens.ItemsSource = null;
            var viewModel = (BindingContext as TodoListViewModel);
            await viewModel.RefreshTaskList();
            listItens.ItemsSource = (BindingContext as TodoListViewModel).GroupedTodoList;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var itemSelecionado = (TodoItem) e.SelectedItem;
            await Navigation.PushModalAsync(new AddTodoItem(itemSelecionado));
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<TodoItem>(this, Constants.ADD_ITEM_COMMAND, async (msg) =>
            {
                await Navigation.PushModalAsync(new AddTodoItem());
            });

            _ = RefreshTaskListAsync();

            MessagingCenter.Subscribe<TodoItem>(this, Constants.ATUALIZAR_LISTAGEM, async (msg) =>
            {
                await RefreshTaskListAsync();
            });

            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<TodoItem>(this, Constants.ADD_ITEM_COMMAND);
            MessagingCenter.Unsubscribe<TodoItem>(this, Constants.ATUALIZAR_LISTAGEM);
        }

    }
}
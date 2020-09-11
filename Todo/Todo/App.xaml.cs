using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Threading.Tasks;
using Todo.Helper;
using Todo.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            _ = App.CheckPermissions();

            ThemeManager.LoadTheme();
            MainPage = new NavigationPage(new TodoListView())
            {
                BarBackgroundColor = Color.FromHex(Constants.BarBackgroundColor)
            };
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async static Task CheckPermissions()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                if (results.ContainsKey(Permission.Storage))
                {
                    status = results[Permission.Storage];
                }
            }
        }

    }
}

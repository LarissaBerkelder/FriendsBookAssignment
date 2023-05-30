using FriendsBook.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FriendsBook.Data;
using System.IO;

namespace FriendsBook
{
    public partial class App : Application
    {
        static FriendDataBase database;

        public static FriendDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FriendDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Friends.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
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
    }
}

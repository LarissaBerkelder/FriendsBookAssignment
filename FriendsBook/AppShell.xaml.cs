using FriendsBook.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FriendsBook
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(FriendsEntryPage), typeof(FriendsEntryPage));
            Routing.RegisterRoute(nameof(PictureSelectPage), typeof(PictureSelectPage));    
        }

    }
}

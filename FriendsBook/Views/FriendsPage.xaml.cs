using FriendsBook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FriendsBook.Views
{
    public partial class FriendsPage : ContentPage
    {
        public FriendsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await App.Database.GetFriendsAsync();
            collectionView.ItemsSource = await App.Database.GetFriendsAsync();
          
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection != null)
            {
                Friend friend = (Friend)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(FriendsEntryPage)}?{nameof(FriendsEntryPage.ItemId)}={friend.ID.ToString()}");
            }
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Button Clicked");
            await Shell.Current.GoToAsync(nameof(FriendsEntryPage));

        }
    }
}
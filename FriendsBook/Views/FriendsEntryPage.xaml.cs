using FriendsBook.Models;
using Plugin.Media;
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
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class FriendsEntryPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadFriend(value);
            }
            get
            {
                return (ItemId);
            }
        }


        public FriendsEntryPage()
        {
            InitializeComponent();
            BindingContext = new Friend();
        }

        async void LoadFriend(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                Friend friend = await App.Database.GetFriendsAsync(id);
                BindingContext = friend;

                entry_day.Text = friend.Birthdate.Day.ToString();
                entry_month.Text = friend.Birthdate.Month.ToString();
                entry_year.Text = friend.Birthdate.Year.ToString();
                user_image.Source = friend.ImageSource;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load friend");
            }
        }

        private async void DayEntryChanged(object sender, EventArgs e)
        {
            bool check_entry = int.TryParse(entry_day.Text, out int day);

            if (!check_entry && !string.IsNullOrEmpty(entry_day.Text))
            {
                await DisplayAlert("Incorrect", "Please enter the day as a number", "OK");
            }

        }
        private async void MonthEntryChanged(object sender, EventArgs e)
        {
            bool check_entry = int.TryParse(entry_month.Text, out int day);

            if (!check_entry && !string.IsNullOrEmpty(entry_month.Text))
            {
                await DisplayAlert("Incorrect", "Please enter the month as a number", "OK");
            }

        }
        private async void YearEntryChanged(object sender, EventArgs e)
        {
            bool check_entry = int.TryParse(entry_year.Text, out int day);

            if (!check_entry && !string.IsNullOrEmpty(entry_year.Text))
            {
                await DisplayAlert("Incorrect", "Please enter the year as a number", "OK");
            }

        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // First check if all the entries are filled in
            bool allEntriesFilled = true;

            if (string.IsNullOrEmpty(entry_firstName.Text))
            {
                allEntriesFilled = false;
            }

            if (string.IsNullOrEmpty(entry_lastName.Text))
            {
                allEntriesFilled = false;
            }

            if (string.IsNullOrEmpty(entry_day.Text) || string.IsNullOrEmpty(entry_month.Text) || string.IsNullOrEmpty(entry_year.Text))
            {
                allEntriesFilled = false;
            }

            // Check if the date that is entered is correct
            string date = entry_year.Text + "-" + entry_month.Text + "-" + entry_day.Text;
            bool validDate = DateTime.TryParse(date, out DateTime dateTime);

            if (!validDate)
            {
                await DisplayAlert("Incorrect date", "Please enter a correct date", "OK");
            }

            if (allEntriesFilled && validDate)
            {
                var friend = (Friend)BindingContext;
                friend.FirstName = entry_firstName.Text;
                friend.LastName = entry_lastName.Text;
                friend.Birthdate = dateTime;

                // Saving the data to the database
                friend.Date = DateTime.Now;
                await App.Database.SaveFriendAsync(friend);

                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Information missing", "Please fill in all the fields", "OK");
            }

        }
        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var friend = (Friend)BindingContext;
            bool delete = await DisplayAlert("Carefull!", "Are you sure you want to delete this friend?", "Yes", "No");
            if (delete)
            {
                await App.Database.DeleteFriendAsync(friend);
                await Shell.Current.GoToAsync("..");
            }
        }

        private async void OnPictureButtonClicked(object sender, EventArgs e)
        {
            var friend = (Friend)BindingContext;
            await Shell.Current.GoToAsync($"{nameof(PictureSelectPage)}?{nameof(PictureSelectPage.ItemId)}={friend.ID.ToString()}");

        }

    }
}
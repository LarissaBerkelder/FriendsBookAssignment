using FriendsBook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace FriendsBook.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class PictureSelectPage : ContentPage
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


        private string picturePath = "";

        public string PicturePath { get { return picturePath; } set { picturePath = value;  } }

        public PictureSelectPage()
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
                Debug.WriteLine(friend.ToString());   

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to load friend");

            }
        }

        public async void TakePictureButton(object sender, EventArgs e)
        {
            var friend = (Friend)BindingContext;

            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                   Directory = "UserPictures",
                   Name = $"{friend.FirstName}{DateTime.UtcNow}.jpg",
                   PhotoSize = PhotoSize.Medium,
                   CompressionQuality = 92
                };


                var file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                if (file == null)
                    return;

                ImageUser.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                SaveButton.IsVisible = true; 

                PicturePath = file.Path; 
                
            }
        }

        public async void PictureGalleryButton(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
                var photo = await CrossMedia.Current.PickPhotoAsync();
        }

        public async void SavePictureButton(object sender, EventArgs e)
        {
            var friend = (Friend)BindingContext;
            friend.ImageSource = PicturePath;
            await App.Database.SaveFriendAsync(friend);
            await Shell.Current.GoToAsync("..");
        }


    }
}
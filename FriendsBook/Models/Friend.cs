using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FriendsBook.Models
{
    public class Friend
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }

        private string imageSource = "user.png";

        public string ImageSource 
        { 
            get 
            { 
                return imageSource; 
            } 
            set 
            { 
                imageSource = value; 
            } 
        }


        //private bool defaultImage = true;
        //public bool DefaultImage
        //{
        //    get 
        //    { 
        //        return defaultImage; 
        //    }
        //    set
        //    {
        //        defaultImage = value;
        //    }
        //}

        public DateTime Date { get; set; }

    }
}

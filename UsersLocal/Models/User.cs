using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace UsersLocal.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int Id { get; set; } // AutoIncrement and set primarykey  
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public static explicit operator User(Java.Lang.Object v)
        {
            throw new NotImplementedException();
        }
    }
}
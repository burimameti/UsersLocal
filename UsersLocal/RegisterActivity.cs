using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using UsersLocal.Models;


namespace UsersLocal
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        EditText txtFirstname, txtLastname, txtAddress, txtEmail,txtPassword;
        Button btnCreate;
        DataBase db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewUser);
            //create Database
            db = new DataBase();
            db.createDataBase();
            // Create your application here  
            btnCreate = FindViewById<Button>(Resource.Id.btnCreate);
            txtFirstname = FindViewById<EditText>(Resource.Id.txtFirstname);
            txtLastname = FindViewById<EditText>(Resource.Id.txtLastname);
            txtAddress = FindViewById<EditText>(Resource.Id.txtAddress);
            txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            btnCreate.Click += Btncreate_Click;
        }
        private void Btncreate_Click(object sender, EventArgs e)
        {
            UserDBHelper db = new UserDBHelper(this);
            string pattern = @"(?=(([a-z0-9])\2?(?!\2))+$)^(?=.*[a-zA-Z])(?=.*[0-9])";
            if (txtLastname.Text.Trim().Length > 4 || txtLastname.Text.Trim().Length < 13)
            {
               
              
                bool test = Regex.IsMatch(txtFirstname.Text, pattern, RegexOptions.IgnoreCase);
                if (!test) { 

                    Toast.MakeText(this, "Validation error two or more repeating strings, letter or numbers detected.", ToastLength.Short).Show(); 
                    return;
                }
            }
            else {
                Toast.MakeText(this, "You  must enter combination of strings and numbers in range of min 5 and max 12 characters.", ToastLength.Short).Show();
            }
            if (txtLastname.Text.Trim().Length > 4 || txtLastname.Text.Trim().Length < 13)

            {
                
                bool test = Regex.IsMatch(txtLastname.Text, pattern, RegexOptions.IgnoreCase);
                if (!test)
                {
                    Toast.MakeText(this, "Validation error two or more repeating strings, letter or numbers detected.", ToastLength.Short).Show();
                    return;
                }
            }
            else
            {
                Toast.MakeText(this, "You must enter combination of strings and numbers in range of min 5 and max 12 characters.", ToastLength.Short).Show();
            }
            if (txtAddress.Text.Trim().Length > 4 || txtAddress.Text.Trim().Length < 13)
            {
                bool test = Regex.IsMatch(txtAddress.Text, pattern, RegexOptions.IgnoreCase);
                if (!test)
                {
                    Toast.MakeText(this, "Validation error two or more repeating strings, letter or numbers detected.", ToastLength.Short).Show();

                    return;
                }
            }
            else
            {
                Toast.MakeText(this, "You must enter combination of strings and numbers in range of min 5 and max 12 characters.", ToastLength.Short).Show();
            }
            if (txtEmail.Text.Trim().Length < 5)
            {
                string EmailPattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                if (!Regex.IsMatch(txtEmail.Text, EmailPattern, RegexOptions.IgnoreCase))
                {
                    Toast.MakeText(this, "Email address must be in format someemail@somedomain.com.", ToastLength.Short).Show();
                    return;
                }
            }

            User us = new User();
            us.Firstname = txtFirstname.Text;
            us.Lastname = txtLastname.Text;
            us.Address = txtAddress.Text;
            us.Email = txtEmail.Text;
            try
            {
               
                db.AddNewUser(us);
                Toast.MakeText(this, "New User Created Successfully.", ToastLength.Short).Show();
                Finish();     
                var mainActivity = new Intent(this, typeof(UserActivity));
                StartActivity(mainActivity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
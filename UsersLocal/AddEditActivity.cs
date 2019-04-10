using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UsersLocal.Models;


namespace UsersLocal
{
    [Activity(Label = "AddEditActivity")]
    public class AddEditActivity : Activity
    {
        EditText txtFirstname, txtLastname, txtAddress, txtEmail;
        Button btnSave;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddEditUser);
            txtFirstname = FindViewById<EditText>(Resource.Id.addEdit_Fname);
            txtLastname = FindViewById<EditText>(Resource.Id.addEdit_Lname);
            txtAddress = FindViewById<EditText>(Resource.Id.addEdit_Address);
            txtEmail = FindViewById<EditText>(Resource.Id.addEdit_Email);
            btnSave = FindViewById<Button>(Resource.Id.addEdit_btnSave);
            btnSave.Click += buttonSave_Click;
        }
        private void LoadDataForEdit(string Id)
        {
            UserDBHelper db = new UserDBHelper(this);
            ICursor cData = db.getUserById(int.Parse(Id));
            if (cData.MoveToFirst())
            {
                txtFirstname.Text = cData.GetString(cData.GetColumnIndex("Firstname"));
                txtLastname.Text = cData.GetString(cData.GetColumnIndex("Lastname"));
                txtAddress.Text = cData.GetString(cData.GetColumnIndex("Address"));
                txtEmail.Text = cData.GetString(cData.GetColumnIndex("Email"));
            }
        }
        void buttonSave_Click(object sender, EventArgs e)
        {
            string pattern = @"(?=(([a-z0-9])\2?(?!\2))+$)^(?=.*[a-zA-Z])(?=.*[0-9])";
            UserDBHelper db = new UserDBHelper(this);
            if (txtFirstname.Text.Trim().Length > 4 || txtFirstname.Text.Trim().Length < 13)
            {
                bool test = Regex.IsMatch(txtFirstname.Text, pattern, RegexOptions.IgnoreCase);
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
                    {
                        Toast.MakeText(this, "Validation error two or more repeating strings, letter or numbers detected.", ToastLength.Short).Show();
                        return;
                    }
                }
                else
                {
                    Toast.MakeText(this, "You must enter combination of strings and numbers in range of min 5 and max 12 characters.", ToastLength.Short).Show();
                }
                if (txtEmail.Text.Trim().Length > 4)
                {
                    string EmailPattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                    if (!Regex.IsMatch(txtEmail.Text, EmailPattern, RegexOptions.IgnoreCase))
                    {
                        Toast.MakeText(this, "Email address must be in format someemail@somedomain.com.",
                            ToastLength.Short).Show();
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
                    Toast.MakeText(this, "New Contact Created Successfully.", ToastLength.Short).Show();
                    Finish();
                    //Go to main activity after save/edit
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
}
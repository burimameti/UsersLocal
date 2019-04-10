using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using SQLite;
using System.IO;
using System;
using System.Linq;
using UsersLocal.Models;



namespace UsersLocal
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText txtEmail;
        EditText txtPassword;
        Button btncreate;
        Button btnsign;
        DataBase db;
        ImageView img;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Main);
            db = new DataBase();
            db.createDataBase();
            // Get our button from the layout resource,  
            // and attach an event to it  
            img = FindViewById<ImageView>(Resource.Id.imageView1);
            btnsign = FindViewById<Button>(Resource.Id.btnLogin);
            btncreate = FindViewById<Button>(Resource.Id.btnRegistration);
            txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            btnsign.Click += Btnsign_Click;
            btncreate.Click += Btncreate_Click;
          
        }
        private void Btncreate_Click(object sender, EventArgs e)
        {
               StartActivity(typeof(RegisterActivity));
        }
        private void Btnsign_Click(object sender, EventArgs e)
        {
          
            try
            {
               db = new DataBase();
               db.createDataBase();
               var data = db.selectTableUsers();// Table<User>(); //Call Table  
               var data1 = data.Where(x => x.Email == txtEmail.Text && x.Password == txtPassword.Text).FirstOrDefault(); //Linq Query  
               if (data1 != null)
               {
                    Toast.MakeText(this, "User Login Success", ToastLength.Short).Show();
                    StartActivity(typeof(UserActivity));
               }
               else
               {
                    Toast.MakeText(this, "Username or Password invalid", ToastLength.Short).Show();
               }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
       
    }
}
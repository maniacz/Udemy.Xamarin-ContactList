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
using Newtonsoft.Json;


namespace ContactList.Droid
{
    [Activity(Label = "AddActivity")]
    public class AddActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddContact);

            var addButton = FindViewById<Button>(Resource.Id.addButton);

            var nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            var emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            var phoneNumberEditText = FindViewById<EditText>(Resource.Id.phoneNumberEditText);

            addButton.Click += delegate 
            {
                var newContact = new Contact(nameEditText.Text, emailEditText.Text, phoneNumberEditText.Text);

                var intent = new Intent();
                intent.PutExtra(MainActivity.NEW_CONTACT_KEY, JsonConvert.SerializeObject(newContact));
                SetResult(Result.Ok, intent);
                Finish();
            };
        }
    }
}
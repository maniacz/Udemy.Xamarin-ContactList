using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace ContactList.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static string NEW_CONTACT_KEY = "NEW_CONTACT_KEY";
        private static int ADD_CONTACT_REQUEST_CODE = 200;

        private List<Contact> contacts;
        private ContactListAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Button button = FindViewById<Button>(Resource.Id.addButton);
            button.Click += delegate 
            {
                StartActivityForResult(typeof(AddActivity), ADD_CONTACT_REQUEST_CODE);
            };

            Initialize();

            var contactListView = FindViewById<ListView>(Resource.Id.contactListView);
            adapter = new ContactListAdapter(contacts, this);
            contactListView.Adapter = adapter;
            contactListView.ItemLongClick += ContactListView_ItemLongClick;
        }

        private void ContactListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs args)
        {
            var contact = contacts[args.Position];

            var alert = new Android.App.AlertDialog.Builder(this).Create();

            alert.SetTitle("Delete Contact");
            alert.SetMessage(string.Format("Are you sure you want to delete {0}", contact.Name));
            alert.SetButton("Yes", delegate
            {
                contacts.Remove(contact);
                adapter.NotifyDataSetChanged();
            });

            alert.SetButton2("No", delegate { });

            alert.Show();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == ADD_CONTACT_REQUEST_CODE && data != null)
            {
                var newContact = JsonConvert.DeserializeObject<Contact>(data.GetStringExtra(NEW_CONTACT_KEY));

                contacts.Add(newContact);
                adapter.NotifyDataSetChanged();
            }
        }
        private void Initialize()
        {
            contacts = new List<Contact>();

            for (int i = 1; i <= 20; i++)
            {
                contacts.Add(new Contact("My Contact " + i,
                                        "contact_email@gmail.com",
                                        "+48 123 123 123"));

            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}


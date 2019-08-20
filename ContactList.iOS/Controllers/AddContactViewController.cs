using Foundation;
using System;
using UIKit;

namespace ContactList.iOS
{
    public partial class AddContactViewController : UIViewController
    {
        public AddContactViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            AddButton.TouchUpInside += delegate
            {
                var newContact = new Contact(NameTextField.Text, EmailTextField.Text, PhoneNumberTextField.Text);

                NavigationController.PopViewController(true);
                (NavigationController.TopViewController as MainViewController).AddContact(newContact);
            };
        }

    }
}
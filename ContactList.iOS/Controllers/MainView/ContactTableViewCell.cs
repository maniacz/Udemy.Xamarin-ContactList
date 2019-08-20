using System;

using Foundation;
using UIKit;

namespace ContactList.iOS.Controllers.MainView
{
    public partial class ContactTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ContactTableViewCell");
        public static readonly UINib Nib;

        static ContactTableViewCell()
        {
            Nib = UINib.FromName("ContactTableViewCell", NSBundle.MainBundle);
        }

        protected ContactTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void Initialize(Contact contact)
        {
            NameLabel.Text = contact.Name;
            PhoneNumberLabel.Text = contact.PhoneNumber;
        }
    }
}

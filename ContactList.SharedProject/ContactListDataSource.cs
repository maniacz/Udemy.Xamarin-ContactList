using System;
using System.Collections.Generic;
using System.Text;

namespace ContactList.SharedProject
{
    public static class ContactListDataSource
    {
        public static List<Contact> GetContacts()
        {
            var contacts = new List<Contact>();

            for (int i = 1; i <= 20; i++)
            {
                contacts.Add(new Contact("My Contact " + i,
                                        "contact_email@gmail.com",
                                        "+48 123 123 123"));

            }

            return contacts;
        }
    }
}

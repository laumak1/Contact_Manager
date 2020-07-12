using System.Collections.Generic;
using System.Linq;

namespace ContactManager
{
    public class ContactList
    {
        public List<Contact> Contacts { get; set; }

        public void AddContact(Contact contact)
        {
            Contacts.Add(contact);
        }
        public void DeleteContact(int contactNo)
        {
            Contacts.RemoveAt(contactNo);
        }
        public void UpdateContact(int contactNo, Contact contact)
        {
            Contacts.RemoveAt(contactNo);
            Contacts.Add(contact);
        }
        public Contact GetContact(int contactNo)
        {
            return Contacts.ElementAtOrDefault(contactNo);
        }
        public List<Contact> GetAllContacts()
        {
            return Contacts;
        }
        public int GetContactsCount()
        {
            return Contacts.Count;
        }

        public List<string> GetAllContactsTable()
        {
            int contactNo = 1;
            List<string> result = new List<string>
            {
                string.Format("|No.{0,1}|Name{0,11}|Last Name{0,6}|Phone Number{0,3}|Address{0,8}|", "")
            };
            foreach (var contact in Contacts)
            {
                result.Add(
                    string.Format("|{0,3}|{1,15}|{2,15}|{3,15}|{4,15}|",
                    contactNo++,
                    contact.Name,
                    contact.LastName,
                    contact.PhoneNumber,
                    contact.Address
                    ));
            }
            return result;
        }

        public bool PhoneNumberExists(string number)
        {
            return Contacts.Where(x => x.PhoneNumber == number).Count() > 0;
        }

    }
}
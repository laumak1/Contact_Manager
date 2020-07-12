using System;
using System.Text.Json;

namespace ContactManager
{
    class Program
    {
        public static ContactList ContactList;

        static void Main(string[] args)
        {
            ContactList = JsonSerializer.Deserialize<ContactList>(ReadData());

            while (true)
            {
                CommandParser();
            }
        }


        public static void CommandParser()
        {
            Console.WriteLine("\nChoose command: Add, Update, Delete, All");
            Console.Write("> ");
            string command = Console.ReadLine().ToLower();

            switch (command)
            {
                case "add":
                    Console.WriteLine("Enter Name:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter LastName:");
                    string lastName = Console.ReadLine();
                    Console.WriteLine("Enter Phone:");
                    string phone = Console.ReadLine();
                    Console.WriteLine("Enter Address:");
                    string address = Console.ReadLine();
                    if (ContactList.PhoneNumberExists(phone))
                    {
                        Console.WriteLine("This {0} phone number already exists", phone);
                        break;
                    }
                    Contact newContact = new Contact
                    {
                        Name = name,
                        LastName = lastName,
                        PhoneNumber = phone,
                        Address = address
                    };
                    var contactErrors = newContact.ValidateContact();
                    if (contactErrors.Count > 0)
                    {
                        foreach (var error in contactErrors)
                        {
                            Console.WriteLine(error);
                        }
                        break;
                    }
                    ContactList.AddContact(newContact);
                    SaveData();
                    Console.WriteLine("Item added");
                    break;

                case "update":
                    Console.WriteLine("Enter contact No. to update");
                    string contactNo = Console.ReadLine();
                    int _contactNo;
                    bool success = Int32.TryParse(contactNo, out _contactNo);
                    if (!success)
                    {
                        Console.WriteLine("Contact No. invalid");
                        break;
                    }
                    if (_contactNo > ContactList.GetContactsCount()
                        || _contactNo < 1)
                    {
                        Console.WriteLine("There is no contact with this No.");
                        break;
                    }
                    _contactNo--;
                    var contact = ContactList.GetContact(_contactNo);
                    Console.WriteLine("Enter new contact Name. Current is: {0}", contact.Name);
                    name = Console.ReadLine();
                    Console.WriteLine("Enter new contact LastName. Current is: {0}", contact.LastName);
                    lastName = Console.ReadLine();
                    Console.WriteLine("Enter new contact Phone. Current is: {0}", contact.PhoneNumber);
                    phone = Console.ReadLine();
                    Console.WriteLine("Enter new contact Address. Current is: {0}", contact.Address);
                    address = Console.ReadLine();
                    if (ContactList.PhoneNumberExists(phone))
                    {
                        Console.WriteLine("This {0} phone number already exists", phone);
                        break;
                    }
                    newContact = new Contact
                    {
                        Name = name,
                        LastName = lastName,
                        PhoneNumber = phone,
                        Address = address
                    };
                    contactErrors = newContact.ValidateContact();
                    if (contactErrors.Count > 0)
                    {
                        foreach (var error in contactErrors)
                        {
                            Console.WriteLine(error);
                        }
                        break;
                    }
                    ContactList.UpdateContact(_contactNo, newContact);
                    SaveData();
                    Console.WriteLine("Item updated");
                    break;

                case "delete":
                    Console.WriteLine("Enter contact No. to delete");
                    contactNo = Console.ReadLine();
                    //_contactNo;
                    success = Int32.TryParse(contactNo, out _contactNo);
                    if (!success)
                    {
                        Console.WriteLine("Contact No. invalid");
                        break;
                    }
                    if (_contactNo > ContactList.GetContactsCount()
                        || _contactNo < 1)
                    {
                        Console.WriteLine("There is no contact with this No.");
                        break;
                    }
                    _contactNo--;
                    Console.WriteLine("Are you sure want to delete this contact?(Y/N)");
                    Console.WriteLine(ContactList.GetContact(_contactNo).ToString());
                    string agreement = Console.ReadLine().ToLower();
                    if (agreement == "y")
                    {
                        ContactList.DeleteContact(_contactNo);
                        SaveData();
                        Console.WriteLine("Item was deleted");
                    }
                    else
                    {
                        Console.WriteLine("Item was not deleted");
                    }
                    break;

                case "all":
                    foreach (var line in ContactList.GetAllContactsTable())
                    {
                        Console.WriteLine(line);
                    }
                    break;

                default:
                    Console.WriteLine("No such command as " + command);
                    break;
            }
        }

        public static string ReadData()
        {
            string text = System.IO.File.ReadAllText(@"..//..//..//Contacts.Json");

            return text;
        }

        public static void WriteData(string text)
        {
            System.IO.File.WriteAllText(@"..//..//..//Contacts.Json", text);
        }

        public static void SaveData()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string contacts = JsonSerializer.Serialize(ContactList, options);
            WriteData(contacts);
        }

    }
}
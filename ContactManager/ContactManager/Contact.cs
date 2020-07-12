using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ContactManager
{
    public class Contact
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return string.Format(" {0} {1} {2} {3} ", Name, LastName, PhoneNumber, Address);
        }

        public List<string> ValidateContact()
        {
            List<string> result = new List<string>();
            if (Name.Length == 0)
            {
                result.Add("*Name is required");
            }
            if (LastName.Length == 0)
            {
                result.Add("*Last name is required");
            }
            if (PhoneNumber.Length == 0)
            {
                result.Add("*Phone is required");
            }
            if (!Regex.IsMatch(PhoneNumber, @"^\+?[0-9]+$"))
            {
                result.Add("*Phone number is invalid");
            }

            return result;
        }

    }
}
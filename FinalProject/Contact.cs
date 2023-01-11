using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{

    public class Contact
    {
        private String name;
        private String phoneNumber;
        private DateTime birth;

        public Contact(String name, String phoneNumber, DateTime birth)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.birth = birth;
        }


        public static Contact createContact(String name, String phoneNumber, DateTime birth)
        {
            return new Contact(name, phoneNumber, birth);
        }
        public DateTime Birth
        {
            get { return birth; }
        }

        public String Name
        {
            get { return name; }
        }

        public String PhoneNumber
        {
            get { return phoneNumber; }
        }
    }
}

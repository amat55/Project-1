using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace FinalProject
{
    public class MobilePhone
    {
        private string phoneNumber;
        private List<Contact> myContact;


        public MobilePhone(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
            this.myContact = new List<Contact>();
        }

        public bool addNewContact(Contact contact)
        {
            if (findContact(contact.Name) >= 0)
            {
                Console.WriteLine("Contact is already on file");
                return false;
            }
            myContact.Add(contact);
            return true;

        }

        public bool updateContact(Contact oldContact, Contact newContact)
        {
            int foundPosition = findContact(oldContact);
            if (foundPosition < 0)
            {
                Console.WriteLine(oldContact.Name + " , wasn't found");
                return false;
            }
            else if (findContact(newContact.Name) != -1)
            {
                Console.WriteLine("Contact with name " + newContact.Name +
                        " already exists.Update wasn't successful");
                return false;
            }
            this.myContact.Insert(foundPosition, newContact);
            this.myContact.RemoveAt(findContact(oldContact));
            Console.WriteLine(oldContact.Name + " , was replaced with " + newContact.Name);
            return true;
        }

        public bool removeContact(Contact contact)
        {
            int foundPosition = findContact(contact);
            if (foundPosition < 0)
            {
                Console.WriteLine(contact.Name + " , wasn't found");
                return false;
            }
            this.myContact.RemoveAt(foundPosition);
            Console.WriteLine(contact.Name + " removed from contact list");
            return true;
        }

        private int findContact(Contact contact)
        {
            return this.myContact.IndexOf(contact);
        }

        public String queryContact(Contact contact)
        {
            if (findContact(contact) >= 0)
            {
                return contact.Name;
            }
            return null;
        }

        public Contact queryContact(String name)
        {
            int position = findContact(name);
            if (position >= 0)
            {
                return this.myContact.ElementAt(position);
            }
            return null;
        }

        public void printContacts()
        {
            Console.WriteLine("Contact list");
            for (int i = 0; i < this.myContact.Count(); i++)
            {
                Console.WriteLine((i + 1) + "." +
                        this.myContact.ElementAt(i).Name + " ->" +
                        this.myContact.ElementAt(i).PhoneNumber + " ->" +
                        this.myContact.ElementAt(i).Birth
                );

            }
        }

        public void saveContacts()
        {
            List<string> contactList = new List<string>();
            for (int i = 0; i < this.myContact.Count(); i++)
            {
                contactList.Add(this.myContact.ElementAt(i).Name + ',' + this.myContact.ElementAt(i).PhoneNumber + ',' + this.myContact.ElementAt(i).Birth);
            }
            string data = string.Join("\n", contactList);
            System.IO.File.WriteAllText(".\\contactData.txt", data);
        }

        public string[] contactNames()
        {
            string[] contactList = new string[this.myContact.Count()];
            for (int i = 0; i < this.myContact.Count(); i++)
            {
                contactList[i] = this.myContact.ElementAt(i).Name;
            }
            return contactList;
        }



        private int findContact(string contactName)
        {
            for (int i = 0; i < this.myContact.Count(); i++)
            {
                Contact contact = this.myContact.ElementAt(i);
                if (contact.Name.Equals(contactName))
                {
                    return i;
                }
            }
            return -1;
        }
    }

}

using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Program
    {
        private static MobilePhone mobilePhone = new MobilePhone("0101 1010 50");
        static void Main(string[] args)
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader("./contactData.txt");     
                line = sr.ReadLine();
                while (line != null)
                {
                    
                    String[] strlist = line.Split(','); //NAME / PHONE NUMBER / BIRTHDAY
                    CultureInfo cc = new CultureInfo("pl-PL");
                    Thread.CurrentThread.CurrentCulture = cc;
                    String name = strlist[0];
                    String number = strlist[1];
                    DateTime birth = new DateTime();
                    //string b = Console.ReadLine();
                    //string a = b.Substring(3, 2) + "/" + b.Substring(0, 2) + "/" + b.Substring(6, 4);
                    birth = DateTime.Parse(strlist[2]);
                    Contact newContact = Contact.createContact(name, number, birth);
                    mobilePhone.addNewContact(newContact);
                    line = sr.ReadLine();
                }
           
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("All contacts successfully exported from datas.txt file.");
            }
            bool quit = false;
            startPhone();
            printActions();
            while (!quit)
            {
                Console.WriteLine("\nEnter action: ");
                int action = Convert.ToInt32(Console.ReadLine());

                switch (action)
                {
                    case 0:
                        Console.WriteLine("\nShutting Down...");
                        mobilePhone.saveContacts();
                        quit = true;
                        break;
                    case 1:
                        mobilePhone.printContacts();
                        break;
                    case 2:
                        addNewContact();
                        break;
                    case 3:
                        updateContact();
                        break;
                    case 4:
                        removeContact();
                        break;
                    case 5:
                        queryContact();
                        break;
                    case 6:
                        printActions();
                        break;
                    case 7:
                        ShowBirthdays();
                        break;

                }
                
            }
            
            
        }

        private static void ShowBirthdays()
        {
            string[] existingContacts = mobilePhone.contactNames();
            string[] currentBirthdays = new string[existingContacts.Length];
            int cnt = 0;
            for (int i = 0; i < existingContacts.Length; i++)
            {
                Contact existingContactRecord = mobilePhone.queryContact(existingContacts[i]);
                if (isBirthWeek(existingContactRecord.Birth))
                {
                    currentBirthdays[cnt++] = existingContacts[i];
                }
            }

            for (int j = 0; j < currentBirthdays.Length; j++)
            {
                Console.WriteLine(currentBirthdays[j]);
            }

        }

        //gun ay yil
        public static void addNewContact()
        {
            CultureInfo cc = new CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentCulture = cc;
            Console.WriteLine("Enter contact name: ");
            String name = Console.ReadLine();
            Console.WriteLine("Enter contact number: ");
            String number = Console.ReadLine();
            Console.WriteLine("Enter your Birthday");

            DateTime birth = new DateTime();
            //string b = Console.ReadLine();
            //string a = b.Substring(3, 2) + "/" + b.Substring(0, 2) + "/" + b.Substring(6, 4);
            birth = DateTime.Parse(Console.ReadLine());

            // birth = DateTime.Parse(Console.ReadLine());

            Contact newContact = Contact.createContact(name, number, birth);
            if (mobilePhone.addNewContact(newContact))
            {
                Console.WriteLine("New contact added: name= " + name + " , phone:" + number + " BirthDay: " + birth);
            }
            else
                Console.WriteLine("Contact " + name + " already in file");
        }

        public static void updateContact()
        {
            Console.WriteLine("Enter existing contact name: ");
            String name = Console.ReadLine();
            Contact existingContactRecord = mobilePhone.queryContact(name);
            if (existingContactRecord == null)
            {
                Console.WriteLine("Contact not found!");
                return;
            }
            Console.WriteLine("Enter new contact name: ");
            String newName = Console.ReadLine();
            Console.WriteLine("Enter new phone number: ");
            String newNumber = Console.ReadLine();
            Console.WriteLine("Enter new birthday: ");
            DateTime birth = new DateTime();
            birth = DateTime.Parse(Console.ReadLine());
            Contact newContact = Contact.createContact(newName, newNumber, birth);
            if (mobilePhone.updateContact(existingContactRecord, newContact))
            {
                Console.WriteLine("Successfully updated record");

            }
            else
                Console.WriteLine("Error updating recording");
        }

        public static void removeContact()
        {
            Console.WriteLine("Enter existing contact name: ");
            String name = Console.ReadLine();
            Contact existingContactRecord = mobilePhone.queryContact(name);
            if (existingContactRecord == null)
            {
                Console.WriteLine("Contact not found");
                return;
            }
            if (mobilePhone.removeContact(existingContactRecord))
            {
                Console.WriteLine("Successfully deleted");
            }
            else
                Console.WriteLine("Error deleting contact");
        }


        public static void queryContact()
        {
            Console.WriteLine("Enter existing contact name:  ");
            String name = Console.ReadLine();
            Contact existingContactRecord = mobilePhone.queryContact(name);
            if (existingContactRecord == null)
            {
                Console.WriteLine("Contact not found");
                return;
            }
            string weekString = isBirthWeek(existingContactRecord.Birth) ? "this week." : "not this week.";
            Console.WriteLine("Name: " + existingContactRecord.Name + " Number: "
                + existingContactRecord.PhoneNumber + " Current BirthDay is : " + existingContactRecord.Birth + " and it's " + weekString);

        }

        public static bool isBirthWeek(DateTime birthDate)
        {
            DateTime dtStart = DateTime.Today;


            while (dtStart.DayOfWeek != DayOfWeek.Monday)
            {
                dtStart = dtStart.AddDays(-1);
            }

            DateTime dtEnd = dtStart.AddDays(7);

            //int dayStart = dtStart.DayOfYear;
            //int dayEnd = dtEnd.DayOfYear;
            //int birthDay = birthDate.DayOfYear;
            DateTime dt2 = new DateTime(dtStart.Year, birthDate.Month, birthDate.Day);
            // return (birthDay >= dayStart && birthDay < dayEnd);
            return (dt2 >= dtStart && dt2 < dtEnd);

        }

        public static void startPhone()
        {
            Console.WriteLine("Phone is starting...");
        }

        public static void printActions()
        {
            Console.WriteLine("\nAvailable Actions\n");
            Console.WriteLine("\n0 - shutdown\n" +
                    "1 - print Contacts\n" +
                    "2 - to add a new contact\n" +
                    "3 - to update an existing contact\n" +
                    "4 - to remove an existing contact\n" +
                    "5 - query if an existing contact exist\n" +
                    "6 - to print a list of available actions\n" +
                    "7 - to print the contacts who have birthdays this week");
            Console.WriteLine("Choose your action: ");
        }

    }
}


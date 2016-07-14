using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Survey
{
    /// <summary>
    /// HouseHold class to read data from file and hold them
    /// </summary>
    public class HouseHold : IHouseHold
    {
        private string lastName;
        private string firstName;
        private string address;
        private string city;
        private string state;
        private int age;

        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                this.lastName = value;
            }
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                this.firstName = value;
            }
        }

        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }

        public string City
        {
            get
            {
                return this.city;
            }
            set
            {
                this.city = value;
            }
        }

        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }
            set
            {
                this.age = value;
            }
        }

        /// <summary>
        /// Constructor takes input text (each record line) from the file 
        ///  and parses them to store separate values such as firstname, lastname, etc.
        /// </summary>
        /// <param name="text">Each line from the input file</param>
        public HouseHold(string text)
        {
            string[] entry =  new string[10];
            int i = 0;
            String pattern = @"[""""]";
            Regex regex = new Regex(pattern);
            string[] substrs = regex.Split(text);
            foreach(string str in substrs)
            {
                if (!string.IsNullOrEmpty(str) && str != ",")
                {
                    entry[i++] = str;
                }
            }

            this.FirstName = entry[0];
            this.LastName = entry[1];
            this.Address = entry[2];
            this.City = entry[3];
            this.State = entry[4];
            int age = 0;
            if (int.TryParse(entry[5], out age))
            {
                this.Age = age;
            }
        }
    }
}

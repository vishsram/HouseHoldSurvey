using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace Survey
{
    /// <summary>
    /// Utility class to create data tables and query results
    /// </summary>
    public class DBUtil
    {
        /// <summary>
        /// A dictionary consisting of HouseHold entries with Lastname as key
        /// </summary>
        Dictionary<string, List<HouseHold>> houseHoldEntries;

        /// <summary>
        /// A data table to store house hold entries read from file and query them
        /// </summary>
        DataTable houseHoldTable;

        /// <summary>
        /// DBUtil constructor to create data table and columns associated with it including the dictionary
        /// </summary>
        public DBUtil()
        {
            houseHoldEntries = new Dictionary<string, List<HouseHold>>();
            houseHoldTable = new DataTable("People");
            DataColumn ln = new DataColumn("LastName", typeof(string));
            DataColumn fn = new DataColumn("FirstName", typeof(string));
            DataColumn add = new DataColumn("Address", typeof(string));
            DataColumn city = new DataColumn("City", typeof(string));
            DataColumn state = new DataColumn("State", typeof(string));
            DataColumn age = new DataColumn("Age", typeof(int));

            houseHoldTable.Columns.Add(ln);
            houseHoldTable.Columns.Add(fn);
            houseHoldTable.Columns.Add(add);
            houseHoldTable.Columns.Add(city);
            houseHoldTable.Columns.Add(state);
            houseHoldTable.Columns.Add(age);

        }

        /// <summary>
        /// Parse the text file and read the data into data table
        /// </summary>
        /// <param name="path">file path</param>
        public void ParseInput(string path)
        {
            string text = string.Empty;

            // Check if the file exists and that its a text file
            if (File.Exists(path) && path.ToLower().EndsWith(".txt"))
            {
                using (var streamReader = new StreamReader(path, Encoding.UTF8))
                {
                    // While there is data left in the file
                    while (streamReader.Peek() >= 0)
                    {
                        // Read each line
                        text = streamReader.ReadLine();

                        // Create a new household object to hold the data
                        HouseHold hh = new HouseHold(text);

                        // Add the household entry into dictionary 
                        if (!string.IsNullOrEmpty(hh.LastName))
                        {
                            if (houseHoldEntries.ContainsKey(hh.LastName))
                            {
                                houseHoldEntries[hh.LastName].Add(hh);
                            }
                            else
                            {
                                houseHoldEntries.Add(hh.LastName, new List<HouseHold>());
                                houseHoldEntries[hh.LastName].Add(hh);
                            }
                        }

                        // Add data into data table
                        DataRow dr = houseHoldTable.NewRow();
                        dr["LastName"] = hh.LastName;
                        dr["FirstName"] = hh.FirstName;
                        dr["Address"] = hh.Address;
                        dr["City"] = hh.City;
                        dr["State"] = hh.State;
                        dr["Age"] = hh.Age;
                        houseHoldTable.Rows.Add(dr);
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid File Name.");
            }
        }

        /// <summary>
        /// Query the data table and display values.
        /// </summary>
        public void DisplayEntries()
        {
            Console.WriteLine("\n\n No of members in each household:");
            Console.WriteLine("*******************************\n");
            foreach (var hh in houseHoldEntries.Values)
            {
                Console.WriteLine("Household: {0}     No. of members: {1}", hh[0].LastName, hh.Count);
            }

            var people = from row in houseHoldTable.AsEnumerable()
                         where row.Field<int>("Age") >= 18
                         orderby row.Field<string>("LastName"), row.Field<string>("FirstName")
                         select row;

            Console.WriteLine("\nMembers older than 18:");
            Console.WriteLine("**********************\n");

            foreach (var row in people)
            {
                string ln = row.Field<string>("LastName");
                string fn = row.Field<string>("FirstName");
                string add = row.Field<string>("Address");
                string city = row.Field<string>("City");
                string state = row.Field<string>("State");
                int age = row.Field<int>("Age");
                Console.WriteLine("{0}, {1}       {2}, {3}, {4},  {5}", ln, fn, add, city, state, age);
            }

        }
    }
}


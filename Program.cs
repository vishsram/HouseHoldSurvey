using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Survey
{
    class Program
    {
        static void Main(string[] args)
        {
            // Store the text file path 
            string path = args[0];

            // Create a new instance of DBUtil class
            DBUtil db = new DBUtil();

            // Parse the user given path & store values into db table
            db.ParseInput(path);

            // Query the db table and display results
            db.DisplayEntries();
        }
    }
}

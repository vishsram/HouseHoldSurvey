using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey
{
    public interface IHouseHold
    {
        string LastName
        {
            get;
            set;
        }

        string FirstName
        {
            get;
            set;
        }

        string Address
        {
            get;
            set;
        }

        string City
        {
            get;
            set;
        }

        string State
        {
            get;
            set;
        }

        int Age
        {
            get;
            set;
        }
    }
}
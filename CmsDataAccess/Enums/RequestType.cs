using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsDataAccess.Enums
{
    public  class RequestType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public static class RequestTypeEnum
    {

        static List<string> services = new List<string> { "Not specified", "Airline Reservations", "Hotel / Apartment Reservations", "Luxury Travel services", "Honeymoon packages", "Tour Packages" };


        public static List<RequestType> RequestTypes
        {
            get
            {
                return  new List<RequestType>
                {
                    new RequestType{ Id = 0,Name=services[0]},
                    new RequestType{ Id = 1,Name=services[1]},
                    new RequestType{ Id = 2,Name=services[2]},
                    new RequestType{ Id = 3,Name=services[3]},
                    new RequestType{ Id = 4,Name=services[4]},
                    new RequestType{ Id = 4,Name=services[5]},
                };  

            }
        }

    }


    public class JobType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public static class JobTypeEnum
    {

        static List<string> services = new List<string> { "Admin", "Sales Analyst", "Customer Support" };

        public static List<JobType> JobTypes
        {
            get
            {
                return new List<JobType>
                {
                    new JobType{ Id = 0,Name=services[0]},
                    new JobType{ Id = 1,Name=services[1]},
                    new JobType{ Id = 2,Name=services[2]},
                };

            }
        }

    }

}

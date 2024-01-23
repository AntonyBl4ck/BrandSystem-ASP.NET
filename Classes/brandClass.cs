using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnilneBrandingSystem.Classes
{
    public class brandClass
    {
        public int brand_id { get; set; }
        public string brand_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string contact { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string keywords { get; set; }
        public string image { get; set; }
        public string category { get; set; }
        public DateTime addedDate { get; set; }

    }
}
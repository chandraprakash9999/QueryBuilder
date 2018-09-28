using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryBuilder.Models
{
    public class EmployeeQuery
    {
        public string column { get; set; }
        public string condition { get; set; }
        public string value { get; set; }
        public string andor { get; set; }
        public string finalquery { get; set; }

    }
   
    public class Columns
    {
        public string columnname { get; set; }
        public string columnvalue { get; set; }

    }

    public class Categories
    {
        public string id { get; set; }
        public string name { get; set; }

    }
    public class Seeds
    {
        public string id { get; set; }
        public string name { get; set; }
        public string categoryId { get; set; }
        public string imageUrl { get; set; }

    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
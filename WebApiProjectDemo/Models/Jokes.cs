using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace WebApiProjectDemo.Models
{
    public class Jokes
    {
        public string Title { get; set; }
        public string Quote { get; set; }
        public string Date { get; set; }

    }
}
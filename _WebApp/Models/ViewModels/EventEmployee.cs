using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Client.Models;
using _WebApp.Infrastructure;

namespace _WebApp.Models.ViewModels
{
    public class EventEmployee
    {

        public Employee Employee { get; set; }
        public IEnumerable<EmployeeAdresse> ListEmp { get; set; }
        public Event Eve { get; set; }
        public AdresseVille EventAdresse { get; set; } = new AdresseVille();

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Client.Models;

namespace _WebApp.Models.ViewModels
{
    public class MemberDiscussion
    {
        public Employee employee { get; set; }
        public Adresse adresse { get; set; }
        public Ville ville { get; set; }
        
        public IEnumerable<MessageEmployee> ListeMessageEmployees { get; set; }
    }
}
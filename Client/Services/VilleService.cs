﻿using Client.Mappers;
using Client.Models;
using DS = DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace Client.Services {
    public class VilleService {

        private DS.VilleService service;

        public VilleService() {
            service = new DS.VilleService();
        }

        public IEnumerable<Ville> GetAll() {
            return service.GetAll().Select(a => a.ToClient());
        }

        public Ville GetById(int id) {
            return service.GetById(id).ToClient();
        }

        public Ville GetByNomZipPays(string nom, string zip, int id) {
            return service.GetByNomZipPays(nom, zip, id).ToClient();
        }

        public IEnumerable<Ville> GetByZip(string zip)
        {
            return service.GetByZip(zip).Select(a => a.ToClient());
        }
    }
}

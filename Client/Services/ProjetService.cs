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
    public class ProjetService {

        private DS.ProjetService service;

        public ProjetService() {
            service = new DS.ProjetService();
        }

        public IEnumerable<Projet> GetAll() {
            return service.GetAll().Select(a => a.ToClient());
        }

        public IEnumerable<Projet> GetByIdEmpl(int id) {
            return service.GetByIdEmpl(id).Select(a => a.ToClient());
        }

        public Projet GetById(int id) {
            return service.GetById(id).ToClient();
        }

        public Projet GetByManager(int id) {
            return service.GetByManager(id).ToClient();
        }

        public Projet Insert(Projet p, int idemp) {
            return service.Insert(p.ToDal(), idemp).ToClient();
        }

        public bool AffecterEquipe(List<int> idList, int idProj) {
            return service.AffecterEquipe(idList, idProj);
        }

        public bool Update(Projet p) {
            return service.Update(p.ToDal());
        }
    }
}

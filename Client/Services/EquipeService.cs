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
    public class EquipeService {

        private DS.EquipeService service;

        public EquipeService() {
            service = new DS.EquipeService();
        }

        public IEnumerable<Equipe> GetAll() {
            return service.GetAll().Select(a => a.ToClient());
        }

        public IEnumerable<Equipe> GetByProjet(int id) {
            return service.GetByProjet(id).Select(a => a.ToClient());
        }

        public IEnumerable<Equipe> GetOtherByProjet(int id) {
            return service.GetOtherByProjet(id).Select(a => a.ToClient());
        }

        public Equipe GetById(int id) {
            return service.GetById(id).ToClient();
        }

        public Equipe GetByEmployee(int id) {
            return service.GetByEmployee(id).ToClient();
        }

        public Equipe Insert(Equipe a) {
            return service.Insert(a.ToDal()).ToClient();
        }

        public bool AffecterEmployee(List<int> idsEmp, int idEq) {
            return service.AffecterEmployee(idsEmp, idEq);
        }

        public bool RemoveEmployee(int emp) {
            return service.RemoveEmployee(emp);
        }

        public bool Update(Equipe a) {
            return service.Update(a.ToDal());
        }

        public bool Delete(int id) {
            return service.Delete(id);
        }
    }
}

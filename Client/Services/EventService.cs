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
    public class EventService {

        private DS.EventService service;

        public EventService() {
            service = new DS.EventService();
        }

        public IEnumerable<Event> GetAll() {
            return service.GetAll().Select(a => a.ToClient());
        }

        public Event GetById(int id) {
            return service.GetById(id).ToClient();
        }
        
        public Event Insert(Event a) {
            return service.Insert(a.ToDal()).ToClient();
        }

        public bool Participe(int Emp, int Eve) {
            return service.Participe(Emp, Eve);
        }
        public bool ParticipeN(int Emp, int Eve) {
            return service.ParticipeN(Emp, Eve);
        }
        public bool Update(Event a) {
            return service.Update(a.ToDal());
        }

        public bool Delete(int id) {
            return service.Delete(id);
        }
    }
}

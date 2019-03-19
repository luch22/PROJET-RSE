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
    public class MessageEquipeService {

        private DS.MessageEquipeService service;

        public MessageEquipeService() {
            service = new DS.MessageEquipeService();
        }

        public IEnumerable<MessageEquipe> GetAll() {
            return service.GetAll().Select(a => a.ToClient());
        }

        public IEnumerable<MessageEquipe> GetMessageBySujet(int id) {
            return service.GetMessageBySujet(id).Select(a => a.ToClient());
        }

        public IEnumerable<MessageEquipe> GetSujetByEquipe(int id) {
            return service.GetSujetByEquipe(id).Select(a => a.ToClient());
        }

        public MessageEquipe GetById(int id) {
            return service.GetById(id).ToClient();
        }

        public MessageEquipe Insert(MessageEquipe me) {
            return service.Insert(me.ToDal()).ToClient();
        }

        public bool Delete(int id) {
            return service.Delete(id);
        }
    }
}

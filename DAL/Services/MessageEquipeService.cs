﻿using DAL.Models;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox;
using System.Configuration;

namespace DAL.Services {
    public class MessageEquipeService {

        private readonly string providerName = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ProviderName;
        private readonly string connString = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ConnectionString;

        public IEnumerable<MessageEquipe> GetAll() {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Message_Equipe;");

            return connection.ExecuteReader(command, (dr) => dr.ToMessageEquipe());
        }

        public IEnumerable<MessageEquipe> GetMessageBySujet(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_MessageBySujetEquipe @id = @i;");
            command.AddParameter("i", id);

            return connection.ExecuteReader(command, (dr) => dr.ToMessageEquipe());
        }

        public IEnumerable<MessageEquipe> GetSujetByEquipe(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_SujetByEquipe @id = @i;");
            command.AddParameter("i", id);

            return connection.ExecuteReader(command, (dr) => dr.ToMessageEquipe());
        }

        public MessageEquipe GetById(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Message_Equipe WHERE Id_Message_Equipe = @id;");
            command.AddParameter("id", id);

            return connection.ExecuteReader(command, (dr) => dr.ToMessageEquipe()).SingleOrDefault();
        }

        public MessageEquipe Insert(MessageEquipe me) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_InsertMsgEquipe @titre = @ti, @date = @da, @texte = @txt, @idmsg = @im, @idemp = @ie, @ideq = @ide;");
            command.AddParameter("ti", me.Titre);
            command.AddParameter("da", me.Date);
            command.AddParameter("txt", me.Contenu);
            command.AddParameter("im", me.MessagePrecedent);
            command.AddParameter("ide", me.Id_Equipe);
            command.AddParameter("ie", me.Id_Employee);

            me.Id = (int)connection.ExecuteScalar(command);

            return me;
        }

        public bool Delete(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("DELETE FROM Message_Equipe WHERE Id_Message_Equipe = @id;");
            command.AddParameter("id", id);

            return connection.ExecuteNonQuery(command) == 1;
        }
    }
}

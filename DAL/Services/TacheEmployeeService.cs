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
    public class TacheEmployeeService {

        private readonly string providerName = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ProviderName;
        private readonly string connString = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ConnectionString;

        public IEnumerable<TacheEmployee> GetAll() {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Tache_Employee;");

            return connection.ExecuteReader(command, (dr) => dr.ToTacheEmployee());
        }

        public IEnumerable<TacheEmployee> GetByEmployee(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_TacheDEmployee @id = @i;");
            command.AddParameter("i", id);

            return connection.ExecuteReader(command, (dr) => dr.ToTacheEmployee());
        }

        public TacheEmployee GetById(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Tache_Employee WHERE Id_Tache_Employee = @id;");
            command.AddParameter("id", id);

            return connection.ExecuteReader(command, (dr) => dr.ToTacheEmployee()).SingleOrDefault();
        }

        public TacheEmployee Insert(TacheEmployee te, int emp) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_InsertTacheEmployee @nt = @nom, @d = @desc, @dd = @datedebut, @dfin = @datefin, @dfinal = @datefinal, @tp = @tprec, @ip = @idproj, @idemp = @idemploye;");
            command.AddParameter("nom", te.Nom);
            command.AddParameter("desc", te.Description);
            command.AddParameter("datedebut", te.Debut);
            command.AddParameter("datefin", te.Fin);
            command.AddParameter("datefinal", te.Final);
            command.AddParameter("tprec", te.TachePrecedente);
            command.AddParameter("idproj", te.Projet);
            command.AddParameter("idemploye", emp);

            te.Id = (int)connection.ExecuteScalar(command);

            return te;
        }

        public bool Update(TacheEmployee te) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("UPDATE Tache_Employee SET Nom_Tache = @nt, Description = @d, Date_Debut = @dd, Date_Fin = @dfin, Date_Final = @dfinal, Tache_Precedente = @tp, Id_Projet = @ip WHERE Id_Tache_Employee = @id;");
            command.AddParameter("nt", te.Nom);
            command.AddParameter("d", te.Description);
            command.AddParameter("dd", te.Debut);
            command.AddParameter("dfin", te.Fin);
            command.AddParameter("dfinal", te.Final);
            command.AddParameter("tp", te.TachePrecedente);
            command.AddParameter("ip", te.Projet);
            command.AddParameter("id", te.Id);

            return connection.ExecuteNonQuery(command) == 1;
        }

        public bool Delete(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("DELETE FROM Tache_Employee WHERE Id_Tache_Employee = @id;");
            command.AddParameter("id", id);

            return connection.ExecuteNonQuery(command) == 1;
        }
    }
}

using DAL.Models;
using DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox;
using System.Configuration;

namespace DAL.Services {
    public class DocumentService {

        private readonly string providerName = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ProviderName;
        private readonly string connString = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/_WebApp").ConnectionStrings.ConnectionStrings["SQLConnection"].ConnectionString;

        public IEnumerable<Document> GetAll() {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Document;");

            return connection.ExecuteReader(command, (dr) => dr.ToDocument());
        }

        public IEnumerable<Document> GetByDep(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_DocumentByDep @id = @i;");
            command.AddParameter("i", id);

            return connection.ExecuteReader(command, (dr) => dr.ToDocument());
        }

        public IEnumerable<Document> GetByProjet(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_DocumentByProjet @id = @i;");
            command.AddParameter("i", id);

            return connection.ExecuteReader(command, (dr) => dr.ToDocument());
        }
        
        public IEnumerable<Document> GetByEquipe(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_DocumentByEquipe @id = @i;");
            command.AddParameter("i", id);

            return connection.ExecuteReader(command, (dr) => dr.ToDocument());
        }

        public IEnumerable<Document> GetByTache(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("EXEC SP_DocumentByTache @id = @i;");
            command.AddParameter("i", id);

            return connection.ExecuteReader(command, (dr) => dr.ToDocument());
        }

        public Document GetById(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("SELECT * FROM Document WHERE Id_Document = @id;");
            command.AddParameter("id", id);

            return connection.ExecuteReader(command, (dr) => dr.ToDocumentDownload()).SingleOrDefault();
        }

        public Document Insert(Document d, string type, int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("INSERT INTO Document (Nom_Document, Description, Date, Contenu, Taille, Format, Id_Employee_Cree, Id_Employee_Maj) VALUES (@nd, @d, @da, @l, @t, @f, @ic, @im); SELECT CONVERT(int, @@IDENTITY);");
            command.AddParameter("nd", d.Nom);
            command.AddParameter("d", d.Description);
            command.AddParameter("da", d.Date);
            command.AddParameter("l", d.Contenu);
            command.AddParameter("t", d.Taille);
            command.AddParameter("f", d.Format);
            command.AddParameter("ic", d.Id_Emp_Creee);
            command.AddParameter("im", d.Id_Emp_Maj);

            d.Id = (int)connection.ExecuteScalar(command);

            Command cmd2 = null;
            switch (type) {
                case "dep":
                    cmd2 = new Command("INSERT INTO Type_Document VALUES (@i, NULL, NULL, NULL, @id, NULL, NULL);");
                    break;
                case "projet":
                    cmd2 = new Command("INSERT INTO Type_Document VALUES (@i, @id, NULL, NULL, NULL, NULL, NULL);");
                    break;
                case "equipe":
                    cmd2 = new Command("INSERT INTO Type_Document VALUES (@i, NULL, @id, NULL, NULL, NULL, NULL);");
                    break;
                case "event":
                    cmd2 = new Command("INSERT INTO Type_Document VALUES (@i, NULL, NULL, @id, NULL, NULL, NULL);");
                    break;
                case "tacheeq":
                    cmd2 = new Command("INSERT INTO Type_Document VALUES (@i, NULL, NULL, NULL, NULL, NULL, @id);");
                    break;
                case "tacheemp":
                    cmd2 = new Command("INSERT INTO Type_Document VALUES (@i, NULL, NULL, NULL, NULL, @id, NULL);");
                    break;
            }
            if (cmd2 != null) {
                cmd2.AddParameter("i", d.Id);
                cmd2.AddParameter("id", id);
                connection.ExecuteScalar(cmd2);
            }
            return d;
        }

        public bool Update(Document d) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("UPDATE Equipe SET Nom_Document = @nd, Description = @d, Date = @da, Contenu = @l, Taille = @t, Format = @f, Id_Employee_Cree = @ic, Id_Employee_Maj = @im WHERE Id_Document = @id;");
            command.AddParameter("nd", d.Nom);
            command.AddParameter("d", d.Description);
            command.AddParameter("da", d.Date);
            command.AddParameter("l", d.Contenu);
            command.AddParameter("t", d.Taille);
            command.AddParameter("f", d.Format);
            command.AddParameter("ic", d.Id_Emp_Creee);
            command.AddParameter("im", d.Id_Emp_Maj);
            command.AddParameter("id", d.Id);

            return connection.ExecuteNonQuery(command) == 1;
        }

        public bool Delete(int id) {
            Connection connection = new Connection(providerName, connString);
            Command command = new Command("DELETE FROM Document WHERE Id_Document = @id;");
            command.AddParameter("id", id);

            return connection.ExecuteNonQuery(command) == 1;
        }
    }
}

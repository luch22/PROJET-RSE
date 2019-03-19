﻿using C = Client.Models;
using D = DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Mappers
{
    /**********************************************************************************************************************
     *XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX*
     *X                                                        TO DAL                                                    X*
     *X                                                        TO DAL                                                    X*
     *X                                                        TO DAL                                                    X*
     *X                                                        TO DAL                                                    X*
     *X                                                        TO DAL                                                    X*
     *X                                                        TO DAL                                                    X*
     *XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX*
     **********************************************************************************************************************/

    internal static class Mappers
    {
        internal static D.Projet ToDal(this C.Projet e)
        {
            return new D.Projet()
            {
                Id = e.Id,
                Admin = e.Admin,
                Debut = e.Debut,
                Description = e.Description,
                Fin = e.Fin,
                Nom = e.Nom
            };
        }

        internal static D.Administrateur ToDal(this C.Administrateur e)
        {
            return new D.Administrateur()
            {
                Employee = e.Employee,
                Id = e.Id,
                NumeroAdmin = e.NumeroAdmin
            };
        }

        internal static D.Employee ToDal(this C.Employee e)
        {
            return new D.Employee()
            {
                Id = e.Id,
                Nom = e.Nom,
                Adresse = e.Adresse,
                Birthday = e.Birthday,
                Coordonnee = e.Coordonnee,
                Email = e.Email,
                HireDate = e.HireDate,
                Password = e.Password,
                Prenom = e.Prenom,
                RegNat = e.RegNat,
                Tel = e.Tel,
                Actif = e.Actif,
                Valide = e.Valide
            };
        }

        internal static D.Adresse ToDal(this C.Adresse e)
        {
            return new D.Adresse()
            {
                Boite_Postal = e.Boite_Postal,
                Id = e.Id,
                Id_Ville = e.Id_Ville,
                Numero = e.Numero,
                Nom_Rue = e.Nom_Rue,
            };
        }

        internal static D.Rue ToDal(this C.Rue e) {
            return new D.Rue() {
                Id_Ville = e.Id_Ville,
                Nom_Rue = e.Nom_Rue,
            };
        }

        internal static D.Coordonnee ToDal(this C.Coordonnee e)
        {
            return new D.Coordonnee()
            {
                Id = e.Id,
                Latitude = e.Latitude,
                Longitude = e.Longitude,
            };
        }

        internal static D.Departement ToDal(this C.Departement e)
        {
            return new D.Departement()
            {
                Admin = e.Admin,
                Description = e.Description,
                Id = e.Id,
                Nom = e.Nom
            };
        }

        internal static D.Document ToDal(this C.Document e)
        {
            return new D.Document()
            {
                Date = e.Date,
                Description = e.Description,
                Format = e.Format,
                Id = e.Id,
                Id_Emp_Creee = e.Id_Emp_Creee,
                Id_Emp_Maj = e.Id_Emp_Maj,
                Contenu = e.Contenu,
                Nom = e.Nom,
                Taille = e.Taille
            };
        }

        internal static D.Equipe ToDal(this C.Equipe e)
        {
            return new D.Equipe()
            {
                Creee = e.Creee,
                Id = e.Id,
                Nom = e.Nom,
                Projet = e.Projet
            };
        }

        internal static D.Event ToDal(this C.Event e)
        {
            return new D.Event()
            {
                DateDebut = e.DateDebut,
                Id = e.Id,
                Nom = e.Nom,
                Description = e.Description,
                Id_Employee = e.Id_Employee,
                Lieu = e.Lieu,
                DateFin = e.DateFin,
                FullDay = e.FullDay
            };
        }

        internal static D.MessageEmployee ToDal(this C.MessageEmployee e)
        {
            return new D.MessageEmployee()
            {
                Contenu = e.Contenu,
                Id = e.Id,
                Id_Employee = e.Id_Employee,
                Date = e.Date,
                MessagePrecedent = e.MessagePrecedent,
                Id_Destinataire = e.Id_Destinataire,
                Titre = e.Titre
            };
        }

        internal static D.MessageEquipe ToDal(this C.MessageEquipe e)
        {
            return new D.MessageEquipe()
            {
                Contenu = e.Contenu,
                Id = e.Id,
                Id_Equipe = e.Id_Equipe,
                Date = e.Date,
                MessagePrecedent = e.MessagePrecedent,
                Titre = e.Titre,
                Id_Employee = e.Id_Employee,
                Auteur = e.Auteur
            };
        }

        internal static D.MessageProjet ToDal(this C.MessageProjet e)
        {
            return new D.MessageProjet()
            {
                Contenu = e.Contenu,
                Id = e.Id,
                Id_Projet = e.Id_Projet,
                Date = e.Date,
                MessagePrecedent = e.MessagePrecedent,
                Titre = e.Titre,
                Id_Employee = e.Id_Employee,
                Auteur = e.Auteur

            };
        }
        internal static D.MessageTache ToDal(this C.MessageTache e)
        {
            return new D.MessageTache()
            {
                Contenu = e.Contenu,
                Id = e.Id,
                Date = e.Date,
                MessagePrecedent = e.MessagePrecedent,
                Titre = e.Titre,
                Id_Employee = e.Id_Employee,
                Id_Tache_Employee = e.Id_Tache_Employee,
                Id_Tache_Equipe = e.Id_Tache_Equipe,
                Auteur = e.Auteur

            };
        }

        internal static D.Pays ToDal(this C.Pays e)
        {
            return new D.Pays()
            {
                Id = e.Id,
                Nom_FR = e.Nom_FR,
                Nom_EN = e.Nom_EN,
                Code = e.Code,
                Alpha2 = e.Alpha2,
                Alpha3 = e.Alpha3
            };
        }

        internal static D.StatutEmployee ToDal(this C.StatutEmployee e)
        {
            return new D.StatutEmployee()
            {
                IdEmployee = e.IdEmployee,
                NomStatut = e.NomStatut,
                DateDebut = e.DateDebut,
                DateFin = e.DateFin
            };
        }
        internal static D.StatutTache ToDal(this C.StatutTache e)
        {
            return new D.StatutTache()
            {
                Id = e.Id,
                NomStatut = e.NomStatut,
            };
        }

        internal static D.TacheEmployee ToDal(this C.TacheEmployee e)
        {
            return new D.TacheEmployee()
            {
                Id = e.Id,
                Nom = e.Nom,
                Projet = e.Projet,
                Description = e.Description,
                Debut = e.Debut,
                Fin = e.Fin,
                Final = e.Final,
                TachePrecedente = e.TachePrecedente
            };
        }
        internal static D.TacheEquipe ToDal(this C.TacheEquipe e)
        {
            return new D.TacheEquipe()
            {
                Id = e.Id,
                Nom = e.Nom,
                Projet = e.Projet,
                Description = e.Description,
                Debut = e.Debut,
                Fin = e.Fin,
                Final = e.Final,
                TachePrecedente = e.TachePrecedente
            };
        }

        internal static D.Ville ToDal(this C.Ville e)
        {
            return new D.Ville()
            {
                Id = e.Id,
                Nom = e.Nom,
                Id_Pays = e.Id_Pays,
                Zip = e.Zip
            };
        }

        internal static D.Planning ToDal(this C.Planning p) {
            return new D.Planning() {
                Id = p.Id,
                Nom = p.Nom,
                Type = p.Type,
                DateDebut = p.DateDebut,
                DateFinal = p.DateFinal,
                FullDay = p.FullDay
            };
        }

        internal static D.Notification ToDal(this C.Notification p) {
            return new D.Notification() {
                Id = p.Id,
                Id_Employee = p.Id_Employee,
                Date = p.Date,
                Type = p.Type,
                Contenu = p.Contenu,
                Lu = p.Lu,
                Id_Lien = p.Id_Lien
            };
        }

        /**********************************************************************************************************************
         *XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX*
         *X                                                      TO CLIENT                                                   X*
         *X                                                      TO CLIENT                                                   X*
         *X                                                      TO CLIENT                                                   X*
         *X                                                      TO CLIENT                                                   X*
         *X                                                      TO CLIENT                                                   X*
         *X                                                      TO CLIENT                                                   X*
         *XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX*
         **********************************************************************************************************************/

        internal static C.Projet ToClient(this D.Projet e)
        {
            if (e != null)
                return new C.Projet(e.Id, e.Nom, e.Description, e.Debut, e.Fin, e.Admin);
            return null;
        }

        internal static C.Administrateur ToClient(this D.Administrateur e)
        {
            if (e != null)
                return new C.Administrateur(e.Id, e.NumeroAdmin, e.Employee);
            return null;
        }

        internal static C.Adresse ToClient(this D.Adresse e)
        {
            if (e != null)
                return new C.Adresse(e.Id, e.Nom_Rue, e.Numero, e.Boite_Postal, e.Id_Ville);
            return null;
        }

        internal static C.Rue ToClient(this D.Rue e) {
            if (e != null)
                return new C.Rue(e.Nom_Rue, e.Id_Ville);
            return null;
        }

        internal static C.Coordonnee ToClient(this D.Coordonnee e)
        {
            if (e != null)
                return new C.Coordonnee(e.Id,e.Longitude,e.Latitude);
            return null;
        }

        internal static C.Departement ToClient(this D.Departement e)
        {
            if (e != null)
                return new C.Departement(e.Id, e.Nom, e.Description, e.Admin);
            return null;
        }
        internal static C.Document ToClient(this D.Document e)
        {
            if (e != null)
                return new C.Document(e.Id, e.Nom, e.Description, e.Date, e.Contenu, e.Taille, e.Format, e.Id_Emp_Creee,
                e.Id_Emp_Maj);
            return null;
        }
        internal static C.Employee ToClient(this D.Employee e)
        {
            if (e != null)
                return new C.Employee(e.Id, e.Nom, e.Prenom, e.Email, e.Password, e.Birthday, e.RegNat, e.Adresse,
                e.HireDate, e.Tel, e.Coordonnee, e.Actif, e.Valide);
            return null;
        }
        internal static C.Equipe ToClient(this D.Equipe e)
        {
            if (e != null)
                return new C.Equipe(e.Id, e.Nom, e.Creee, e.Projet);
            return null;
        }
        internal static C.Event ToClient(this D.Event e)
        {
            if (e != null)
                return new C.Event(e.Id, e.Nom, e.Description, e.Lieu, e.DateDebut, e.DateFin, e.FullDay, e.Id_Employee);
            return null;
        }
        internal static C.MessageEmployee ToClient(this D.MessageEmployee e)
        {
            if (e != null)
                return new C.MessageEmployee(e.Id, e.Titre, e.Date, e.Contenu, e.MessagePrecedent, e.Id_Employee, e.Id_Destinataire);
            return null;
        }
        internal static C.MessageEquipe ToClient(this D.MessageEquipe e)
        {
            if (e != null)
                return new C.MessageEquipe(e.Id, e.Titre, e.Date, e.Contenu, e.MessagePrecedent, e.Id_Employee, e.Id_Equipe,e.Auteur);
            return null;
        }
        internal static C.MessageProjet ToClient(this D.MessageProjet e)
        {
            if (e != null)
                return new C.MessageProjet(e.Id, e.Titre, e.Date, e.Contenu, e.MessagePrecedent, e.Id_Employee, e.Id_Projet,e.Auteur);
            return null;
        }
        internal static C.MessageTache ToClient(this D.MessageTache e)
        {
            if (e != null)
                return new C.MessageTache(e.Id, e.Titre, e.Date, e.Contenu, e.MessagePrecedent, e.Id_Employee, e.Id_Tache_Equipe,e.Id_Tache_Employee,e.Auteur);
            return null;
        }
        internal static C.Pays ToClient(this D.Pays e)
        {
            if (e != null)
                return new C.Pays(e.Id,e.Code, e.Alpha2, e.Alpha3, e.Nom_FR, e.Nom_EN);
            return null;
        }
        internal static C.StatutEmployee ToClient(this D.StatutEmployee e)
        {
            if (e != null)
                return new C.StatutEmployee(e.NomStatut, e.DateDebut, e.DateFin, e.IdEmployee);
            return null;
        }
        internal static C.StatutTache ToClient(this D.StatutTache e)
        {
            if (e != null)
                return new C.StatutTache(e.Id, e.NomStatut);
            return null;
        }
        internal static C.TacheEmployee ToClient(this D.TacheEmployee e)
        {
            if (e != null)
                return new C.TacheEmployee(e.Id, e.Nom, e.Description, e.Debut, e.Fin, e.Final, e.TachePrecedente,e.Projet);
            return null;
        }
        internal static C.TacheEquipe ToClient(this D.TacheEquipe e)
        {
            if (e != null)
                return new C.TacheEquipe(e.Id, e.Nom, e.Description, e.Debut, e.Fin, e.Final, e.TachePrecedente, e.Projet);
            return null;
        }
        internal static C.Ville ToClient(this D.Ville e)
        {
            if (e != null)
                return new C.Ville(e.Id, e.Nom, e.Zip, e.Id_Pays);
            return null;
        }
        internal static C.Planning ToPlanning(this D.Planning p) {
            if (p != null)
                return new C.Planning(p.Id, p.Nom, p.Type, p.DateDebut, p.DateFinal, p.FullDay);
            return null;
        }
        internal static C.Notification ToClient(this D.Notification p) {
            if (p != null)
                return new C.Notification(p.Id, p.Id_Employee, p.Date, p.Type, p.Contenu, p.Lu, p.Id_Lien);
            return null;
        }
    }

}
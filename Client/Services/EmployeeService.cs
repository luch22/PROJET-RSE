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
    public class EmployeeService
    {

        private DS.EmployeeService service;

        public EmployeeService()
        {
            service = new DS.EmployeeService();
        }

        public IEnumerable<Employee> GetAll()
        {
            return service.GetAll().Select(a => a.ToClient());
        }

        public IEnumerable<Employee> GetAllActive() {
            return service.GetAllActive().Select(a => a.ToClient());
        }

        public IEnumerable<Employee> GetByDep(int id)
        {
            return service.GetByDep(id).Select(a => a.ToClient());
        }

        public IEnumerable<Employee> GetWithoutDep(int id)
        {
            return service.GetOtherByDep(id).Select(a => a.ToClient());
        }

        public IEnumerable<Employee> GetByProjet(int id)
        {
            return service.GetByProjet(id).Select(a => a.ToClient());
        }

        public IEnumerable<Employee> GetByEquipe(int id)
        {
            return service.GetByEquipe(id).Select(a => a.ToClient());
        }

        public IEnumerable<Employee> GetCollegue(int id) {
            return service.GetCollegue(id).Select(a => a.ToClient());
        }

        public IEnumerable<Employee> GetWithoutEquipe() {
            return service.GetWithoutEquipe().Select(a => a.ToClient());
        }

        public IEnumerable<Employee> GetByTacheEquipe(int id)
        {
            return service.GetByTacheEquipe(id).Select(a => a.ToClient());
        }

        public IEnumerable<Employee> GetByTacheEmployee(int id)
        {
            return service.GetByTacheEmployee(id).Select(a => a.ToClient());
        }

        public IEnumerable<Employee> GetByNomPrenom(string np)
        {
            return service.GetByNomPrenom(np).Select(a => a.ToClient());
        }

        public IEnumerable<Employee> GetWithDiscussion(int id)
        {
            return service.GetWithDiscussion(id).Select(a => a.ToClient());
        }

        public IEnumerable<Employee> GetByEvent(int id)
        {
            return service.GetByEvent(id).Select(a => a.ToClient());
        }
    

        public Employee GetById(int id) {
            return service.GetById(id).ToClient();
        }

        public Employee GetByEmailPassword(string email, string password) {
            return service.GetByEmailPassword(email, password).ToClient();
            //DAL.Models.Employee e = service.GetByEmailPassword(email, password);
            //if (e != null)
            //    return e.ToClient();
            //else
            //    return null;
        }

        public Employee GetManagerByProjet(int idProjet) {
            return service.GetManagerByProjet(idProjet).ToClient();
        }

        public Employee GetManagerByEquipe(int idEq) {
            return service.GetManagerByEquipe(idEq).ToClient();
        }

        public Employee Insert(Employee a) {
            return service.Insert(a.ToDal()).ToClient();
        }

        public bool Update(Employee a) {
            return service.Update(a.ToDal());
        }

        public bool ChangePassword(int id, string password) {
            return service.ChangePassword(id, password);
        }

        public bool ActiveEmployee(int id) {
            return service.ActiveEmployee(id);
        }

        public bool Delete(int id) {
            return service.Delete(id);
        }
    }
}

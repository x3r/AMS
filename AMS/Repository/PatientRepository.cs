using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AMS.Models;

namespace AMS.Repository
{
    public class PatientRepository : IRepository<Patient>
    {
        private DatabaseContext _databaseContext;

        public PatientRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Patient model)
        {
            _databaseContext.Patients.Add(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(Patient model)
        {
            _databaseContext.Patients.Remove(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Update(Patient model)
        {
            _databaseContext.Entry(model).State = EntityState.Modified;
            _databaseContext.SaveChanges();
            return true;
        }

        public List<Patient> GetAll()
        {
            return _databaseContext.Patients.ToList();
        }

        public Patient Get(int id)
        {
            return _databaseContext.Patients.Find(id);
        }
    }
}
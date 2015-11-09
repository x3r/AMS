using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AMS.Models;
using WebGrease.Css;

namespace AMS.Repository
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private DatabaseContext _databaseContext;
        public DoctorRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Doctor model)
        {
            _databaseContext.Doctors.Add(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(Doctor model)
        {
            _databaseContext.Doctors.Remove(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Update(Doctor model)
        {
            var oldModel = Get(model.DoctorId);
            oldModel.FirstName = model.FirstName;
            oldModel.LastName = model.LastName;
            oldModel.MobileNumber = model.MobileNumber;
            oldModel.Specialization = model.Specialization;
            oldModel.Gender = model.Gender;
            oldModel.Age = model.Age;
            _databaseContext.Entry(oldModel).State = EntityState.Modified;
            return _databaseContext.SaveChanges() > 0;
        }
        public List<Doctor> GetAll()
        {
            return _databaseContext.Doctors.ToList();
        }

        public Doctor Get(int id)
        {
            return _databaseContext.Doctors.Find(id);
        }

        public Doctor GetByUser(int id)
        {
            return _databaseContext.Doctors.FirstOrDefault(u => u.UserId == id);
        }

        public bool Update(Doctor model, int userId)
        {
            var oldModel = GetByUser(userId);
            oldModel.FirstName = model.FirstName;
            oldModel.LastName = model.LastName;
            oldModel.MobileNumber = model.MobileNumber;
            oldModel.Specialization = model.Specialization;
            oldModel.Gender = model.Gender;
            oldModel.Age = model.Age;
            _databaseContext.Entry(oldModel).State = EntityState.Modified;
            return _databaseContext.SaveChanges() > 0;
        }

        public List<Doctor> GetDoctors(int page, int size)
        {
            return _databaseContext.Doctors.Select(product => product).OrderBy(p => p.Specialization).Skip(page * size).Take(size).ToList();
        }

        public List<Doctor> SearchDoctors(String area, String speciality, int page, int size)
        {
            if (area.Length == 0 || speciality.Length == 0)
            {
                var docList = from doctor in _databaseContext.Doctors
                              join address in _databaseContext.Addresses on doctor.UserId equals address.UserId
                              where doctor.Specialization.Equals(speciality) || (address.Thana.Equals(area) || address.Zilla.Equals(area))
                              select doctor;
                return docList.ToList();
            }
            else
            {
                var list = from doctor in _databaseContext.Doctors
                           join address in _databaseContext.Addresses on doctor.UserId equals address.UserId
                           where doctor.Specialization.Equals(speciality) && (address.Thana.Equals(area) || address.Zilla.Equals(area))
                           select doctor;
                return list.ToList();
            }
        }
    }
}
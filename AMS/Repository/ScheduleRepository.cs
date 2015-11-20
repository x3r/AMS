using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AMS.Models;

namespace AMS.Repository
{
    public class ScheduleRepository : IRepository<Schedule>
    {
        private DatabaseContext _databaseContext;

        public ScheduleRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Schedule model)
        {
            _databaseContext.Schedules.Add(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(Schedule model)
        {
            _databaseContext.Schedules.Remove(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Update(Schedule model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Schedule model, int doctorId)
        {
            Schedule schedule = GetByDoctor(doctorId);
            schedule.Dates = model.Dates;
            schedule.PatientNumber = model.PatientNumber;
            schedule.FromHour = model.FromHour;
            schedule.FromHour = model.FromHour;
            schedule.ToHour = model.ToHour;
            schedule.ToMinute = model.ToMinute;
            schedule.VisitingFee = model.VisitingFee;
            schedule.RegistrationFee = model.RegistrationFee;
            _databaseContext.Entry(schedule).State = EntityState.Modified;
            return _databaseContext.SaveChanges() > 0;
        }

        public List<Schedule> GetAll()
        {
            throw new NotImplementedException();
        }

        public Schedule Get(int id)
        {
            return _databaseContext.Schedules.Find(id);
        }

        public Schedule GetByDoctor(int doctorId)
        {
            return _databaseContext.Schedules.FirstOrDefault(e => e.DoctorId == doctorId);
        }
    }
}
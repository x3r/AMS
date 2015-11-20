using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AMS.Models;
using AMS.ViewModel;

namespace AMS.Repository
{
    public class AppointmentRepository : IRepository<Appointment>
    {
        private readonly DatabaseContext _databaseContext;
        public AppointmentRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public bool Add(Appointment model)
        {
            var schedule =
                _databaseContext.Schedules.First(e => e.DoctorId == model.DoctorId && e.Dates.Contains(model.Date));
            model.VisitingTime = schedule.FromHour + ":" + (schedule.FromMinute < 10 ? "0" + schedule.FromMinute : schedule.FromMinute + "");
            _databaseContext.Appointments.Add(model);
            return _databaseContext.SaveChanges() > 0;
        }

        public bool Remove(Appointment model)
        {
            _databaseContext.Appointments.Remove(model);
            return _databaseContext.SaveChanges() > 0;
        }
        public bool Remove(int appointmentId)
        {
            return Remove(Get(appointmentId));
        }
        public bool Update(Appointment model)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAllByDoctor(int doctorId)
        {
            return _databaseContext.Appointments.Where(a => a.DoctorId == doctorId).ToList();
        }

        public Appointment Get(int id)
        {
            return _databaseContext.Appointments.Find(id);
        }

        public List<AppointmentModel> GetAppointmentModels(int doctorId)
        {
            var res = from appointment in _databaseContext.Appointments
                      join patient in _databaseContext.Patients on appointment.PatientId equals patient.PatientId
                      select new AppointmentModel() { Date = appointment.Date, PatientId = patient.PatientId, PatientName = patient.Name, Serial = appointment.Serial, Uid = appointment.Uid, VisitingTime = appointment.VisitingTime };
            return res.Select(e => e).OrderByDescending(e => e.Date).ToList();
        }

        public List<AppointmentModel> GetAppointmentModelsByDate(int doctorId, string date)
        {
            var res = from appointment in _databaseContext.Appointments
                      join patient in _databaseContext.Patients on appointment.PatientId equals patient.PatientId
                      where appointment.Date == date
                      select new AppointmentModel() { Date = appointment.Date, PatientId = patient.PatientId, PatientName = patient.Name, Serial = appointment.Serial, Uid = appointment.Uid, VisitingTime = appointment.VisitingTime };
            return res.Select(e => e).OrderByDescending(e => e.Date).ToList();
        }

        public bool IsAppointmentAvailable(int doctorId, String date)
        {
            return _databaseContext.Appointments.Where(e => e.DoctorId == doctorId && e.Date == date).ToList().Count() < _databaseContext.Schedules.First(e => e.DoctorId == doctorId).PatientNumber;
        }

        public int GenerateSerial(int doctorId, String date)
        {
            return _databaseContext.Appointments.Where(e => e.DoctorId == doctorId && e.Date == date).ToList().Count();
        }

        public List<Appointment> GetAppointmentsByUser(int userId)
        {
            var res = from appointment in _databaseContext.Appointments
                      join patient in _databaseContext.Patients on appointment.PatientId equals patient.PatientId
                      where patient.UserId == userId
                      select appointment;
            return res.ToList();
        }

        public List<Appointment> GetHistory(int userId)
        {
            var res = from appointment in _databaseContext.Appointments
                      join patient in _databaseContext.Patients on appointment.PatientId equals patient.PatientId
                      where patient.UserId == userId
                      select appointment;
            return res.ToList().Where(e=>e.Date.CompareTo(DateTime.Now.ToString("M/d/yyyy"))<0).ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models;
using AMS.Repository;
using AMS.ViewModel;

namespace AMS.Service
{
    public class PatientService
    {
        private PatientRepository _patientRepository;
        private AppointmentRepository _appointmentRepository;
        private WaitingRepository _waitingRepository;
        public PatientService(PatientRepository patientRepository, AppointmentRepository appointmentRepository, WaitingRepository waitingRepository)
        {
            _patientRepository = patientRepository;
            _appointmentRepository = appointmentRepository;
            _waitingRepository = waitingRepository;
        }

        public List<Patient> GetPatients()
        {
            return _patientRepository.GetAll();
        }

        public Appointment RegisterAppointment(Appointment model, Patient patient)
        {
            if (_appointmentRepository.IsAppointmentAvailable(model.DoctorId, model.Date))
            {
                model.PatientId = _patientRepository.AddPatient(patient);
                model.TakenAt = DateTime.Now;
                model.Uid = Guid.NewGuid().ToString();
                model.Serial = _appointmentRepository.GenerateSerial(model.DoctorId, model.Date) + 1;
                _appointmentRepository.Add(model);
                return model;
            }
            else
            {
                var waiting = new Waiting();
                waiting.TakenAt = DateTime.Now;
                waiting.Name = patient.Name;
                waiting.UserId = patient.UserId;
                waiting.DoctorId = model.DoctorId;
                waiting.Date = model.Date;
                _waitingRepository.Add(waiting);
            }
            return null;
        }

        public Patient GetPatient(int id)
        {
            return _patientRepository.Get(id);
        }

        public List<Appointment> GetAppointments(int userId)
        {
            return _appointmentRepository.GetAppointmentsByUser(userId);
        }

        public bool IsAppointmentTaken(int userId, string date)
        {
            return GetAppointments(userId).Count(e => e.Date == date) > 0;
        }

        public List<Appointment> GetHistory(int userId)
        {
            return _appointmentRepository.GetHistory(userId);
        } 
    }
}
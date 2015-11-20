using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models;
using AMS.Repository;
using AMS.ViewModel;

namespace AMS.Service
{
    public class DoctorService
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly UserRepository _userRepository;
        private readonly AddressRepository _addressRepository;
        private readonly ScheduleRepository _scheduleRepository;
        private readonly AppointmentRepository _appointmentRepository;
        private readonly WaitingRepository _waitingRepository;
        public DoctorService(DoctorRepository doctorRepository, UserRepository userRepository, AddressRepository addressRepository, ScheduleRepository scheduleRepository, AppointmentRepository appointmentRepository, WaitingRepository waitingRepository)
        {
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _scheduleRepository = scheduleRepository;
            _appointmentRepository = appointmentRepository;
            _waitingRepository = waitingRepository;
        }

        public Doctor GetDoctor(String email)
        {
            return _doctorRepository.GetByUser(_userRepository.GetUserId(email));
        }

        public Doctor GetDoctor(int userId)
        {
            return _doctorRepository.GetByUser(userId);
        }

        public bool SaveProfile(Doctor doctor, String email)
        {
            doctor.UserId = _userRepository.GetUserId(email);
            return _doctorRepository.Add(doctor);
        }
        public void EditProfile(Doctor doctor, string userName)
        {
            _doctorRepository.Update(doctor, _userRepository.GetUserId(userName));
        }

        public Address GetAddress(String userName)
        {
            return _addressRepository.GetByUser(_userRepository.GetUserId(userName));
        }
        public Address GetAddress(int userId)
        {
            return _addressRepository.GetByUser(userId);
        }
        public void SaveAddress(Address address, String userName)
        {
            address.UserId = _userRepository.GetUserId(userName);
            _addressRepository.Add(address);
        }

        public void UpdateAddress(Address address, String userName)
        {
            _addressRepository.Update(address, _userRepository.GetUserId(userName));
        }

        public List<Doctor> GetDoctors(int page, int size)
        {
            return _doctorRepository.GetDoctors(page, size);
        }

        public List<Doctor> SearchDoctors(String area, String speciality, int page, int size)
        {
            return _doctorRepository.SearchDoctors(area, speciality, page, size);
        }

        public Schedule GetSchedule(String userName)
        {
            return _scheduleRepository.GetByDoctor(_doctorRepository.GetByUser(_userRepository.GetUserId(userName)).DoctorId);
        }
        public Schedule GetSchedule(int userId)
        {
            return _scheduleRepository.GetByDoctor(_doctorRepository.GetByUser(userId).DoctorId);
        }
        public void SaveSchedule(Schedule model, String userName)
        {
            model.DoctorId = _doctorRepository.GetByUser(_userRepository.GetUserId(userName)).DoctorId;
            _scheduleRepository.Add(model);
        }
        public void UpdateSchedule(Schedule model, String userName)
        {
            _scheduleRepository.Update(model, _doctorRepository.GetByUser(_userRepository.GetUserId(userName)).DoctorId);
        }
        public List<AppointmentModel> GetAppointments(String userName)
        {
            return
                _appointmentRepository.GetAppointmentModels(
                    _doctorRepository.GetByUser(_userRepository.GetUserId(userName)).DoctorId);
        }

        public int GetDoctorId(String userName)
        {
            return _doctorRepository.GetByUser(_userRepository.GetUserId(userName)).DoctorId;
        }

        public List<Waiting> GetWaitings(String email)
        {
            return _waitingRepository.GetAllByDoctor(GetDoctorId(email)).Where(e => e.Date.CompareTo(DateTime.Now.ToString("M/d/yyyy")) >= 0).ToList();
        }

    }

}
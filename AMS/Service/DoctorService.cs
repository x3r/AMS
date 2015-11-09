using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models;
using AMS.Repository;

namespace AMS.Service
{
    public class DoctorService
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly UserRepository _userRepository;
        private readonly AddressRepository _addressRepository;
        public DoctorService(DoctorRepository doctorRepository, UserRepository userRepository, AddressRepository addressRepository)
        {
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }

        public Doctor GetDoctor(String email)
        {
            return _doctorRepository.GetByUser(_userRepository.GetUserId(email));
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
    }
}
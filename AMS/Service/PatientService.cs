using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Models;
using AMS.Repository;

namespace AMS.Service
{
    public class PatientService
    {
        private PatientRepository _patientRepository;
        public PatientService(PatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public List<Patient> GetPatients()
        {
            return _patientRepository.GetAll();
        }
    }
}
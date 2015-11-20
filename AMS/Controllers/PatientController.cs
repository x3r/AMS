using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.Models;
using AMS.Service;

namespace AMS.Controllers
{
    public class PatientController : Controller
    {
        private PatientService _patientService;
        private AccountService _accountService;
        public PatientController(PatientService patientService, AccountService accountService)
        {
            _patientService = patientService;
            _accountService = accountService;
        }
        //
        // GET: /Patient/
        //public ActionResult Index()
        //{
        //    return Json(_patientService.GetPatients(), JsonRequestBehavior.AllowGet); ;
        //}

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Appointment(Appointment appointment, Patient patient, String email, String password)
        {
            if (_accountService.ValidateLogin(email, password) && _accountService.GetUser(email).Role == "Patient")
            {
                if (_patientService.IsAppointmentTaken(_accountService.GetUser(email).UserId, appointment.Date))
                {
                    return Json("Appointment already taken", JsonRequestBehavior.AllowGet);
                }
                patient.UserId = _accountService.GetUser(email).UserId;
                return Json(_patientService.RegisterAppointment(appointment, patient), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Unauthorized Access", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Patient(int id)
        {
            return View(_patientService.GetPatient(id));
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Appointments(String email, String password)
        {
            if (_accountService.ValidateLogin(email, password) && _accountService.GetUser(email).Role == "Patient")
            {
                return Json(_patientService.GetAppointments(_accountService.GetUser(email).UserId),
                    JsonRequestBehavior.AllowGet);
            }
            return Json("Unauthorized Access", JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult History(String email, String password)
        {
            if (_accountService.ValidateLogin(email, password) && _accountService.GetUser(email).Role == "Patient")
            {
                return Json(_patientService.GetHistory(_accountService.GetUser(email).UserId),
                    JsonRequestBehavior.AllowGet);
            }
            return Json("Unauthorized Access", JsonRequestBehavior.AllowGet);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AMS.Models;
using AMS.Service;
using AMS.ViewModel;

namespace AMS.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private DoctorService _doctorService;
        private PatientService _patientService;
        public DoctorController(DoctorService doctorService, PatientService patientService)
        {
            _doctorService = doctorService;
            _patientService = patientService;
        }
        //
        // GET: /Doctor/Profile
        [HttpPost]
        public ActionResult EditProfile(DoctorModel model)
        {
            if (ModelState.IsValid)
            {
                var profile = new Doctor();
                profile.FirstName = model.FirstName;
                profile.LastName = model.LastName;
                profile.Gender = model.Gender;
                profile.MobileNumber = model.MobileNumber;
                profile.Age = model.Age;
                profile.Specialization = model.Specialization;
                _doctorService.EditProfile(profile, User.Identity.Name);
                //address
                var address = new Address();
                address.HouseNo = model.HouseNo;
                address.RoadNo = model.RoadNo;
                address.Thana = model.Thana;
                address.Zilla = model.Zilla;
                _doctorService.UpdateAddress(address, User.Identity.Name);
                Session["Success"] = "Profile Updated Successfully";
            }
            else
            {
                Session["Error"] = "Sorry, Profile Cannot Be Updated";
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult EditProfile()
        {
            var doctor = _doctorService.GetDoctor(User.Identity.Name);
            var model = new DoctorModel();
            model.FirstName = doctor.FirstName;
            model.LastName = doctor.LastName;
            model.Specialization = doctor.Specialization;
            model.Gender = doctor.Gender;
            model.MobileNumber = doctor.MobileNumber;
            model.Age = doctor.Age;
            var address = _doctorService.GetAddress(User.Identity.Name);
            if (address != null)
            {
                model.HouseNo = address.HouseNo;
                model.RoadNo = address.RoadNo;
                model.Thana = address.Thana;
                model.Zilla = address.Zilla;
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult GetProfile()
        {
            var doctor = _doctorService.GetDoctor(User.Identity.Name);
            var model = new DoctorModel();
            model.FirstName = doctor.FirstName;
            model.LastName = doctor.LastName;
            model.Specialization = doctor.Specialization;
            model.Gender = doctor.Gender;
            model.MobileNumber = doctor.MobileNumber;
            model.Age = doctor.Age;
            var address = _doctorService.GetAddress(User.Identity.Name);
            if (address != null)
            {
                model.HouseNo = address.HouseNo;
                model.RoadNo = address.RoadNo;
                model.Thana = address.Thana;
                model.Zilla = address.Zilla;
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult GetProfiles(int page = 0, int size = 5)
        {
            return Json(new { Doctors = _doctorService.GetDoctors(page, size) }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult Search(String area, String speciality, int page = 0, int size = 5)
        {
            return Json(new { Doctors = _doctorService.SearchDoctors(area, speciality, page, size) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Schedule()
        {
            Schedule schedule = _doctorService.GetSchedule(User.Identity.Name);
            return View(schedule);
        }

        [HttpPost]
        public ActionResult Schedule(Schedule schedule)
        {
            _doctorService.UpdateSchedule(schedule, User.Identity.Name);
            return View(schedule);
        }
        [AllowAnonymous]
        public ActionResult GetDetails(int userId)
        {
            return Json(new { Profile = _doctorService.GetDoctor(userId), Address = _doctorService.GetAddress(userId), Schedule = _doctorService.GetSchedule(userId) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Appointments()
        {
            var date = DateTime.Now;
            string today = date.ToString("M/d/yyyy");
            ViewBag.Dates = _doctorService.GetSchedule(User.Identity.Name).Dates.Split(',').OrderBy(e => e).Where(e => e.CompareTo(today) >= 0);
            return View(_doctorService.GetAppointments(User.Identity.Name));
        }
        [HttpPost]
        public ActionResult AddAppointment(Appointment appointment, Patient patient)
        {
            appointment.DoctorId = _doctorService.GetDoctorId(User.Identity.Name);
            _patientService.RegisterAppointment(appointment, patient);
            return RedirectToAction("Appointments", "Doctor", _doctorService.GetAppointments(User.Identity.Name));
        }

        public ActionResult GetWaiting()
        {
            return View(_doctorService.GetWaitings(User.Identity.Name));
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.Service;

namespace AMS.Controllers
{
    public class PatientController : Controller
    {
        private PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }
        //
        // GET: /Patient/
        public ActionResult Index()
        {
            return Json(_patientService.GetPatients(), JsonRequestBehavior.AllowGet); ;
        }
    }
}
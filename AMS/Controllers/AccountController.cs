using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AMS.Models;
using AMS.Repository;
using AMS.Service;
using AMS.ViewModel;

namespace AMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly DoctorService _doctorService;
        private User _user;
        private EditAccountModel _editAccountModel;
        public AccountController(AccountService accountService, User user, EditAccountModel editAccountModel, DoctorService doctorService)
        {
            _accountService = accountService;
            _user = user;
            _doctorService = doctorService;
            _editAccountModel = editAccountModel;
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/#/home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                bool status = _accountService.ValidateLogin(model.Email, model.Password);
                if (status)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    ViewData["Status"] = "Login Successful";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["Error"] = "User Name/ Password Mismatch";
                    ViewData["Status"] = "Login Failure";
                    return RedirectToAction("Login", "Account");
                }

            }
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                bool status = _accountService.ValidateRegistration(model.Email);
                if (status)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    _user.Password = model.Password;
                    _user.Email = model.Email;
                    _user.Role = model.Role;
                    _accountService.SaveUser(_user);
                    if (model.Role.Equals("Doctor"))
                    {
                        var doctor = new Doctor();
                        doctor.LastName = model.LastName;
                        doctor.FirstName = model.FirstName;
                        doctor.MobileNumber = model.MobileNumber;
                        _doctorService.SaveProfile(doctor, model.Email);
                        _doctorService.SaveAddress(new Address(), model.Email);
                    }
                    Session["Success"] = "Registration Successful";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User Name already exists");
                    Session["Error"] = "Registration Failure";
                    return RedirectToAction("Register", "Account");
                }

            }
            return RedirectToAction("Register", "Account");
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            var user = _accountService.GetUser(User.Identity.Name);
            _editAccountModel.Email = user.Email;
            return View(_editAccountModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword([Bind(Exclude = "Email")]EditAccountModel model)
        {
            model.Email = User.Identity.Name;
            if (ModelState.IsValid)
            {
                if (_accountService.ValidateLogin(model.Email, model.CurrentPassword))
                {
                    _user.Email = model.Email;
                    _user.Password = model.Password;
                    _accountService.UpdateAccount(_user);
                    Session["Success"] = "Edit account successful";
                }
                else
                {
                    Session["Error"] = "Current Password does not match";
                }
            }

            return RedirectToAction("ChangePassword");
        }
        [Authorize]
        public ActionResult LogOut()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public ActionResult IsAuthenticated()
        {
            return Json(User.Identity.IsAuthenticated, JsonRequestBehavior.AllowGet);
        }

    }
}

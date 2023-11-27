
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using LabWork1.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LabWork1.Controllers
{
    public class ContactController : Controller
    {
        private IValidator<AppUser> _validator;

        public ContactController(IValidator<AppUser> validator)
        {
            _validator=validator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(AppUser model)
        {
            ValidationResult result = _validator.Validate(model);

            if (!result.IsValid)
            {

                result.AddToModelState(this.ModelState);

                // re-render the view when validation failed.
                return View("Index", model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
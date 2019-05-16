using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CozyCo.Domain.Models;
using CozyCo.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace CozyCo.WebUI.Controllers
{
    public class PropertyController : Controller
    {
        private const string PROPERTYTYPES = "PropertyTypes";
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;

        public PropertyController(IPropertyService propertyService, IPropertyTypeService propertyTypeService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
        }

        //property/Index
        public IActionResult Index()
        {
            // check if got any errors in TempData
            if(TempData["Error"] != null)
            {

                //Pass that error to the ViewData, because
                //we are communicating between action and view
                ViewData.Add("Error", TempData["Error"]);

            }



            var properties = _propertyService.GetAllProperties();
            return View(properties);
        }

        //GET property/add
        [HttpGet]
        public IActionResult Add()
        {
            //Missing the list of Property Types
            GetPropertyTypes();
            return View("Form"); //--> looking for Add.cshtml, renamed to Form.cshtml
        }

        [HttpPost]
        public IActionResult Add(Property newProperty) // -> receive data from HTML form
        {
            if (ModelState.IsValid) // all required fields are completed
            {
                // We should be able to add the new property
                _propertyService.Create(newProperty);
                //Service receives the new property
                //service send the new property to respository (saved)

                return RedirectToAction(nameof(Index));
            }

            GetPropertyTypes();
            return View("Form");



        }
        public IActionResult Detail(int id) // -> get id from URL
        {
            var property = _propertyService.GetById(id);
            //Need to know what Id I am going to look for
            return View(property);
        }

        public IActionResult Delete(int id)
        {
            var succeeded = _propertyService.Delete(id);

            if (!succeeded) //when delete fails (false)
                //Using tempdata - because we are communicating
                //between actions = from Delete to Index
                TempData.Add("Error", "Sorry, the property cannot be deleted, try again later.");

            return RedirectToAction(nameof(Index));

        }
        //property/edit/1 or property that will be edited
        public IActionResult Edit(int id) // --> get id from URL
        {
            var property = _propertyService.GetById(id);

            return View("Form", property); //Edit.cshtml, renamed to Form.cshtml
        }

        //property has been edited
        [HttpPost]
        //get id from URL
        //get updatedProperty from FORM
        public IActionResult Edit(int id, Property updatedProperty)
        {
            if (ModelState.IsValid)
            {
                _propertyService.Update(updatedProperty);

                return RedirectToAction(nameof(Index));
            }

            return View("Form.cshtml", updatedProperty); //By passing updated property
                                                         //We trigger the logic
                                                         //For edit within the Form.cshtml
        }

        private void GetPropertyTypes()
        {
            var propertyTypes = _propertyTypeService.GetAll();
            ViewData.Add(PROPERTYTYPES, propertyTypes);
        }
    }

}
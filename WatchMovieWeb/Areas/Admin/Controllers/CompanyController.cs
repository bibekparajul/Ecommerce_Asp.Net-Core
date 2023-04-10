using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WacthMovie.DataAccess.Repository.IRepository;
using WatchMovie.Models;
using WatchMovie.Models.ViewModels;
using WatchMovieWeb.DataAccess;


namespace WatchMovieWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {

            Company company = new();


            if (id == null || id == 0)

            {
                return View(company);
            }
            else
            {
                company = _unitOfWork.Company.GetFirstorDefault(u => u.Id == id);
                return View(company);

            }
        }

            //POST
            [HttpPost]                 //used to handle the http request
            [ValidateAntiForgeryToken] //used to prevent the cross site request forgery attack
            public IActionResult Upsert(Company obj, IFormFile? file)
            {
                if (ModelState.IsValid)
                {

                    if (obj.Id == 0)
                    {
                        _unitOfWork.Company.Add(obj);
                        TempData["success"] = "Company Created Successfully";


                    }
                    else
                    {
                        _unitOfWork.Company.Update(obj);
                        TempData["success"] = "Company updated Successful";
                    }   
                    _unitOfWork.Save();        //    

                    return RedirectToAction("Index");
                }
                return View(obj);
            }


            #region API CALLS
            [HttpGet]

            public IActionResult GetAll()
            {
                var companyList = _unitOfWork.Company.GetAll();
                return Json(new { data = companyList });
            }
            //delete
            [HttpDelete]                 //used to handle the http request
            public IActionResult Delete(int? id)
            {

                var obj = _unitOfWork.Company.GetFirstorDefault(u => u.Id == id);
                if (obj == null)
                {
                    return Json(new { success = false, message = "Error while deleting" });
                }
     
                _unitOfWork.Company.Remove(obj);    //
                _unitOfWork.Save();         //
                return Json(new { success = true, message = "Successfully  deleted" });

            }
            #endregion
        
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WacthMovie.DataAccess.Repository.IRepository;
using WatchMovie.Models;
using WatchMovieWeb.DataAccess;


namespace WatchMovieWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; //

        public ProductController(IUnitOfWork unitOfWork)    //
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll(); //   
            return View(objCoverTypeList);
        }
        //GET


        //GET(for Edit) (Validation remains same)
        public IActionResult Upsert(int? id)
        {
            //below ienu are used for accesing the dropdown form product form

            Product product = new Product();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

            IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
            u => new SelectListItem
             {
            Text = u.Name,
            Value = u.Id.ToString()
             });
            if (id == null || id == 0)

            {
                //create product
                ViewBag.CategoryList = CategoryList; //viewbag in action
                return View(product);
            }
            else
            {
                //update product
            }


            return View(product);
        }

        //POST
        [HttpPost]                 //used to handle the http request
        [ValidateAntiForgeryToken] //used to prevent the cross site request forgery attack
        public IActionResult Upsert(CoverType obj)
        {

            //server side validation because name cannot be empty
            if (ModelState.IsValid)
            {

                _unitOfWork.CoverType.Update(obj);   //
                _unitOfWork.Save();        //    
                TempData["success"] = "Covertype Updated Successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET(for Delete) 
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstorDefault(u => u.Id == id);
            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst);
        }

        //POST
        [HttpPost]                 //used to handle the http request
        [ValidateAntiForgeryToken] //used to prevent the cross site request forgery attack
        public IActionResult DeletePOSt(int? id)
        {

            var obj = _unitOfWork.CoverType.GetFirstorDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(obj);    //
            _unitOfWork.Save();         //
            TempData["success"] = "Covertype Deleted Successfully";

            return RedirectToAction("Index");


        }

    }
}

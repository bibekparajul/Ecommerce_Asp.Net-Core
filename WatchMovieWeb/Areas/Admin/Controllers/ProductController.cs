using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WacthMovie.DataAccess.Repository.IRepository;
using WatchMovie.Models;
using WatchMovie.Models.ViewModels;
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
            //ViewBag ra viewdata ko satta

            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                })
            };

            if (id == null || id == 0)

            {


                return View(productVM);
            }
            else
            {
                //update product
            }


            return View(productVM);
        }

        //POST
        [HttpPost]                 //used to handle the http request
        [ValidateAntiForgeryToken] //used to prevent the cross site request forgery attack
        public IActionResult Upsert(ProductVM obj, IFormFile file)
        {

            //server side validation because name cannot be empty
            if (ModelState.IsValid)
            {

                //_unitOfWork.CoverType.Update(obj);   //
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

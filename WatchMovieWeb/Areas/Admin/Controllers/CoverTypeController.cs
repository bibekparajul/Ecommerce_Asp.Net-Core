using Microsoft.AspNetCore.Mvc;
using WacthMovie.DataAccess.Repository.IRepository;
using WatchMovie.Models;
using WatchMovieWeb.DataAccess;


namespace WatchMovieWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; //

        public CoverTypeController(IUnitOfWork unitOfWork)    //
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll(); //   
            return View(objCoverTypeList);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]                 //used to handle the http request
        [ValidateAntiForgeryToken] //used to prevent the cross site request forgery attack
        public IActionResult Create(CoverType obj)
        {


            //server side validation because name cannot be empty
            if (ModelState.IsValid)
            {

                _unitOfWork.CoverType.Add(obj);    //
                _unitOfWork.Save();      //
                TempData["success"] = "Covertype Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET(for Edit) (Validation remains same)
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
  

            //find incase of priimary key only if not below sabai bujhxa!!!
            var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstorDefault(u => u.Id == id);    //
            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst);
        }

        //POST
        [HttpPost]                 //used to handle the http request
        [ValidateAntiForgeryToken] //used to prevent the cross site request forgery attack
        public IActionResult Edit(CoverType obj)
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

using Microsoft.AspNetCore.Mvc;
using WacthMovie.DataAccess.Repository.IRepository;
using WatchMovie.Models;
using WatchMovieWeb.DataAccess;


namespace WatchMovieWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; //

        public CategoryController(IUnitOfWork unitOfWork)    //
        {
            _unitOfWork = unitOfWork;
        }
        //the below is used to retrieve and display the data from database and show it to page 
        //configured in Index.cshtml of category
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll(); //   
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            
            return View();
        }

        //POST
        [HttpPost]                 //used to handle the http request
        [ValidateAntiForgeryToken] //used to prevent the cross site request forgery attack
        public IActionResult Create(Category obj)
        {

            //custom validation can be done as follows
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display order cannot be same as name");
            }
            //server side validation because name cannot be empty
            if (ModelState.IsValid)
            {

                _unitOfWork.Category.Add(obj);    //
                _unitOfWork.Save();      //
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);   
        }
        
        //GET(for Edit) (Validation remains same)
        public IActionResult Edit(int? id)
        {
            if(id==null|| id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);

            //find incase of priimary key only if not below sabai bujhxa!!!
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstorDefault(u => u.Id == id);    //
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if(categoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(categoryFromDbFirst);
        }

        //POST
        [HttpPost]                 //used to handle the http request
        [ValidateAntiForgeryToken] //used to prevent the cross site request forgery attack
        public IActionResult Edit(Category obj)
        {

            //custom validation can be done as follows
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display order cannot be same as name");
            }
            //server side validation because name cannot be empty
            if (ModelState.IsValid)
            {

                _unitOfWork.Category.Update(obj);   //
                _unitOfWork.Save();        //    
                TempData["success"] = "Category Updated Successfully";

                return RedirectToAction("Index");
            }
            return View(obj);   
        }
        
        //GET(for Delete) 
        public IActionResult Delete(int? id)
        {
            if(id==null|| id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstorDefault(u => u.Id == id);   
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if(categoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(categoryFromDbFirst);
        }

        //POST
        [HttpPost]                 //used to handle the http request
        [ValidateAntiForgeryToken] //used to prevent the cross site request forgery attack
        public IActionResult DeletePOSt(int ?id)
        {

            var obj = _unitOfWork.Category.GetFirstorDefault(u => u.Id == id);  
            if(obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);    //
                _unitOfWork.Save();         //
            TempData["success"] = "Category Deleted Successfully";

            return RedirectToAction("Index");
     
     
        }

    }
}

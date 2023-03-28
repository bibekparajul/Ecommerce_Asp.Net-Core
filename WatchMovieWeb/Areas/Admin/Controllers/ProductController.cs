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

        //to access wwwroot we need webhost env
        //for create firstmodel git

        private readonly IWebHostEnvironment _hostEnvironment;


        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)    //
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment; 
        }

        public IActionResult Index()
        {
            return View();
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
                productVM.Product = _unitOfWork.Product.GetFirstorDefault(u=>u.Id==id);
                return View(productVM);

                //update product
            }


        }

        //POST
        [HttpPost]                 //used to handle the http request
        [ValidateAntiForgeryToken] //used to prevent the cross site request forgery attack
        public IActionResult Upsert(ProductVM obj, IFormFile ?file)
        {


            
            if (ModelState.IsValid) 
            {
                //for creating firstproduct
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extention = Path.GetExtension(file.FileName);

                    if(obj.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extention;
                }
                if(obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);

                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);


                }   //
                //for creating firstproduct
                _unitOfWork.Save();        //    
                TempData["success"] = "Product Created Successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //apiendpoints data tables (edit and delete ko lagi)

        #region API CALLS
        [HttpGet]

        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");
            return Json(new {data =  productList });
        }
        //delete
        [HttpDelete]                 //used to handle the http request
        [ValidateAntiForgeryToken] //used to prevent the cross site request forgery attack
        public IActionResult Delete(int? id)
        {

            var obj = _unitOfWork.Product.GetFirstorDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(obj);    //
            _unitOfWork.Save();         //
            return Json(new { success = true, message = "Successfully  deleted" });




        }
        #endregion
    }


}

using Microsoft.AspNetCore.Mvc;
using MVCWeb.DataAccess.Data;
using MVCWeb.DataAccess.Repository.IRepository;
using MVCWeb.Models;

namespace MVCWebApp.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //if(obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "Name and Display Order cannot be same");
            //}

            //if(obj.Name != null && obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Name cannot be test");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //If finding by primary key 
            //Category? categoryObj = _db.Categories.Find(id);
            // //If finding by anyother key
            //Category? categoryObj1 = _db.Categories.FirstOrDefault(c => c.Id == id);
            // // If finding by any criteria
            //Category? categoryObj2 = _db.Categories.Where(c => c.Id == id).FirstOrDefault();

            Category? categoryObj = _unitOfWork.Category.Get(c => c.Id == id);

            if (categoryObj == null)
            {
                return NotFound();
            }

            return View(categoryObj);
        }
        [HttpPost]
        public IActionResult Edit(Category categoryObj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(categoryObj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //If finding by primary key 
            Category? categoryObj = _unitOfWork.Category.Get(c => c.Id == id);

            if (categoryObj == null)
            {
                return NotFound();
            }
            return View(categoryObj);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? categoryObj = _unitOfWork.Category.Get(c => c.Id == id);
            if(categoryObj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(categoryObj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

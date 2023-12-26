using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {


                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Create Successfully";
                return RedirectToAction("Index");
            }
            return View(obj); 
        }
        //Edit
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            var categoeyFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (categoeyFromDb == null)
            {
                return NotFound();
            }

            return View(categoeyFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {


                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Update Successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoeyFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (categoeyFromDb == null)
            {
                return NotFound();
            }

            return View(categoeyFromDb);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

                _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";

            return RedirectToAction("Index");
            
            
        }
    }
}

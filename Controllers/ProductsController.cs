using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Pharmacy.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles = "admin")]
        public IActionResult List()
        {
            var products = _db.Products.ToList();
            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products product, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var filePath = Path.Combine(_uploadFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    product.ImagePath = $"/images/{fileName}";
                }

                _db.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Products product, IFormFile? ImageFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Handle file upload
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var filePath = Path.Combine(_uploadFolder, fileName);

                    // Delete the old image if it exists
                    if (!string.IsNullOrEmpty(product.ImagePath))
                    {
                        var oldFilePath = Path.Combine(_uploadFolder, Path.GetFileName(product.ImagePath));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    product.ImagePath = $"/images/{fileName}";
                }
                else
                {
                    // If no new file is uploaded, keep the existing image path
                    var existingProduct = _db.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);
                    if (existingProduct != null)
                    {
                        product.ImagePath = existingProduct.ImagePath;
                    }
                }

                _db.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(product);
        }



        // GET: Products/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}

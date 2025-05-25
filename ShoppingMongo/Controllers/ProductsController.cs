using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingMongo.Dtos.CategoryDtos;
using ShoppingMongo.Dtos.ProductDos;
using ShoppingMongo.Services.CategoryServices;
using ShoppingMongo.Services.ProductServices;
using System.Threading.Tasks;

namespace ShoppingMongo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productservice;

        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productservice, ICategoryService categoryService)
        {
            _productservice = productservice;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> ProductList()
        {
            var products = await _productservice.GetAllProductAsync();
            var categories = await _categoryService.GetAllCategoryAsync();

            var result = products.Select(p => new ResultProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                CategoryName = categories.FirstOrDefault(c => c.CategoryId == p.CategoryId)?.CategoryName,
                ProductPrice = p.ProductPrice,
                StockCount = p.StockCount,
                Status = p.Status,
                ImageUrls = p.ImageUrls ?? new List<string>(),

            }).ToList();

            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            ViewBag.v = categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId
            }).ToList();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            if (createProductDto.ImageFiles != null && createProductDto.ImageFiles.Count > 0)
            {
                createProductDto.ImageUrls = new List<string>();

                foreach (var file in createProductDto.ImageFiles)
                {
                    if (file.Length > 0)
                    {
                        // Benzersiz dosya ismi oluştur
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine("wwwroot/productimages", fileName);

                        using var stream = new FileStream(filePath, FileMode.Create);
                        await file.CopyToAsync(stream);

                        // URL olarak kaydet (örnek: "/productimages/filename.jpg")
                        var url = "/productimages/" + fileName;
                        createProductDto.ImageUrls.Add(url);
                    }
                }
            }

            await _productservice.CreateProductAsync(createProductDto);
            return RedirectToAction("ProductList");
        }


        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productservice.DeleteProductAsync(id);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var product = await _productservice.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            var categories = await _categoryService.GetAllCategoryAsync();

            var updateProductDto = new UpdateProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                Status = product.Status,
                StockCount = product.StockCount,
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName,
                Size = product.Size,
                Color = product.Color,

                // Burada görselleri set et
                ImageUrls = product.ImageUrls ?? new List<string>(),

                // Eğer ImagePaths kullanıyorsan onu da set et, ama senin senaryonda gerek yok gibi
                ImagePaths = product.ImagePaths ?? new List<string>(),

                Categories = categories.Select(c => new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId.ToString()
                }).ToList()
            };

            // ViewData ya da ViewBag ile kategori listesini View'a gönder
            ViewData["Categories"] = new SelectList(categories, "Id", "Name", product.CategoryId);

            return View(updateProductDto);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto, [FromForm] List<string> ImagesToDelete)
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            ViewData["Categories"] = new SelectList(categories, "CategoryId", "CategoryName", updateProductDto.CategoryId);

            if (!ModelState.IsValid)
            {
                return View(updateProductDto);
            }

            try
            {
                if (ImagesToDelete != null && ImagesToDelete.Any())
                {
                    updateProductDto.ImageUrls = updateProductDto.ImageUrls?.Where(url => !ImagesToDelete.Contains(url)).ToList() ?? new List<string>();

                    foreach (var imageUrl in ImagesToDelete)
                    {
                        var filePath = Path.Combine("wwwroot", imageUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                }

                if (updateProductDto.NewImageFiles != null && updateProductDto.NewImageFiles.Count > 0)
                {
                    if (updateProductDto.ImageUrls == null)
                        updateProductDto.ImageUrls = new List<string>();

                    foreach (var file in updateProductDto.NewImageFiles)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            var filePath = Path.Combine("wwwroot", "productimages", fileName);

                            using var stream = new FileStream(filePath, FileMode.Create);
                            await file.CopyToAsync(stream);

                            var url = "/productimages/" + fileName;
                            updateProductDto.ImageUrls.Add(url);
                        }
                    }
                }

                await _productservice.UpdateProductAsync(updateProductDto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu: " + ex.Message);
                // Kategoriler zaten yüklü, view'a dön
                return View(updateProductDto);
            }

            return RedirectToAction("ProductList");
        }


        public async Task<IActionResult> DetailProduct(string id)
        {
            var product = await _productservice.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound("Ürün bulunamadı.");
            }

            var categoryList = await _categoryService.GetAllCategoryAsync();
            var categoryName = categoryList?.FirstOrDefault(c => c.CategoryId == product.CategoryId)?.CategoryName ?? "Kategori Yok";

            var result = new ResultProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ImageUrls = product.ImageUrls, // List<string>
                Status = product.Status,
                StockCount = product.StockCount,
                CategoryId = product.CategoryId,
                CategoryName = categoryName,
                Size = product.Size,
                Color = product.Color
            };


            return View(result);
        }

    }
}

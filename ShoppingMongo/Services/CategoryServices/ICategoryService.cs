using ShoppingMongo.Dtos.CategoryDtos;
using ShoppingMongo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMongo.Services.CategoryServices
{
    /// <summary>
    /// Kategori ile ilgili iş servislerini tanımlayan arayüz.
    /// CRUD işlemleri ve ID'ye göre veri getirme gibi işlemleri içerir.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Tüm kategorileri asenkron olarak getirir.
        /// </summary>
        /// <returns>Kategori DTO listesini döner.</returns>
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();

        /// <summary>
        /// Yeni bir kategori oluşturur.
        /// </summary>
        /// <param name="createCategoryDto">Kategori oluşturmak için gerekli veriler.</param>
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);

        /// <summary>
        /// Var olan bir kategoriyi günceller.
        /// </summary>
        /// <param name="updateCategoryDto">Güncellenecek kategori bilgileri.</param>
        Task<bool> UpdateCategoryAsync(GetCategoryByIdDto dto, IFormFile imageFile);


        /// <summary>
        /// Belirtilen ID'ye sahip kategoriyi siler.
        /// </summary>
        /// <param name="id">Silinecek kategorinin ID değeri.</param>
        Task DeleteCategoryAsync(string id);

        /// <summary>
        /// ID'ye göre bir kategori getirir.
        /// </summary>
        /// <param name="id">Getirilecek kategorinin ID değeri.</param>
        /// <returns>Belirli bir kategori DTO’su.</returns>
        Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id);

        Task<List<Category>> GetAllAsync();
        Task<Category> GetCategoryByNameAsync(string categoryName);

    }
}

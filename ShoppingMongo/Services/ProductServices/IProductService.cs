using ShoppingMongo.Dtos.ProductDos;
using ShoppingMongo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMongo.Services.ProductServices
{
    /// <summary>
    /// Ürün (Product) ile ilgili servis işlemlerini tanımlayan arayüz.
    /// Ürünleri listeleme, oluşturma, güncelleme, silme ve ID’ye göre getirme işlemleri içerir.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Tüm ürünleri asenkron olarak getirir.
        /// </summary>
        /// <returns>Ürün DTO listesini döner.</returns>
        Task<List<ResultProductDto>> GetAllProductAsync();

        /// <summary>
        /// Yeni bir ürün oluşturur.
        /// </summary>
        /// <param name="createProductDto">Oluşturulacak ürün bilgileri.</param>
        Task CreateProductAsync(CreateProductDto createProductDto);

        /// <summary>
        /// Mevcut bir ürünü günceller.
        /// </summary>
        /// <param name="updateProductDto">Güncellenecek ürün bilgileri.</param>
        Task UpdateProductAsync(UpdateProductDto updateProductDto);

        /// <summary>
        /// Belirtilen ID'ye sahip ürünü siler.
        /// </summary>
        /// <param name="id">Silinecek ürünün ID'si.</param>
        Task DeleteProductAsync(string id);

        /// <summary>
        /// Belirtilen ID'ye göre bir ürünü getirir.
        /// </summary>
        /// <param name="id">Getirilecek ürünün ID'si.</param>
        /// <returns>Ürün detaylarını içeren DTO.</returns>
        Task<GetProductByIdDto> GetProductByIdAsync(string id);
        Task<List<ResultProductDto>> GetProductsByCategoryNameAsync(string categoryName);
        Task<List<ResultProductDto>> GetAllAsync();
    }
}

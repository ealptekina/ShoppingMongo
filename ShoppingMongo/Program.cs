using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShoppingMongo.Services.CategoryServices;
using ShoppingMongo.Services.CsutomerServices;
using ShoppingMongo.Services.ProductServices;
using ShoppingMongo.Services.SliderService;
using ShoppingMongo.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



// Uygulamadaki servis katmanlar�n�n ba��ml�l�klar�n� belirliyoruz.
// Scoped olarak tan�mland�klar� i�in her HTTP iste�i ba��na bir nesne �rne�i olu�turulur.
// Bu yap�, Entity Framework gibi veritaban� i�lemlerinde veri tutarl�l��� sa�lar.
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISliderService, SliderService>();

// AutoMapper konfig�rasyonlar�n� bu projedeki (�al��an derleme i�indeki) t�m profillerden otomatik olarak y�kler.
// B�ylece manuel olarak her mapping profilini tek tek belirtmeye gerek kalmaz.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// "DatabaseSettingsKey" adl� yap�land�rma b�l�m�n�, DatabaseSettings s�n�f�na map eder.
// B�ylece uygulama genelinde IOptions<DatabaseSettings> �zerinden tip g�venli �ekilde eri�ilebilir.
//appsettings.json i�indeki b�l�m ad� "DatabaseSettingsKey" olmal�.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettingsKey"));


// "DatabaseSettings" s�n�f�, "IDatabaseSettings" aray�z�n� implemente ediyorsa,
// bu sat�r, IOptions<DatabaseSettings> �zerinden yap�land�rmay� okuyup IDatabaseSettings t�r�yle servis olarak sunar.
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetSection("DatabaseSettingsKey:ConnectionString").Value;
    return new MongoClient(connectionString);
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();

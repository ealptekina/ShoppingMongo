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



// Uygulamadaki servis katmanlarýnýn baðýmlýlýklarýný belirliyoruz.
// Scoped olarak tanýmlandýklarý için her HTTP isteði baþýna bir nesne örneði oluþturulur.
// Bu yapý, Entity Framework gibi veritabaný iþlemlerinde veri tutarlýlýðý saðlar.
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISliderService, SliderService>();

// AutoMapper konfigürasyonlarýný bu projedeki (çalýþan derleme içindeki) tüm profillerden otomatik olarak yükler.
// Böylece manuel olarak her mapping profilini tek tek belirtmeye gerek kalmaz.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// "DatabaseSettingsKey" adlý yapýlandýrma bölümünü, DatabaseSettings sýnýfýna map eder.
// Böylece uygulama genelinde IOptions<DatabaseSettings> üzerinden tip güvenli þekilde eriþilebilir.
//appsettings.json içindeki bölüm adý "DatabaseSettingsKey" olmalý.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettingsKey"));


// "DatabaseSettings" sýnýfý, "IDatabaseSettings" arayüzünü implemente ediyorsa,
// bu satýr, IOptions<DatabaseSettings> üzerinden yapýlandýrmayý okuyup IDatabaseSettings türüyle servis olarak sunar.
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

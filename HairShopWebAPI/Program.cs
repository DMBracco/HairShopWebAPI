using BLL.Entities;
using BLL.IRepositories;
using DAL.Entities;
using DAL.Repositories;
using HairShopWebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;
    var env = builder.Environment;
    // Add services to the container.
    services.AddCors(options =>
    {
        options.AddPolicy(name: "ReactPolicy",
                          builder =>
                          {
                              builder.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                          });
    });

    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddDbContext<ShopContext>();

    services.AddScoped<IRepository<Brand>, EFBrandRepository>();
    services.AddScoped<IGenericRepository<ProductType>, GenericRepository<ProductType>>();
    services.AddScoped<IGenericRepository<HairType>, GenericRepository<HairType>>();
    services.AddScoped<IGenericRepository<Discount>, GenericRepository<Discount>>();
    services.AddScoped<IGenericRepository<Supplier>, GenericRepository<Supplier>>();
    services.AddScoped<IGenericRepository<Supply>, GenericRepository<Supply>>();
    services.AddScoped<IRepository<Product>, EFProductRepository>();
    services.AddScoped<IRepository<Check>, EFCheckRepository>();
    services.AddScoped<IBaseRepository, EFBaseRepository>();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ReactPolicy");

app.UseAuthorization();

app.MapControllers();

SeedData.EnsurePopulated(app);

app.Run();

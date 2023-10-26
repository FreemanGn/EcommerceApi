using EcommerceApi.Data;
using EcommerceApi.Repository;
using EcommerceApi.Repository.Interface;
using EcommerceApi.Services;
using EcommerceApi.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION"));
    });
}


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.UseRouting();
app.UseCors();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();


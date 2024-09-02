using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ShopNow.BAL.Services;
using ShopNow.DAL.Data;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using ShopNow.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ProductCategoryServices>();
builder.Services.AddScoped<IRepository<ProductCategory>, ProductCategoryRepository>();

builder.Services.AddScoped<ProductServices>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();

builder.Services.AddScoped<IProductRepository, GetProductByPaginationRepository>();

builder.Services.AddScoped<ImageServices>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

builder.Services.AddScoped<ProductImageServices>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();


builder.Services.AddScoped<ContactServices>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();


builder.Services.AddScoped<CustomerServices>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<AddressServices>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();


builder.Services.AddScoped<ShoppingCartServices>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

builder.Services.AddScoped<WishListServices>();
builder.Services.AddScoped<IWishListRepository, WishListRepository>();

builder.Services.AddScoped<ProductOrderServices>();
builder.Services.AddScoped<IProductOrderRepository, ProductOrderRepository>();

builder.Services.AddScoped<ReviewServices>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddScoped<RatingServices>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();

builder.Services.AddScoped<ComplaintServices>();
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();

builder.Services.AddScoped<AdminServices>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();


builder.Services.AddScoped<ProductCategoryImageServices>();
builder.Services.AddScoped<IProductCategoryImageRepository, ProductCategoryImageRepository>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Customer/Login"; 
    options.Events.OnRedirectToLogin = context =>
    {
        if (context.Request.Path.StartsWithSegments("/Admin"))
        {
            context.Response.Redirect("/Admin/Login");
        }
        else
        {
            context.Response.Redirect("/Customer/Login");
        }
        return Task.CompletedTask;
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

var connectionString = builder.Configuration.GetConnectionString("ConnStr");
builder.Services.AddDbContext<ShopNowDbContext>(x =>
{
    x.UseSqlServer(connectionString);
    x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

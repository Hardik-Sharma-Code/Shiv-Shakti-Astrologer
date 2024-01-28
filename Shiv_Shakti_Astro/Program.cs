using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shiv_Shakti_Astro.Data;
using Shiv_Shakti_Astro.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ShivShaktiDbContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), options => options.EnableRetryOnFailure()));
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=GetCustomer}/{id?}");

app.Run();

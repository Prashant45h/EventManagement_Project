using EventManagement_Project.Repository_Class;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Plugins;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;         
    options.Cookie.IsEssential = true;
});




// Add services to the container.
builder.Services.AddControllersWithViews();

                    
builder.Services.AddScoped<SignIn_Repo>(_ =>
{
    var connectionString = builder.Configuration.GetConnectionString("connection");
    return new SignIn_Repo(connectionString);
});
builder.Services.AddScoped<VenuRepos>(_ =>
{
    var _connectionString = builder.Configuration.GetConnectionString("connection");
    return new VenuRepos(_connectionString);
});
builder.Services.AddScoped<EquipmentRepos>(_ =>
{
    var _connString = builder.Configuration.GetConnectionString("connection");
    return new EquipmentRepos(_connString);
});
builder.Services.AddScoped<FoodRepos>(_ =>
{
    var Sqlconn = builder.Configuration.GetConnectionString("connection");
    return new FoodRepos(Sqlconn);
});
builder.Services.AddScoped<LightingsRepos>(_ =>
{
    var Con = builder.Configuration.GetConnectionString("connection");
    return new LightingsRepos(Con);
});
builder.Services.AddScoped<FlowerRepos>(_ =>
{
    var _SqlConn = builder.Configuration.GetConnectionString("connection");
    return new FlowerRepos(_SqlConn);
});
builder.Services.AddScoped<BookingVenuRepos>(_ =>
{
    var _Connection = builder.Configuration.GetConnectionString("connection");
    return new BookingVenuRepos(_Connection);
});
builder.Services.AddScoped<BookEqipmentRepos>(_ =>
{
    var _sqlConnection = builder.Configuration.GetConnectionString("connection");
    return new BookEqipmentRepos(_sqlConnection);
});
builder.Services.AddScoped<BookingFoodRepos>(_ =>
{
    var SQLCON = builder.Configuration.GetConnectionString("connection");
    return new BookingFoodRepos(SQLCON);
});
builder.Services.AddScoped<LightBookingRepos>(_ =>
{
    var _SQLCON = builder.Configuration.GetConnectionString("connection");
    return new LightBookingRepos(_SQLCON);
});
builder.Services.AddScoped<BookingFlowerRepos>(_ =>
{
    var _CONN = builder.Configuration.GetConnectionString("connection");
    return new BookingFlowerRepos(_CONN);
});
builder.Services.AddScoped<BookingDetailsRepos>(_ =>
{
    var _CONNECTION = builder.Configuration.GetConnectionString("connection");
    return new BookingDetailsRepos(_CONNECTION);
});
builder.Services.AddScoped<AllBookingRepos>(_ =>
{
    var SQLCONNECTION = builder.Configuration.GetConnectionString("connection");
    return new AllBookingRepos(SQLCONNECTION);
});






var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SignIn}/{action=Login}/{id?}");

app.Run();

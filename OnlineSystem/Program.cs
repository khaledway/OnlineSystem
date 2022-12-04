
using OnlineSystem.Service.Services;
using OnlineSystem.Service.Interfaces;
using System;
using OnlineSystem.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using OnlineSystem.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using NLog.Web;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Hosting;




var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{

var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddControllers();
    #region My Service

    builder.Services.AddDbContext<OnlineShopContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
    });
    builder.Services.AddScoped<IOnlineShopContext, OnlineShopContext>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    #endregion


    #region my app configuration
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    #endregion



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

}
catch (Exception ex)
{
    logger.Error(ex , "test In program : Stopped program because of exception");
}
finally 
{
    NLog.LogManager.Shutdown();
}
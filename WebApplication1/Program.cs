using System.Globalization;
using LabWork1.Models;
using LabWork2.Services;
using LabWork1;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using FluentValidation;
using FluentValidation.AspNetCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews()
    .AddFluentValidation(s =>
    {
        s.RegisterValidatorsFromAssemblyContaining<Program>();

    })
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options => {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(LangResource));
    });

builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("uk"),
    };
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

string connection = builder.Configuration.GetConnectionString("BookingContext");

builder.Services.AddDbContext<BookingContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IValidator<AppUser>, AppUserValidator>();


builder.Logging.ClearProviders();



builder.Logging.AddSerilog();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.Use((context, next) =>
{
    Log.Information($"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString} [:] " +
                    $"T:{DateTime.Now}, " +
                    $"IP:{context.Connection.RemoteIpAddress}");
    return next();
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

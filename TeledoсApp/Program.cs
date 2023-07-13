using Microsoft.EntityFrameworkCore;
using Teledoc.Application.Interfaces;
using Teledoc.Application.Services;
using Teledoc.Database;
using Teledoc.Database.Repositories;
using Teledoc.Domain;
using Teledoc.Domain.Interfaces;
using Teledoc.Domain.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddScoped<TeledocContext, TeledocContext>();

builder.Services.AddDbContext<TeledocContext>(
        option => option.UseSqlServer(builder.Configuration.GetConnectionString("Default")
    ));

builder.Services.AddTransient<IRepository<ClientFounder>, BaseRepository<ClientFounder>>();
builder.Services.AddTransient<IRepository<Founder>, FounderRepository>();
builder.Services.AddTransient<IRepository<Client>, ClientRepository>();

builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IFounderService, FounderService>();   

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Client}/{action=Index}/{id?}");

app.Run();

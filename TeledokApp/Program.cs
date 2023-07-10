using Microsoft.EntityFrameworkCore;
using Teledok.Domain;
using Teledok.Domain.Infrastructure;
using Teledok.Domain.Interfaces;
using Teledok.Domain.Models;
using Teledok.Domain.Models.Clients;
using Teledok.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TeledokContext>(
        option => option.UseSqlServer(builder.Configuration.GetConnectionString("Default")
    ));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));

builder.Services.AddTransient<IRepository<Incorporator>, IncorporatorRepository>();
builder.Services.AddTransient<IRepository<IndividualPerson>, IndividualPersonRepository>();
builder.Services.AddTransient<IRepository<LegalEntity>, LegalEntityRepository>();

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

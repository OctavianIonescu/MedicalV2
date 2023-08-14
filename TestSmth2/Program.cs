using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestSmth2.Data;
using TestSmth2.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MedicalDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalConnectionString")));
builder.Services.AddDbContext<MedAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MedAuthConnectionString")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MedAuthDbContext>();
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IAntiBioticRepo, AntiBioticRepo>();
builder.Services.AddScoped<IEntryRepo, EntryRepo>();
builder.Services.AddScoped<IFileRepo, CloudinaryFileRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IPatientRepo, PatientRepo>();
builder.Services.AddScoped<IResistanceRepo, ResistanceRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();

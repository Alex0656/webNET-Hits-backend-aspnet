
using lab1_backend;
using lab1_backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));

//Секретные фразы, которые знает только сервер
var secretKey = builder.Configuration.GetSection("JWTSettings:SecretKey").Value;
var issuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value;
var audience = builder.Configuration.GetSection("JWTSettings:Audience").Value;
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true,
        IssuerSigningKey = signingKey,
        ValidateIssuerSigningKey = true,
        //ClockSkew = TimeSpan.Zero
        LifetimeValidator = CustomLifeTime.CustomLifetimeValidator
    };
});

/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyMale", policyBuilder => policyBuilder.RequireClaim("Gender", "Female"));
});*/


//builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Сервис для страниц муви контроллера
builder.Services.AddControllersWithViews();

//DB:
//var connection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<MovieCatalogContext>(options => options.UseSqlServer(connection));
// эксперементальный код
var connectionStringUsers = builder.Configuration.GetConnectionString("UserDB");
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlite(connectionStringUsers));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();

var connectionStringMovies = builder.Configuration.GetConnectionString("MovieDB");
builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlite(connectionStringMovies));
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<GenresDbContext>();


var app = builder.Build();


//DB init and update:
/*using var serviceScope = app.Services.CreateScope();
var dbContext = serviceScope.ServiceProvider.GetService<MovieCatalogContext>();
dbContext?.Database.Migrate();
*/

////////////////////////////////////////////////////////////////////////////////////////////////////////////


// Configure the HTTP request pipeline.
/*if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}*/
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//app.UseStaticFiles();

//app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
*/

app.MapControllers();
app.Run();


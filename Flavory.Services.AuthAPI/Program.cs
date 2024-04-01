using Flavory.Services.AuthAPI.Data;
using Flavory.Services.AuthAPI.Models;
using Flavory.Services.AuthAPI.Services;
using Flavory.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();  //This act as a bridge between EF and .Net identity

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();


void ApplyMigration()
{
	using (var scope = app.Services.CreateScope())  //It will give us all the services
	{
		var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>(); //Now we bring this service out from all services

		if (_db.Database.GetPendingMigrations().Count() > 0)
		{
			_db.Database.Migrate(); //And finally apply changes if migration still pending
		}
	}
}

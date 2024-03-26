using AutoMapper;
using Flavory.Services.CouponAPI.Data;
using Mango.Services.CouponAPI;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//--------------------Auto Mapper-------------------
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//--------------------------------------------------


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

app.UseAuthorization();

app.MapControllers();

ApplyMigration();  // (1)

app.Run();


//  (1)  It checks if there is any pending migration, apply them
//With this method we never need to "update-database" in the command!
//whenever we start the application, it will automatically update our database and apply changes!
void ApplyMigration()
{
	using(var scope = app.Services.CreateScope())  //It will give us all the services
	{
		var _db=scope.ServiceProvider.GetRequiredService<AppDbContext>(); //Now we bring this service out from all services

		if (_db.Database.GetPendingMigrations().Count()>0)
		{
			_db.Database.Migrate(); //And finally apply changes if migration still pending
		}
	}
}

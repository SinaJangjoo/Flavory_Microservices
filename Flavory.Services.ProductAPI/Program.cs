using AutoMapper;
using Flavory.Services.CouponAPI.Extensions;
using Flavory.Services.ProductAPI.Data;
using Mango.Services.CouponAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


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
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});

// in this class in Extensions folder we define and implement authentication to avoid make this page codes dirty!
builder.AddAppAuthetication();

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
app.UseStaticFiles();  //We add this to tell the API project to read "wwwroot" folder inside that
app.MapControllers();

ApplyMigration();  // (1)

app.Run();


//  (1)  It checks if there is any pending migration, apply them
//With this method we never need to "update-database" in the command!
//whenever we start the application, it will automatically update our database and apply changes!
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

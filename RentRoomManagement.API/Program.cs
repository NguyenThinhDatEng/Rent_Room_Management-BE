using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RentContractManagement.BL.Tenant.Dictonary.ContractBL;
using RentContractManagement.DL.Tenant.Dictionary.ContractDL;
using RentRoomManagement.BL;
using RentRoomManagement.BL.auth;
using RentRoomManagement.BL.Notification;
using RentRoomManagement.BL.RoomManagement;
using RentRoomManagement.BL.RoomManagement.FeeBL;
using RentRoomManagement.BL.RoomManagement.vehicleBL;
using RentRoomManagement.BL.Tenant.Dictionary.ResidentBL;
using RentRoomManagement.BL.Tenant.Dictionary.ServiceFeeBL;
using RentRoomManagement.BL.Tenant.Dictonary.BuildingBL;
using RentRoomManagement.BL.Tenant.Dictonary.RoomBL;
using RentRoomManagement.BL.Tenant.Dictonary.RoomCategoryBL;
using RentRoomManagement.BL.Tenant.Dictonary.VehicleFeeBL;
using RentRoomManagement.BL.Tenant.RoomSearch;
using RentRoomManagement.DL;
using RentRoomManagement.DL.auth;
using RentRoomManagement.DL.Notification;
using RentRoomManagement.DL.RoomManagement;
using RentRoomManagement.DL.RoomManagement.FeeDL;
using RentRoomManagement.DL.RoomManagement.RenterDL;
using RentRoomManagement.DL.RoomManagement.VehicleDL;
using RentRoomManagement.DL.RoomSearch;
using RentRoomManagement.DL.Tenant.Dictionary.BuildingDL;
using RentRoomManagement.DL.Tenant.Dictionary.ResidentDL;
using RentRoomManagement.DL.Tenant.Dictionary.RoomCategoryDL;
using RentRoomManagement.DL.Tenant.Dictionary.RoomDL;
using RentRoomManagement.DL.Tenant.Dictionary.ServiceFeeDL;
using RentRoomManagement.DL.Tenant.Dictionary.VehicleFeeDL;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
                      policy =>
                      {
                          policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                      });
});

// Add services to the container.
// Cấu hình AddJsonOptions => Không trả về thông tin null
builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Dependency injectionGetUserOAuth2GetUserOAuth2
builder.Services.AddScoped<IAuthBL, AuthBL>();
builder.Services.AddScoped<IAuthDL, AuthDL>();

builder.Services.AddScoped<IRenterDL, RenterDL>();

builder.Services.AddScoped<INotificationBL, NotificationBL>();
builder.Services.AddScoped<INotificationDL, NotificationDL>();

#region Dictionary
builder.Services.AddScoped<IBuildingBL, BuildingBL>();
builder.Services.AddScoped<IBuildingDL, BuildingDL>();

builder.Services.AddScoped<IRoomCategoryBL, RoomCategoryBL>();
builder.Services.AddScoped<IRoomCategoryDL, RoomCategoryDL>();

builder.Services.AddScoped<IRoomBL, RoomBL>();
builder.Services.AddScoped<IRoomDL, RoomDL>();

builder.Services.AddScoped<IContractBL, ContractBL>();
builder.Services.AddScoped<IContractDL, ContractDL>();

builder.Services.AddScoped<IResidentBL, ResidentBL>();
builder.Services.AddScoped<IResidentDL, ResidentDL>();

builder.Services.AddScoped<IVehicleFeeDL, VehicleFeeDL>();
builder.Services.AddScoped<IVehicleFeeBL, VehicleFeeBL>();

builder.Services.AddScoped<IServiceFeeDL, ServiceFeeDL>();
builder.Services.AddScoped<IServiceFeeBL, ServiceFeeBL>();
#endregion

builder.Services.AddScoped<IHouseholdBL, HouseholdBL>();
builder.Services.AddScoped<IHouseholdDL, HouseholdDL>();

builder.Services.AddScoped<IVehicleDL, VehicleDL>();
builder.Services.AddScoped<IVehicleBL, VehicleBL>();

builder.Services.AddScoped<IFeeBL, FeeBL>();
builder.Services.AddScoped<IFeeDL, FeeDL>();

#region RoomSearch
builder.Services.AddScoped<IRoomPostBL, RoomPostBL>();
builder.Services.AddScoped<IRoomPostDL, RoomPostDL>();
#endregion

#region
#endregion

// Lấy connectionString từ file appsetting
DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

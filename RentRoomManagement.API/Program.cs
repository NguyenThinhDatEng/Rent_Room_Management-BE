using RentContractManagement.BL.Tenant.Dictonary.ContractBL;
using RentContractManagement.DL.Tenant.Dictionary.ContractDL;
using RentRoomManagement.BL.Tenant.Dictionary.ResidentBL;
using RentRoomManagement.BL.Tenant.Dictionary.ServiceFeeBL;
using RentRoomManagement.BL.Tenant.Dictonary.BuildingBL;
using RentRoomManagement.BL.Tenant.Dictonary.RoomBL;
using RentRoomManagement.BL.Tenant.Dictonary.RoomCategoryBL;
using RentRoomManagement.BL.Tenant.Dictonary.VehicleFeeBL;
using RentRoomManagement.BL.Tenant.RoomSearch;
using RentRoomManagement.DL;
using RentRoomManagement.DL.RoomSearch;
using RentRoomManagement.DL.Tenant.Dictionary.BuildingDL;
using RentRoomManagement.DL.Tenant.Dictionary.ResidentDL;
using RentRoomManagement.DL.Tenant.Dictionary.RoomCategoryDL;
using RentRoomManagement.DL.Tenant.Dictionary.RoomDL;
using RentRoomManagement.DL.Tenant.Dictionary.ServiceFeeDL;
using RentRoomManagement.DL.Tenant.Dictionary.VehicleFeeDL;

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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection
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

app.UseAuthorization();

app.MapControllers();

app.Run();

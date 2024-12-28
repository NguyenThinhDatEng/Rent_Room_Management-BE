using RentRoomManagement.BL.Tenant.Action;
using RentRoomManagement.BL.Tenant.Dictionary.ServiceFeeBL;
using RentRoomManagement.BL.Tenant.Dictonary.RoomBL;
using RentRoomManagement.BL.Tenant.Dictonary.RoomCategoryBL;
using RentRoomManagement.BL.Tenant.Dictonary.ServiceCategoryBL;
using RentRoomManagement.BL.Tenant.Dictonary.VehicleFeeBL;
using RentRoomManagement.BL.Tenant.RoomSearch;
using RentRoomManagement.DL;
using RentRoomManagement.DL.RoomSearch;
using RentRoomManagement.DL.Tenant.Action;
using RentRoomManagement.DL.Tenant.Dictionary.RoomCategoryDL;
using RentRoomManagement.DL.Tenant.Dictionary.RoomDL;
using RentRoomManagement.DL.Tenant.Dictionary.ServiceCategoryDL;
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
builder.Services.AddScoped<IRoomCategoryBL, RoomCategoryBL>();
builder.Services.AddScoped<IRoomCategoryDL, RoomCategoryDL>();

builder.Services.AddScoped<IRoomBL, RoomBL>();
builder.Services.AddScoped<IRoomDL, RoomDL>();

builder.Services.AddScoped<IServiceCategoryBL, ServiceCategoryBL>();
builder.Services.AddScoped<IServiceCategoryDL, ServiceCategoryDL>();

builder.Services.AddScoped<IVehicleFeeDL, VehicleFeeDL>();
builder.Services.AddScoped<IVehicleFeeBL, VehicleFeeBL>();

builder.Services.AddScoped<IServiceFeeDL, ServiceFeeDL>();
builder.Services.AddScoped<IServiceFeeBL, ServiceFeeBL>();
#endregion

#region Action
builder.Services.AddScoped<IRentingBL, RentingBL>();
builder.Services.AddScoped<IRentingDL, RentingDL>();
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

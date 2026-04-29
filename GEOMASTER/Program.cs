using GEOMASTER;
using GEOMASTER.Interface.BusinessEntity;
using GEOMASTER.Interface.BusinessUnit;
using GEOMASTER.Interface.City;
using GEOMASTER.Interface.Country;
using GEOMASTER.Interface.Department;
using GEOMASTER.Interface.Designation;
using GEOMASTER.Interface.Employee;
using GEOMASTER.Interface.Holiday;
using GEOMASTER.Interface.Menu;
using GEOMASTER.Interface.Role;
using GEOMASTER.Interface.Signup;
using GEOMASTER.Interface.State;
using GEOMASTER.Models;
using GEOMASTER.Repository;
using GEOMASTER.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//  Add services BEFORE Build()
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
//connection string
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//signup
builder.Services.AddScoped<ISignupRepository, SignupRepository>();
builder.Services.AddScoped<ISignupService, SignupService>();
//menu
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();
// country
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();
//state
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<IStateService, StateService>();
//city
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();
// business unit
builder.Services.AddScoped<IBusinessUnitRepository, BusinessUnitRepository>();
builder.Services.AddScoped<IBusinessUnitService, BusinessUnitService>();
// business entity
builder.Services.AddScoped<IBusinessEntityRepository, BusinessEntityRepository>();
builder.Services.AddScoped<IBusinessEntityService, BusinessEntityService>();
//holiday
builder.Services.AddScoped<IHolidayService, HolidayService>();
builder.Services.AddScoped<IHolidayRepository, HolidayRepository>();
// department
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
//designation
builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();
builder.Services.AddScoped<IDesignationService, DesignationService>();
//role
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
//employee
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
//  Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  CORS must be registered BEFORE calling Build()
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build(); // ❗ AFTER all services
app.UseStaticFiles(); // Must be before routing/auth
//  Middleware
app.UseHttpsRedirection();
app.UseCors("AllowAll");


// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "GeoMaster API V1");
    options.RoutePrefix = "swagger"; // serve at /swagger
});

app.UseAuthorization();
app.MapControllers();

app.Run();

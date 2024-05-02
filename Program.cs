using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using MiniDemo;
using MiniDemo.Model;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddTransient<DataSeeder>();
builder.Services.AddTransient<IDataRepository, DataRepository>();
builder.Services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwaggerUI();

if(args.Length ==1 && args[0].ToLower() == "seeddata")
	SeedData(app);



void SeedData(IHost app)
{
	var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
	
	using (var scope = scopedFactory.CreateScope())
	{
		var service = scope.ServiceProvider.GetService<DataSeeder>();
		service.Seed();
	}
}

app.UseSwagger(x => x.SerializeAsV2 = true);

app.MapGet("/", () => "Hello World!");

app.MapGet("/employee/{id}", ([FromServices] IDataRepository db, string id) =>
{
	return db.GetEmployeeById(id);
});

app.MapPut("/employee/{id}", ([FromServices] IDataRepository db, Employee employee) =>
{
	return db.PutEmployee(employee);
});

app.MapGet("/employees", ([FromServices] IDataRepository db) =>
{
	return db.GetEmployees();
});

app.MapPost("/employee", ([FromServices] IDataRepository db, Employee employee) =>
{
	return db.AddEmployee(employee);
});

app.Run();

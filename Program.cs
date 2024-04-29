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
builder.Services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

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


app.MapGet("/", () => "Hello World!");

app.MapGet("/employee/{id}", ([FromServices] EmployeeDbContext db, string id) =>
{
	return db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
});

app.MapPut("/employee/{id}", ([FromServices] EmployeeDbContext db, Employee employee) =>
{
	db.Employees.Update(employee);
	db.SaveChanges();
	return db.Employees.Where(x => x.EmployeeId == employee.EmployeeId).FirstOrDefault();
});

app.MapGet("/employees", ([FromServices] EmployeeDbContext db) =>
{
	return db.Employees.ToList();
});

app.MapPost("/employee", ([FromServices] EmployeeDbContext db, Employee employee) =>
{
	db.Employees.Add(employee);
	db.SaveChanges();
	return db.Employees.Where(x => x.EmployeeId == employee.EmployeeId).FirstOrDefault();
});

app.Run();

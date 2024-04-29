using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MiniDemo;
using MiniDemo.Model;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/employee", (Func<Employee>)(() => new Employee
{
	EmployeeId = "1",
	Name = "John Doe",
	Citizenship = "Kenya"
	
}));

// app.MapGet("/employees", (Func<List<Employee>>)(() =>
// {
// 	return new EmployeeCollection().GetEmployees();
// }));

// app.MapGet("/employees/{id}", async(http) => 
// {
// 	if (!http.Request.RouteValues.ContainsKey("id"))
// 	{
// 		http.Response.StatusCode = 400;
// 		await http.Response.WriteAsJsonAsync(new { Message = "Employee Id is required" });
// 		return;
// 	}
// 	else 
// 	{
// 		await http.Response.WriteAsJsonAsync(new EmployeeCollection()
// 											.GetEmployees()
// 											.FirstOrDefault(e => e.EmployeeId == http.Request.RouteValues["id"].ToString()));
// 	}
// });


app.Run();

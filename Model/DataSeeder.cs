namespace MiniDemo.Model;

public class DataSeeder
{
	private readonly EmployeeDbContext employeeDbContextcontext;
	
	public DataSeeder(EmployeeDbContext employeeDbContext)
	{
		this.employeeDbContextcontext = employeeDbContext;
	}

	public void Seed()
	{
		if(!employeeDbContextcontext.Employees.Any())
		{
			var employees = new List<Employee>
			{
				new Employee
				{
					EmployeeId = "1",
					Name = "John Doe",
					Citizenship = "Kenya"
				},
				new Employee
				{
					EmployeeId = "2",
					Name = "Jane Doe",
					Citizenship = "Uganda"
				}
			};
			
			employeeDbContextcontext.Employees.AddRange(employees);
			employeeDbContextcontext.SaveChanges();
		}
	}
	
}

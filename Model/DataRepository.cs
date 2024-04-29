namespace MiniDemo.Model;

public class DataRepository:IDataRepository
{
	private EmployeeDbContext db;
	public DataRepository(EmployeeDbContext db)
	{
		this.db = db;
	}
	
	public List<Employee> GetEmployees() => db.Employees.ToList();
	
	public Employee PutEmployee(Employee employee)
	{
		db.Employees.Update(employee);
		db.SaveChanges();
		return db.Employees.Where(x => x.EmployeeId == employee.EmployeeId).FirstOrDefault();
	}
	
	public List<Employee> AddEmployee(Employee employee)
	{
		db.Employees.Add(employee);
		db.SaveChanges();
		return db.Employees.ToList();
	}
	
	public Employee GetEmployeeById(string id)
	{
		return db.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
	}
}

namespace MiniDemo.Model;

public interface IDataRepository
{
	List<Employee> GetEmployees();
	Employee PutEmployee(Employee employee);
	List<Employee> AddEmployee(Employee employee);
	Employee GetEmployeeById(string id);
}

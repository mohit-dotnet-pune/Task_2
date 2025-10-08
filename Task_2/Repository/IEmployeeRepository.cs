using Task_2.Model;

namespace Task_2.Repository
{
    public interface IEmployeeRepository
    {
        public Employee GetEmployeeById(int id);
        public IEnumerable<Employee> GetAllEmployees();

        public IEnumerable<Employee> GetEmployeesByDept(string dept);
        public void AddEmploye(Employee emp);
        public void UpdateEmployee(Employee emp);
        public void DeleteEmployee(int id);
        public void UpdateEmployeeEmail(int id, string newEmail);

    }
}

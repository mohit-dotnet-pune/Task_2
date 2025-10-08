using Task_2.Model;

namespace Task_2.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static List<Employee> employees = new List<Employee>()
        {
            new Employee { Id = 1, Name = "Vaibhav", Department = "HR", MobileNo = "9876543210", Email = "vaibhav@encora.com" },
            new Employee { Id = 2, Name = "Shriyash", Department = "IT", MobileNo = "9123456780", Email = "shriyash@encora.com" }
        };
        public Employee GetEmployeeById(int id)
        {
            return employees.FirstOrDefault(emp => emp.Id == id);
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return employees;
        }

        public IEnumerable<Employee> GetEmployeesByDept(string dept)
        {
            return employees.Where(emp => emp.Department == dept).ToList();
        }
        public void AddEmploye(Employee emp)
        {
            emp.Id = employees.Max(emp => emp.Id) + 1;
            employees.Add(emp);
        }
        public void UpdateEmployee(Employee emp)
        {
            var existingEmp = GetEmployeeById(emp.Id);

            if (existingEmp != null)
            {
                existingEmp.Name = emp.Name;
                existingEmp.Department = emp.Department;
                existingEmp.MobileNo = emp.MobileNo;
                existingEmp.Email = emp.Email;
            }

        }
        public void DeleteEmployee(int id)
        {
            var existingEmp = GetEmployeeById(id);

            if (existingEmp != null)
            {
                employees.Remove(existingEmp);
            }
        }
        public void UpdateEmployeeEmail(int id, string newEmail)
        {
            var existingEmp = GetEmployeeById(id);
            if (existingEmp != null)
            {
                existingEmp.Email = newEmail;
            }
        }
    }
}

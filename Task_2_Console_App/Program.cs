

    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Task_2_Console_App.Models;

    namespace EmployeeClientApp
    {
        internal class Program
        {
            private static readonly HttpClient client = new HttpClient();

            static async Task Main(string[] args)
            {
                client.BaseAddress = new Uri("http://localhost:5277/api/Employee/"); // ⚠️ Change port if different

                Console.WriteLine("=== Employee API Client ===");

                while (true)
                {
                    Console.WriteLine("\nMenu:");
                    Console.WriteLine("1. Get All Employees");
                    Console.WriteLine("2. Get Employee By ID");
                    Console.WriteLine("3. Add Employee");
                    Console.WriteLine("4. Update Employee");
                    Console.WriteLine("5. Delete Employee");
                    Console.WriteLine("6. Update Employee Email");
                    Console.WriteLine("7. Exit");
                    Console.WriteLine("8. Get Employees By Department");
                    Console.Write("Enter your choice: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            await GetAllEmployees();
                            break;
                        case "2":
                            await GetEmployeeById();
                            break;
                        case "3":
                            await AddEmployee();
                            break;
                        case "4":
                            await UpdateEmployee();
                            break;
                        case "5":
                            await DeleteEmployee();
                            break;
                        case "6":
                            await UpdateEmployeeEmail();
                            break;
                        case "7":
                            return;
                        case "8":
                            await GetEmployeesByDept();
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
            }

            static async Task GetAllEmployees()
            {
                var response = await client.GetAsync("");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

            static async Task GetEmployeeById()
            {
                Console.Write("Enter Employee ID: ");
                var id = Console.ReadLine();
                var response = await client.GetAsync(id);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

            static async Task AddEmployee()
            {
                Employee emp = new Employee();
                Console.Write("Enter Name: ");
                emp.Name = Console.ReadLine();
                Console.Write("Enter Department: ");
                emp.Department = Console.ReadLine();
                Console.Write("Enter Mobile No: ");
                emp.MobileNo = Console.ReadLine();
                Console.Write("Enter Email: ");
                emp.Email = Console.ReadLine();

                var json = JsonConvert.SerializeObject(emp);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("", content);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

            static async Task UpdateEmployee()
            {
                Employee emp = new Employee();
                Console.Write("Enter Employee ID: ");
                emp.Id = int.Parse(Console.ReadLine());
                Console.Write("Enter New Name: ");
                emp.Name = Console.ReadLine();
                Console.Write("Enter New Department: ");
                emp.Department = Console.ReadLine();
                Console.Write("Enter New Mobile No: ");
                emp.MobileNo = Console.ReadLine();
                Console.Write("Enter New Email: ");
                emp.Email = Console.ReadLine();

                var json = JsonConvert.SerializeObject(emp);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{emp.Id}", content);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

            static async Task DeleteEmployee()
            {
                Console.Write("Enter Employee ID: ");
                var id = Console.ReadLine();

                var response = await client.DeleteAsync(id);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

            static async Task UpdateEmployeeEmail()
            {
                Console.Write("Enter Employee ID: ");
                var id = Console.ReadLine();
                Console.Write("Enter New Email: ");
                var newEmail = Console.ReadLine();

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{id}/email?email={newEmail}");

                var response = await client.SendAsync(request);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

            static async Task GetEmployeesByDept()
            {
                Console.Write("Enter Department Name: ");
                var dept = Console.ReadLine();

                var response = await client.GetAsync($"byDept/{dept}");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Employees in " + dept + " Department:");
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }


        }
    }

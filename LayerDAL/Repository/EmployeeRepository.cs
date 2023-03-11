using LayerDAL.Entities;
using LayerDAL.Setting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerDAL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConnectionSetting _connection;

        public EmployeeRepository(IOptions<ConnectionSetting> connection)
        {
            _connection=connection.Value;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> ListEmployees = new List<Employee>();
            using (var conn = new SqlConnection(_connection.SQLString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM EMPLOYEES", conn);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while ( await reader.ReadAsync()) {
                        ListEmployees.Add(new Employee()
                        {
                            Id= Guid.Parse(reader["Id"].ToString()),
                            Name=reader["Name"].ToString(),
                            Email=reader["Email"].ToString(),
                            Phone=reader["Phone"].ToString(),
                            Department=reader["Email"].ToString(),
                            Salary= Convert.ToDecimal(reader["Salary"])

                        });


                    }
                }


            }
            return ListEmployees;

        }
        public async void AddEmp(Employee employee)
        {
            using (var conn = new SqlConnection(_connection.SQLString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO EMPLOYEES VALUES('"+employee.Id+"','"+employee.Name+"','"+employee.Email+"','"+employee.Phone+"','"+employee.Salary+"','"+employee.Department+"')", conn);
               await cmd.ExecuteNonQueryAsync();
                conn.Close();
            }
    }
}}

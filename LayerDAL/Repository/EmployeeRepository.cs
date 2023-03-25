using LayerDAL.Entities;
using LayerDAL.Setting;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

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
                            Department=reader["Department"].ToString(),
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
        public async Task<Employee> EditEmp(Guid id)
        {
            var employee = new Employee();
            using (SqlConnection con = new SqlConnection(_connection.SQLString))
            {
                con.Open();
                SqlCommand objSqlCommand = new SqlCommand("select * from employees where id='" + id + "'", con);
                try
                {
                    using (var reader = await objSqlCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employee = new Employee()
                            {
                                Id= Guid.Parse(reader["Id"].ToString()),
                                Name=reader["Name"].ToString(),
                                Email=reader["Email"].ToString(),
                                Phone=reader["Phone"].ToString(),
                                Department=reader["Department"].ToString(),
                                Salary= Convert.ToDecimal(reader["Salary"])
                            };
                        }
                    }
                }

                catch (Exception)
                {
                    con.Close();
                }
            }
            return employee;
        }
        public async void UpdateEmp(Employee employee)
        {
            using (var conn = new SqlConnection(_connection.SQLString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Employees SET name = '"+employee.Name+"', Email = '"+employee.Email+"',phone='"+employee.Phone+"',salary='"+employee.Salary+"',Department='"+employee.Department+"' WHERE Id = '"+employee.Id+"'", conn);
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
            }
        }
        public async void DeleteEmp(Guid id)
        {
            using (var conn = new SqlConnection(_connection.SQLString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Employees  WHERE Id = '"+id+"'", conn);
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
            }
        }
    }
}

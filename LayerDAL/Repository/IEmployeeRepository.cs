using LayerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerDAL.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
        void AddEmp(Employee employee);
        Task<Employee> EditEmp(Guid id);
        void UpdateEmp(Employee employee);
        void DeleteEmp(Guid id);
        
    }
}

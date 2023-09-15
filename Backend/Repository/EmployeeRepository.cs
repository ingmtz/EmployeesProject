using EmployeesBE.Data;
using EmployeesBE.Models;
using EmployeesBE.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EmployeesBE.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _db.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _db.Employees.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(Employee employee)
        {
            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _db.Remove(employee);
            await _db.SaveChangesAsync();
        }
    }
}

using Task.Domain.Dto;
using Task.Domain.Helpers;
using Task.Domain.Models;

namespace Task.Domain.Interfaces.Repos
{
    public interface IDriverRepository
    {
        DBSetting _dbSettings { get; }
        string CreateAsync(DriverDto entity);
        string DeleteAsync(int id);
        string UpdateAsync(DriverModel entity);
        DriverModel GetbyIdAsync(int id);
        IEnumerable<DriverModel> GetAllAsync(int page, int size);
        
    }
}

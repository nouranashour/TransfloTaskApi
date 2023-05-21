using Task.Domain.Custom;
using Task.Domain.Dto;
using Task.Domain.Models;

namespace Task.Application.Interfaces.Services
{
    public interface IDriverService
    {
        Response CreateDriver(DriverDto entity);
        Response GetAllDriver(int page, int size);
        Response UpdateDriver(DriverModel dto);
        Response DeleteDriver(int id);
        Response GetbyId(int id);         
        Response GenerateRandomDrivers(int count);
        Response AlphapetizedNames(string fullname);
        Response GetAlphapetizeDrivers(int page, int size);
    }
}

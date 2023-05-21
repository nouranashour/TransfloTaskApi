using Microsoft.AspNetCore.Mvc;
using Task.Application.Interfaces.Services;
using Task.Domain.Dto;
using Task.Domain.Models;

namespace Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : BaseApiController
    {
        public DriverController(IDriverService driverService) : base(driverService)
        {
        }

        // GET: DriverController
        [HttpGet("/list")]
        public async Task<IActionResult> GetAllAsync(int page, int size)
        {
            var response = _driverService.GetAllDriver(page, size);
            return SendAppropirateResponse(response);
        }

        [HttpPost("/Create")]
        public async Task<IActionResult> CreateDriver(DriverDto dto)
        {
            var response = _driverService.CreateDriver(dto);
            return SendAppropirateResponse(response);
        }

        [HttpPut("/Update")]
        public async Task<IActionResult> UpdateDriver(DriverModel dto)
        {
            var response = _driverService.UpdateDriver(dto);
            return SendAppropirateResponse(response);
        }
        [HttpGet("/GetbyId")]
        public async Task<IActionResult> GetDriverbyId(int id)
        {
            var response = _driverService.GetbyId(id);
            return SendAppropirateResponse(response);
        }
        [HttpDelete("/Delete")]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var response = _driverService.DeleteDriver(id);
            return SendAppropirateResponse(response);
        }

        [HttpGet("/InsertBulk")]
        public IActionResult AddBulkOfDrivers(int count)
        {
            var response = _driverService.GenerateRandomDrivers(count);
            return SendAppropirateResponse(response);
        }

        [HttpGet("/NameAlphapetized")]
        public IActionResult AlphapetizedName(string fullname)
        {
            var response = _driverService.AlphapetizedNames(fullname);
            return SendAppropirateResponse(response);
        }
      
        [HttpGet("/AlphapetizeDriversList")]
        public IActionResult GetAlphapetizeDrivers(int page, int size)
        {
            var response = _driverService.GetAlphapetizeDrivers( page,  size);
            return SendAppropirateResponse(response);
        }

    }
}

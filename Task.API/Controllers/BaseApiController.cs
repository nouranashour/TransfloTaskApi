using Microsoft.AspNetCore.Mvc;
using Task.Application.Interfaces.Services;
using Task.Domain.Custom;
using Task.Infra.Constants;

namespace Task.API.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected IDriverService _driverService;
        public BaseApiController(IDriverService driverService)
        {
            _driverService = driverService;
        }
        protected IActionResult SendAppropirateResponse(Response response)
        {
            switch (response.HttpResponse)
            {
                case HttpResponseCustom.BadRequest:
                    return BadRequest(response);
                case HttpResponseCustom.NotFound:
                    return NotFound(response);
                case HttpResponseCustom.Unauthorized:
                    return Unauthorized(response);
                case HttpResponseCustom.InternalServerError:
                    return Unauthorized(response);

            }

            return Ok(response.Data);
        }
    }
}

using System;
using System.Text;
using System.Xml.Linq;
using Task.Application.Interfaces.Services;
using Task.Domain.Custom;
using Task.Domain.Dto;
using Task.Domain.Interfaces.Repos;
using Task.Domain.Models;
using Task.Infra.Constants;

namespace Task.Application.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _repository;
        private static readonly Random _random = new Random();
        public DriverService(IDriverRepository repository)
        {
            _repository = repository;

        }

          public Response AlphapetizedNames(string fullName)
          {

              string[] Names = fullName.Split(' ');
              if (Names.Length > 0)
              {
                   StringBuilder alphabetizedbuiltString = new StringBuilder();
                   for (int i = 0; i < Names.Length; i++)
                   {
                          string alphabetizedName = new string(Names[i].OrderBy(c => c.ToString(), StringComparer.OrdinalIgnoreCase).ToArray());
                          alphabetizedbuiltString.Append(alphabetizedName);
                          if(i < Names.Length - 1)
                          alphabetizedbuiltString.Append(' ');
                   }
                  return new Response
                  {
                      Data = new {fullName = alphabetizedbuiltString.ToString() },
                      HttpResponse = HttpResponseCustom.OK,
                      Message = "Name Alphapetized Successfully",
                      IsSuccessed = true

                  };
              }
              return new Response
              {
                  Data = fullName,
                  HttpResponse = HttpResponseCustom.BadRequest,
                  Message = "Invalid Name",
                  IsSuccessed = false

              };
          }
        public Response CreateDriver(DriverDto entity)
        {
            var result = _repository.CreateAsync(entity);
            if (result == ((int)ReturnCode.Success).ToString())
                return new Response
                {
                    IsSuccessed = true,
                    Data = entity,
                    HttpResponse = HttpResponseCustom.OK,
                    Message = "Driver Added Successfully.",
                };
            else if (result == ((int)ReturnCode.ExistingEmail).ToString())
                return new Response
                {
                    IsSuccessed = false,
                    HttpResponse = HttpResponseCustom.BadRequest,
                    Message = "This Driver Email Already Registered.",
                };


            return new Response
            {
                IsSuccessed = false,
                HttpResponse = HttpResponseCustom.NotFound,
            };
        }

        public Response DeleteDriver(int id)
        {
            var result = _repository.DeleteAsync(id);
            if(result == ((int)ReturnCode.Success).ToString())
               return new Response
               {
                   IsSuccessed = true, HttpResponse = HttpResponseCustom.OK,
                   Message= "Driver Deleted Successfully.", 
               };
           else if (result == ((int)ReturnCode.NotExisitingRecord).ToString())
                return new Response
            {
                IsSuccessed = false,
                HttpResponse = HttpResponseCustom.BadRequest,
                Message = "This Driver Does Not Exist.",
            };


            return new Response
            {
                IsSuccessed = false,
                HttpResponse = HttpResponseCustom.NotFound,
            };
        }
        public Response GetbyId(int id)
        {
            var result = _repository.GetbyIdAsync(id);
            if (result != null)
            {
                return new Response
                {
                    Data = result,
                    IsSuccessed = true,
                    Message = "Data Returned Successfully.",
                    HttpResponse = Infra.Constants.HttpResponseCustom.OK
                };
            }
            return new Response
            {
                IsSuccessed = false,
                Message = "No Data existing.",
                HttpResponse = Infra.Constants.HttpResponseCustom.NotFound
            };
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public Response GenerateRandomDrivers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var item = new DriverDto();
                item.FirstName = GenerateRandomString(_random.Next(5, 10));
                item.LastName = GenerateRandomString(_random.Next(5, 10));
                item.Email = $"{item.FirstName}.{item.LastName}{i + 1}@gmail.com";
                item.PhoneNumber = "+201155478965";
                var result = _repository.CreateAsync(item);
                if (result == ((int)ReturnCode.ExistingEmail).ToString())
                    return new Response
                    {
                        IsSuccessed = false,
                        HttpResponse = HttpResponseCustom.BadRequest,
                        Message = "This Driver Email Already Registered.",
                    };

            }

            return new Response
            {
                IsSuccessed = true,
                HttpResponse = HttpResponseCustom.OK,
                Message = "Drivers Added Successfully.",
            };
        }

        /*public Response GenerateRandomDrivers(int count)
        {

            var names = new List<string>();
            for (int i = 0; i < count; i++)
            {
                var firstName = GenerateRandomString(_random.Next(5, 10));
                var lastName = GenerateRandomString(_random.Next(5, 10));
                names.Add($"{firstName} {lastName}");
            }

            Random rand = new Random(DateTime.Now.Second);
            string[] maleNames = {"Ahmed","Ali","Ashour","Marwan","Mohamed", "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolfo", "adolph", "adrian" };
            string[] femaleNames = {"Nouran","Aya","Gana","Marwa","Lobna","Fatma","Farida","Eman","Wafaa", "abby", "abigail", "adele", "adrian" };
            string[] lastNames = {"Michal","Saif","Omar","Mahmoud","Waleed","wael", "abbott", "acosta", "adams", "adkins", "aguilar" };

            for (int i = 0; i < count; i++)
            {
                int indexM = rand.Next(maleNames.Length);
                int indexF = rand.Next(femaleNames.Length);
                int indexL = rand.Next(lastNames.Length);
                var item = new DriverDto();
                item.FirstName = (i % 2 == 0)? maleNames[indexM] : femaleNames[indexF];
                item.LastName = lastNames[indexL];
                item.Email = $"{item.FirstName}.{item.LastName}{i+1}@gmail.com";
                item.PhoneNumber = "+201155478965";
                var result = _repository.CreateAsync(item);
                if (result == ((int)ReturnCode.ExistingEmail).ToString())
                    return new Response
                    {
                        IsSuccessed = false,
                        HttpResponse = HttpResponseCustom.BadRequest,
                        Message = "This Driver Email Already Registered.",
                    };

            }

            return new Response
            {
                IsSuccessed = true,
                HttpResponse = HttpResponseCustom.OK,
                Message = "Drivers Added Successfully.",
            };
        }*/

        public Response GetAllDriver(int page, int size)
        {
            var result = _repository.GetAllAsync( page,  size);
            if (result != null)
            {
                result =result.OrderBy(u => u.FirstName).ThenBy(x=>x.LastName);
                return new Response
                {
                    Data = result,
                    IsSuccessed = true,
                    Message = "Data Returned Successfully.",
                    HttpResponse = Infra.Constants.HttpResponseCustom.OK
                };
            }
            return new Response
            {
                IsSuccessed = false,
                Message = "No Data existing.",
                HttpResponse = Infra.Constants.HttpResponseCustom.NotFound
            };
        }

        public Response GetAlphapetizeDrivers(int page, int size)
        {
            var result = _repository.GetAllAsync(page, size);
            if (result != null)
            {
                var Drivers = result.OrderBy(u => u.FirstName).ThenBy(x => x.LastName).ToList();
                foreach (var item in Drivers)
                {
                    item.FirstName = new string(item.FirstName.OrderBy(c => c.ToString(), StringComparer.OrdinalIgnoreCase).ToArray());
                    item.LastName = new string(item.LastName.OrderBy(c => c.ToString(), StringComparer.OrdinalIgnoreCase).ToArray());
                }
                return new Response
                {
                    Data = Drivers,
                    IsSuccessed = true,
                    Message = "Data Returned Successfully.",
                    HttpResponse = Infra.Constants.HttpResponseCustom.OK
                };
            }
            return new Response
            {
                IsSuccessed = false,
                Message = "No Data existing.",
                HttpResponse = Infra.Constants.HttpResponseCustom.NotFound
            };
        }
        public Response UpdateDriver(DriverModel dto)
        {
            var result = _repository.UpdateAsync(dto);
            if (result == ((int)ReturnCode.Success).ToString())
                return new Response
                {
                    IsSuccessed = true,
                    Data = dto,
                    HttpResponse = HttpResponseCustom.OK,
                    Message = "Driver Updated Successfully.",
                };
            else if (result == ((int)ReturnCode.NotExisitingRecord).ToString())
                return new Response
                {
                    IsSuccessed = false,
                    HttpResponse = HttpResponseCustom.BadRequest,
                    Message = "This Driver Does Not Exist.",
                };


            return new Response
            {
                IsSuccessed = false,
                HttpResponse = HttpResponseCustom.NotFound,
            };
        }
    }
}

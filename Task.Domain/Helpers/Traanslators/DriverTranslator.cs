
using System.Data.SqlClient;

using Task.Domain.Models;

namespace Task.Domain.Helpers.Traanslators
{
    public static class DriverTranslator
    {
        public static DriverModel TranslateAsDriver(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new DriverModel();
            if (reader.IsColumnExists("Id"))
                item.Id = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("FirstName"))
                item.FirstName = SqlHelper.GetNullableString(reader, "FirstName");

            if (reader.IsColumnExists("LastName"))
                item.LastName = SqlHelper.GetNullableString(reader, "LastName");

            if (reader.IsColumnExists("Email"))
                item.Email = SqlHelper.GetNullableString(reader, "Email");

            if (reader.IsColumnExists("PhoneNumber"))
                item.PhoneNumber = SqlHelper.GetNullableString(reader, "PhoneNumber");

         

            return item;
        }

        public static List<DriverModel> TranslateAsDriverList(this SqlDataReader reader)
        {
            var list = new List<DriverModel>();
            while (reader.Read())
            {
                list.Add(TranslateAsDriver(reader, true));
            }
            return list;
        }
       
    }
}

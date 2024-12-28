using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Functions
{
    public class BuildQuery
    {
        public static string TableNameMapper<T>()
        {
            var type = typeof(T);
            TableAttribute tableAttribute = (TableAttribute)Attribute.GetCustomAttribute(type, typeof(TableAttribute));
            var name = string.Empty;

            if (tableAttribute != null)
            {
                name = tableAttribute.Name;
            }

            return name;
        }
    }
}

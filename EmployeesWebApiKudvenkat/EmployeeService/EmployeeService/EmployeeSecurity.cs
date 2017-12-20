using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace EmployeeService
{
    public class EmployeeSecurity
    {
       public static bool Login(string un, string pw)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Users.Any(user => user.UserName.Equals
                (un, StringComparison.OrdinalIgnoreCase) && user.Password == pw);
            }
        }
    }
}
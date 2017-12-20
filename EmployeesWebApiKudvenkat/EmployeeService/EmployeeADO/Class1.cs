using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeDataAccess;
using System.Data.SqlClient;
using System.Configuration;

namespace ADO
{
    public class EmployeeADO
    {
        string connString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        public bool InsertEmployee(Employee employee)
        {
            SqlConnection objConnection = new SqlConnection();
            try
            {
                objConnection.ConnectionString = connString;

            }

            catch(Exception ex)
            {

            }
            objConnection.Open();

            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = objConnection;
            objCommand.CommandType = System.Data.CommandType.Text;

            objCommand.CommandText = "Insert into Employees (FirstName, LastName, Salary, Gender) values('" + employee.FirstName + "', '" + employee.LastName + "'," + employee.Salary + ", '" + employee.Gender + "')";
            if( objCommand.ExecuteNonQuery() > 0)
            {
                objConnection.Close();
                return true;
            }
            objConnection.Close();
            return false;
        }

        public bool GetAllEmployee(Employee employee)
        {
            SqlConnection objConnection = new SqlConnection();
            try
            {
                objConnection.ConnectionString = connString;

            }

            catch (Exception ex)
            {

            }
            objConnection.Open();

            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = objConnection;
            objCommand.CommandType = System.Data.CommandType.Text;

            objCommand.CommandText = "Insert into Employees (FirstName, LastName, Salary, Gender) values('" + employee.FirstName + "', '" + employee.LastName + "'," + employee.Salary + ", '" + employee.Gender + "')";
            if (objCommand.ExecuteNonQuery() > 0)
            {
                objConnection.Close();
                return true;
            }
            objConnection.Close();
            return false;
        }
    }
}

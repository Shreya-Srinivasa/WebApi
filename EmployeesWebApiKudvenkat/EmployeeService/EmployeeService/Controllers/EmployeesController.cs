 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;
using System.Threading;
using ADO;
//using System.Web.Http.Cors;

namespace EmployeeService.Controllers
{
    //[EnableCorsAttribute("*","*","*")]
    public class EmployeesController : ApiController
    {
        [HttpGet]
        public List<Employee> LoadAllEmployees()
        {

            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Employees.ToList();
            }
        }


    

        [HttpGet]
        public HttpResponseMessage LoadAllEmployeesById(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {

                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }

                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id = " + id.ToString() + "not found");
                }
            }
        }

        //public HttpResponseMessage Get(string gender = "All")
        //{
        //    using (EmployeeDBEntities entities = new EmployeeDBEntities())
        //    {
        //        string username = Thread.CurrentPrincipal.Identity.Name;
        //        switch (gender.ToLower())
        //        {

        //            case "male":
        //                return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.ToLower() == "male").ToList());

        //            case "female":
        //                return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());

        //            default:
        //                return Request.CreateResponse(HttpStatusCode.BadRequest);
        //        }

        //    }
        //}

        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    //entities.Employees.Add(employee);
                    //entities.SaveChanges();

                    EmployeeADO ADOemployee = new EmployeeADO();
                    if(ADOemployee.InsertEmployee(employee))
                    {
                        var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                        message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                        return message;
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed");

                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The employee with ID = " + entity + " was not found to delete.");
                    }

                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody]int id, [FromUri]Employee employee)
            {
                try
                {
                    using (EmployeeDBEntities entities = new EmployeeDBEntities())
                    {
                        var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                        if (entity == null)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The employee " + entity + " was not found to update");
                        }
                        else
                        {
                            entity.FirstName = employee.FirstName;
                            entity.LastName = employee.LastName;
                            entity.Gender = employee.Gender;
                            entity.Salary = employee.Salary;

                            entities.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, entity);
                        }

                    }

                }
                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }

            }

    }
}


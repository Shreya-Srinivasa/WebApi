using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace EmployeeService.Controllers
{
    public class EmployeeEvaluationController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return entities.Employees.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.ToList().Where(e => e.ID == id);
                    return Request.CreateResponse(HttpStatusCode.OK, entity);

                }
            }

            catch(Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "employee whose id is " + id + " could not be found to delete!");
            }
           
        }

        public HttpResponseMessage Post(Employee employee)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                try
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }

                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
               
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.Where(e => e.ID == id).FirstOrDefault();

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The employee with id as " + id + " was not found to delete");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);

                    }
                }
            }

            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody]Employee employee,[FromUri] int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                try
                {
                    Employee DBemployee = entities.Employees.Where(e => e.ID == id).FirstOrDefault();
                    if (employee == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NoContent, "The employee details were not mentioned. Please mention them.");
                    }
                    else
                    {
                        DBemployee.FirstName = employee.FirstName;
                        DBemployee.LastName = employee.LastName;
                        DBemployee.Gender = employee.Gender;
                        DBemployee.Salary = employee.Salary;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);

                    }
                }

                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }

            }
        }
    }
}

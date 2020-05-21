using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Http;
using EmployeesDataAccess;

namespace WebApiProjectDemo.Controllers
{
    public class EmployeeController : ApiController
    {
        //When we create the ADO class and had it create the model from our database
        //VS automatically create several class for us in the background including the
        //EmployeeDBEntities which handles the connections to our database

        //When the controller gets the data from the database, the job of the controller is done
        //it then sends the data to the API pipeline and depending on the formant the client has requested
        //it will then format the data according i.e. XML, JSON
        public HttpResponseMessage Get(string gender = "All")
        {
            var employees = new EmployeeDBEntities();
            switch (gender.ToLower())
            {
                case "all":
                    return Request.CreateResponse(HttpStatusCode.OK, employees);
                case "male":
                    return Request.CreateResponse(HttpStatusCode.OK,
                        employees.Employees.Where(e => e.Gender == "male"));
                case "female":
                    return Request.CreateResponse(HttpStatusCode.OK,
                        employees.Employees.Where(e => e.Gender == "female"));
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Value for gender must be all, male, or female, {gender} is invalid.");
                    

            }
        }

        //By default, if client request an id that doesn't exist, it will response with a status code
        //of ok but the rest standards want use to return a status code of 404 not found.
        public HttpResponseMessage Get(int id)
        {
            var employee = new EmployeeDBEntities().Employees.FirstOrDefault(e => e.ID == id);
            //Simple lambda expression where we return the employee if in matches the id
            //in the parameter or the default value of the type
            //If it is an object type, it will be null, int it will be 0, etc.
            if (employee != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with {id} not found");
            }
        }

        //When a client makes a request, it will contain a header, uri, and body.
        //We are tell the web api that the employee object is going to come from the
        //body.
        //According to the rest services, when we create a new object, we need to return
        //the correct status code as well as the location of the newly created item.

        //If a return type is void, by default it will return a status code of 204 No Content
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                EmployeeDBEntities entities = new EmployeeDBEntities();
                entities.Employees.Add(employee);
                entities.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                return message;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Delete(int id)
        {

            try
            {
                EmployeeDBEntities entities = new EmployeeDBEntities();
                var employee = entities.Employees.FirstOrDefault(e => e.ID == id);
                if (employee != null)
                {
                    entities.Employees.Remove(employee);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with {id} not found");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

        }
        public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        {
            try
            {
                EmployeeDBEntities entities = new EmployeeDBEntities();
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.ID = employee.ID;
                    entity.Gender = employee.Gender;
                    entity.Salary = employee.Salary;
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with {id} not found");
                }
                
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}

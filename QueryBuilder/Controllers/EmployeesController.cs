using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using EmployeeDataAccess;
using QueryBuilder.Models;
using QueryBuilder.Services;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Web.Http.Tracing;


namespace QueryBuilder.Controllers
{
    [Authorize]
    public class EmployeesController : ApiController
    {
        

        private IEmpRepository<Employee> interfaceobj;
        public EmployeesController()
        {
            this.interfaceobj = new EmpRepository<Employee>();
        }
        
 EmployeeDBEntities entities = new EmployeeDBEntities();
        public IEnumerable<Employee> Get()
        {
           

            return entities.Employees.ToList();
        }

        public Employee Get(int id)
        {
            

            return entities.Employees.FirstOrDefault(e => e.ID == id);
        }
       
        [Route("api/employees/ColumnNames")]
        [Authorize]
        public IEnumerable<Columns> GetColumnNames()
        {
            List<Columns> column = new List<Columns>();
            var type = typeof(Employee);

            var metadata = ((IObjectContextAdapter)entities).ObjectContext.MetadataWorkspace;

            // Get the part of the model that contains info about the actual CLR types
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get the entity type from the model that maps to the CLR type
            var entityType = metadata
                                .GetItems<EntityType>(DataSpace.OSpace)
                                      .Single(e => objectItemCollection.GetClrType(e) == type);

            // Get the entity set that uses this entity type
            var entitySet = metadata
                            .GetItems<EntityContainer>(DataSpace.CSpace)
                                  .Single()
                                  .EntitySets
                                  .Single(s => s.ElementType.Name == entityType.Name);

            // Find the mapping between conceptual and storage model for this entity set
            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                                     .Single()
                                     .EntitySetMappings
                                     .Single(s => s.EntitySet == entitySet);

            // Find all properties (column) that are mapped
            var columnName = mapping
                           .EntityTypeMappings.Single()
                           .Fragments.Single()
                           .PropertyMappings
                           .OfType<ScalarPropertyMapping>()
                           .Select(t => t.Property.Name).ToList();
            foreach(var x in columnName)
            {
                column.Add(new Columns()
                {
                    columnname=x,
                    columnvalue=x
                });
            }

            return column;
        }

        [Route("api/employees/CategoryList")]
        [AllowAnonymous]
        public HttpResponseMessage getCategories()
        {
           
            string jsonFile = @"C:\WebApiProject\QueryBuilder\QueryBuilder\json\categories.json";
            var json = File.ReadAllText(jsonFile);
            List<Categories> categoryList = new List<Categories>();

             categoryList = JsonConvert.DeserializeObject<List<Categories>>(json);

            return Request.CreateResponse(HttpStatusCode.OK, categoryList);

            
        }
        [Route("api/employees/CategoryListCopy")]
        [AllowAnonymous]
        public IHttpActionResult getCategoriesCopy()
        {
            SimpleTracer s = new SimpleTracer();
            s.Trace(Request, "EmployeeController", TraceLevel.Error, "getCategoriesCopy starts here");
            
            string jsonFile = @"C:\WebApiProject\QueryBuilder\QueryBuilder\json\categories.json";
                var json = File.ReadAllText(jsonFile);
                List<Categories> categoryList = new List<Categories>();

                categoryList = JsonConvert.DeserializeObject<List<Categories>>(json);
            s.Trace(Request, "", TraceLevel.Info, "getCategoriesCopy ends here");
            return Ok(categoryList);

            
            
            


        }

        [Route("api/employees/SeedsByCategory/{categoryid}")]
        [AllowAnonymous]
        public HttpResponseMessage getSeedsByCategory(string categoryid)
        {
            string jsonFile = @"C:\WebApiProject\QueryBuilder\QueryBuilder\json\seeds.json";
            var json = File.ReadAllText(jsonFile);
            List<Seeds> seedsList = new List<Seeds>();

            var list = JsonConvert.DeserializeObject<List<Seeds>>(json);
            seedsList = (from x in list where (x.categoryId == categoryid)
                        select x).ToList<Seeds>() ;

            return Request.CreateResponse(HttpStatusCode.OK,seedsList);


        }

        public IEnumerable<Employee> Post([FromBody]List<EmployeeQuery> querycondition)
        {
            string whereclause = "";
            foreach(var x in querycondition)
            {
                whereclause = whereclause+" "+x.andor+" "+x.column+" "+x.condition+" "+x.value;
            }
            if(whereclause.Trim()!="")
            whereclause = " "+"where"+" "+whereclause;
            
            string query = "Select * From Employees" + whereclause;
           // string query = "Select * From Employees";
            //var query = entities.Employees.Where(whereclause);
            var resultobj=entities.Database.SqlQuery<Employee>(query).ToArray();

           
            return resultobj;
        }

    }
}

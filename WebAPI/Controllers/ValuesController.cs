using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        static List<string> stringList = new List<string>()
        {
            "item1","item1","item1","item1"
        };

        // GET api/values
        public IEnumerable<string> Get()
        {
            return stringList;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return stringList[id];
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            stringList.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            stringList[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            stringList.RemoveAt(id);
        }
    }
}

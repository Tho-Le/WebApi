using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiProjectDemo.Controllers
{
    public class ValuesController : ApiController
    {
        private List<string> _jokesList = new List<string>()
        {
            "jokes", "jokes2", "jokes3"
        };
        
        // GET api/values
        public IEnumerable<string> Get()
        {
            return _jokesList;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return _jokesList[id];
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            _jokesList.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            _jokesList[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _jokesList.RemoveAt(id);
        }
    }
}

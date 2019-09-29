using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Server.Controllers
{
    [RoutePrefix("api/users")]
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class UserController : ApiController
    {
        // GET: api/User/5
        [HttpGet]
        public string Get(int id)
        {
            System.Diagnostics.Debug.WriteLine("HTTP GET");
            return "value";
        }
        
        // POST: api/User
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }

        [HttpGet]
        public Weapon GetPistol()
        {
            return Weapon.Builder.GetInstance()
                .setName("TestPistol")
                .build();
        }
    }
}

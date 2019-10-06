using Server.Game.Bonuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Server.Controllers
{
    [RoutePrefix("api/bonuses")]
    public class BonusController : ApiController
    {
        [HttpGet]
        [Route("health")]
        public HttpResponseMessage CreateHealthBonus()
        {
            var bonus = BonusFactory.GetBonus(BonusType.Health);
            bonus.ApplyBonus(null);
            return Request.CreateResponse(HttpStatusCode.OK, "Created health bonus");
        }

        [HttpGet]
        [Route("ammo")]
        public HttpResponseMessage CreateAmmoBonus()
        {
            var bonus = BonusFactory.GetBonus(BonusType.Ammo);
            bonus.ApplyBonus(null);
            return Request.CreateResponse(HttpStatusCode.OK, "Created ammo bonus");
        }

        [HttpGet]
        [Route("speed")]
        public HttpResponseMessage CreateSpeedBonus()
        {
            var bonus = BonusFactory.GetBonus(BonusType.Speed);
            bonus.ApplyBonus(null);
            return Request.CreateResponse(HttpStatusCode.OK, "Created speed bonus");
        }
    }
}

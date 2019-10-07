using Server.Game.Bonuses;
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
            var bonus = new HealthFactory().GetBonus();
            bonus.ApplyBonus(null);
            return Request.CreateResponse(HttpStatusCode.OK, "Created health bonus");
        }

        [HttpGet]
        [Route("ammo")]
        public HttpResponseMessage CreateAmmoBonus()
        {
            var bonus = new AmmoFactory().GetBonus();
            bonus.ApplyBonus(null);
            return Request.CreateResponse(HttpStatusCode.OK, "Created ammo bonus");
        }

        [HttpGet]
        [Route("speed")]
        public HttpResponseMessage CreateSpeedBonus()
        {
            var bonus = new SpeedFactory().GetBonus();
            bonus.ApplyBonus(null);
            return Request.CreateResponse(HttpStatusCode.OK, "Created speed bonus");
        }
    }
}

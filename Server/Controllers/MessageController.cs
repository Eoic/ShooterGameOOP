using Server.Game;
using Server.Network;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.WebSockets;

namespace Server.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    [RoutePrefix("api/messages")]
    public class MessageController : ApiController
    {
        // Upgrade connection from HTTP to WebSocket on connection request.
        [Route("")]
        public HttpResponseMessage Get()
        {
            var currentContext = HttpContext.Current;

            if (currentContext.IsWebSocketRequest || currentContext.IsWebSocketRequestUpgrading)
                currentContext.AcceptWebSocketRequest(ProcessWebSocketSession);

            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        } 

        // Create and return web socket connections handler.
        private Task ProcessWebSocketSession(AspNetWebSocketContext context)
        {
            var handler = new Client();
            handler.Attach(GameManagerWrapper.GetInstance().GetGameManager());
            var processTask = handler.ProcessWebSocketRequestAsync(context);
            return processTask;
        }
    }
}

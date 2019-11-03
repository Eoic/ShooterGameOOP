using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Game.Entities;

namespace Server.Game.Physics
{
    public interface ICollider
    {
        bool IsColliding(GameObject subject, GameObject obstacle);
        bool WillCollide(Vector nextPosition, GameObject obstacle);
        bool IsPositionValid(Vector nextPosition);
        void Move(Vector change, GameObject subject);
    }
}

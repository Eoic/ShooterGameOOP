using Server.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Game.Physics
{
    public abstract class Collider
    {
        public virtual bool ProcessMotion(long delta, GameObject subject)
        {
            var change = GetPositionChange(subject, delta);
            var nextPosition = GetNextPosition(subject, change);

            if (!IsNextPositionValid(nextPosition))
                return false;

            Move(change, subject);
            return true;
        }

        public abstract bool IsNextPositionValid(Vector nextPosition);

        public abstract void Move(Vector change, GameObject subject);

        public abstract Vector GetNextPosition(GameObject subject, Vector change);

        public abstract Vector GetPositionChange(GameObject subject, long delta);
    }
}
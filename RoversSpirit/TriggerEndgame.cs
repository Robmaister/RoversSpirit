using System;

using OpenTK;

using RoversSpirit.Entities;
using RoversSpirit.Physics;

namespace RoversSpirit
{
	public class TriggerEndgame
	{
		public AABB BBox { get; private set; }

		public TriggerEndgame(Vector2 position, Vector2 size)
		{
			BBox = new AABB(position.X - size.X / 2, position.X + size.X / 2, position.Y + size.Y / 2, position.Y - size.Y / 2);
		}

		public void Activate()
		{

		}
	}
}

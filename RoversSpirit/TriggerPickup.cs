using System;

using OpenTK;

using RoversSpirit.Entities;
using RoversSpirit.Physics;

namespace RoversSpirit
{
	public class TriggerPickup
	{
		public Entity Ent { get; private set; }

		public AABB BBox { get; private set; }

		public TriggerPickup(Vector2 position, Vector2 size, Entity ent)
		{
			Ent = ent;
			BBox = new AABB(position.X - size.X / 2, position.X + size.X / 2, position.Y + size.Y / 2, position.Y - size.Y / 2);
		}
	}
}

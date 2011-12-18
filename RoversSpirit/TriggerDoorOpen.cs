using System;

using OpenTK;

using RoversSpirit.Entities;
using RoversSpirit.Physics;

namespace RoversSpirit
{
	public class TriggerDoorOpen
	{
		public Door Door { get; private set; }
		public string LockCode { get; private set; }

		public AABB BBox { get; private set; }

		public TriggerDoorOpen(Vector2 position, Vector2 size, Door door, string lockCode)
		{
			Door = door;
			LockCode = lockCode;
			BBox = new AABB(position.X - size.X / 2, position.X + size.X / 2, position.Y + size.Y / 2, position.Y - size.Y / 2);
		}
	}
}

using System;
using System.Collections.Generic;

using OpenTK;

using RoversSpirit.Physics;

namespace RoversSpirit
{
	public class TriggerChangeArea
	{
		public Area Area { get; private set; }
		public AABB BBox { get; private set; }

		public TriggerChangeArea(Vector2 position, Vector2 size, Area area)
		{
			Area = area;
			BBox = new AABB(position.X - size.X / 2, position.X + size.X / 2, position.Y + size.Y / 2, position.Y - size.Y / 2);
		}
	}
}

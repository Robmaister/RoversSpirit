using System;
using System.Collections.Generic;

using OpenTK;

using RoversSpirit.Entities;
using RoversSpirit.Physics;

namespace RoversSpirit
{
	public class TriggerButtonPress
	{
		public List<Door> Doors { get; private set; }
		public Button Button { get; private set; }

		public AABB BBox { get; private set; }

		public TriggerButtonPress(Vector2 position, Vector2 size, Button b, params Door[] doors)
		{
			Doors = new List<Door>();
			Doors.AddRange(doors);
			Button = b;
			BBox = new AABB(position.X - size.X / 2, position.X + size.X / 2, position.Y + size.Y / 2, position.Y - size.Y / 2);
		}

		public void Activate()
		{
			foreach (Door d in Doors)
			{
				if (d.Visible)
				{
					d.Visible = false;
					d.Solid = false;
				}
				else
				{
					d.Visible = true;
					d.Solid = true;
				}
			}

			Button.Activated = false;
		}
	}
}

using System;

using OpenTK;

using RoversSpirit.Graphics;
using RoversSpirit.Physics;

namespace RoversSpirit
{
	public class AreaTransition
	{
		public Vector2 Position { get; private set; }
		public Vector2 Size { get; private set; }

		public AABB BBox { get; private set; }

		public AreaTransition(Vector2 position, Vector2 size)
		{
			Position = position;
			Size = size;
			BBox = new AABB(position.X - size.X / 2, position.X + size.X / 2, position.Y + size.Y / 2, position.Y - size.Y / 2);
		}

		public void Activate()
		{
		}
	}
}

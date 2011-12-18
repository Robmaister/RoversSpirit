using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class Door : Entity
	{
		public Door(Vector2 position, float angle)
			: base(position, new Vector2(96, 16), Resources.Textures["door.png"], Vector2.One, true)
		{
			AllowPickup = false;
			this.Angle = angle;
		}

		public Door(Vector2 position, float angle, string name)
			: this(position, angle)
		{
			this.Name = name;
		}
	}
}

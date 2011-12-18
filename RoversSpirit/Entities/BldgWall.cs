using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class BldgWall : Entity
	{
		public BldgWall(Vector2 position, Vector2 size, float angle)
			: base(position, size, Resources.Textures["wallBrick.png"], Vector2.Divide(size, Resources.Textures["wallBrick.png"].SizeVec), true)
		{
			this.Angle = angle;
			AllowPickup = false;
		}
	}
}

using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class CaveWall : Entity
	{
		public CaveWall(Vector2 position, Vector2 size, float angle)
			: base(position, size, Resources.Textures["caveWall.png"], Vector2.Divide(size, Resources.Textures["caveWall.png"].SizeVec), true)
		{
			this.Angle = angle;
			AllowPickup = false;
		}
	}
}

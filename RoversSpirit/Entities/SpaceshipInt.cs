using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class SpaceshipInt : Entity
	{
		public SpaceshipInt(Vector2 position)
			: base(position, Resources.Textures["spaceshipInt.png"].SizeVec, Resources.Textures["spaceshipInt.png"], Vector2.One, false)
		{
			this.Angle = angle;
			AllowPickup = false;
		}
	}
}

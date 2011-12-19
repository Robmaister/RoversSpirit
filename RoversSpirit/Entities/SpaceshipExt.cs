using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class SpaceshipExt : Entity
	{
		public SpaceshipExt(Vector2 position)
			: base(position, Resources.Textures["spaceshipExt.png"].SizeVec, Resources.Textures["spaceshipExt.png"], Vector2.One, true)
		{
			this.Angle = angle;
			AllowPickup = false;
		}
	}
}

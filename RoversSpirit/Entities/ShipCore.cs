using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class ShipCore : Entity
	{
		public ShipCore(Vector2 position)
			: base(position, Resources.Textures["core.png"].SizeVec, Resources.Textures["core.png"], Vector2.One, false)
		{
			this.Name = "core";
		}
	}
}

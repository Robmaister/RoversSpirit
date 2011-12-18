using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class Rock : Entity
	{
		public Rock(Vector2 position, Texture tex)
			: base(position, new Vector2(tex.Size.Width, tex.Size.Height), tex, Vector2.One, true)
		{
			AllowPickup = false;
		}

	}
}

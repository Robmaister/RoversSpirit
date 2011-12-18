using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class Pebble : Entity
	{
		public Pebble(Vector2 position, Texture tex)
			: base(position, new Vector2(tex.Size.Width, tex.Size.Height), tex, Vector2.One, false)
		{
		}
	}
}

using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class Border : Entity
	{
		public Border(Vector2 position, Vector2 size)
			: base(position, size, Resources.Textures["trans.png"], Vector2.One, true)
		{
		}
	}
}

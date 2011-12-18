using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class Stairs : Entity
	{
		public Stairs(Vector2 position)
			: base(position, Resources.Textures["stairs.png"].SizeVec, Resources.Textures["stairs.png"], Vector2.One, false)
		{
		}
	}
}

using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class CaveStairs : Entity
	{
		public CaveStairs(Vector2 position)
			: base(position, Resources.Textures["caveStairs.png"].SizeVec, Resources.Textures["caveStairs.png"], Vector2.One, false)
		{
		}
	}
}

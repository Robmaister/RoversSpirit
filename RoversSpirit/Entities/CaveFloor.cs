using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class CaveFloor : Entity
	{
		public CaveFloor(Vector2 position, Vector2 size)
			: base(position, size, Resources.Textures["caveFloor.png"], Vector2.Divide(size, Resources.Textures["caveFloor.png"].SizeVec), false)
		{
			AllowPickup = false;
		}
	}
}

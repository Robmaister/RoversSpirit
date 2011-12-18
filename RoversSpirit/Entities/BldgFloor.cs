using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class BldgFloor : Entity
	{
		public BldgFloor(Vector2 position, Vector2 size)
			: base(position, size, Resources.Textures["bldgFloor.png"], Vector2.Divide(size, Resources.Textures["bldgFloor.png"].SizeVec), false)
		{
			AllowPickup = false;
		}
	}
}

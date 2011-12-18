using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class Fuse : Entity
	{
		public Fuse(Vector2 position)
			: base(position, Resources.Textures["fuse.png"].SizeVec, Resources.Textures["fuse.png"], Vector2.One, false)
		{
			this.Name = "fuse";
		}
	}
}

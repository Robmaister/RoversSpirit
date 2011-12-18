using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class Switch : Entity
	{
		public Switch(Vector2 position)
			: base(position, Resources.Textures["switch.png"].SizeVec, Resources.Textures["switch.png"], Vector2.One, false)
		{
			this.Name = "switch";
		}
	}
}

using System;

using OpenTK;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class DoorKey : Entity
	{
		public DoorKey(Vector2 position, string name)
			: base(position, new Vector2(32, 32), Resources.Textures["key.png"], Vector2.One, false)
		{
			AllowPickup = true;
			Name = name;
		}
	}
}

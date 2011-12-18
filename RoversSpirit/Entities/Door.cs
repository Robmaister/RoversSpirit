using System;

using OpenTK;

namespace RoversSpirit.Entities
{
	public class Door : Entity
	{
		public Door(DoorKey k, Vector2 position, Vector2 size)
			: base(position, size, null, Vector2.One, true)
		{
			AllowPickup = false;
		}
	}
}

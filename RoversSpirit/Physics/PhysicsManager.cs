using System;

namespace RoversSpirit.Physics
{
	public static class PhysicsManager
	{
		public static bool IsColliding(Entity ent1, Entity ent2)
		{
			return IsColliding(ent1.BoundingBox, ent2.BoundingBox);
		}

		public static bool IsColliding(AABB box1, AABB box2)
		{
			if (box1.Left > box2.Right) return false;
			if (box1.Right < box2.Left) return false;
			if (box1.Top < box2.Bottom) return false;
			if (box1.Bottom > box2.Top) return false;

			return true;
		}
	}
}

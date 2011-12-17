using System;

using OpenTK;

namespace RoversSpirit.Physics
{
	[Flags]
	public enum Axes
	{
		None = 0,
		Left = 1,
		Right = 2,
		Top = 4,
		Bottom = 8
	}

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

		/// <summary>
		/// Minimum vector for ent1 to leave ent2
		/// </summary>
		/// <param name="ent1"></param>
		/// <param name="ent2"></param>
		/// <returns></returns>
		public static Vector2 ReactCollision(Entity ent1, Entity ent2)
		{
			Vector2 sizes = ent1.Size + ent2.Size;

			Vector2 colDist = ent1.Position - ent2.Position;

			//abs
			if (colDist.X < 0) colDist.X = -colDist.X;
			if (colDist.Y < 0) colDist.Y = -colDist.Y;

			colDist += (sizes / 2);

			Vector2 minMoveDist = sizes - colDist;

			AABB bb1 = ent1.BoundingBox, bb2 = ent2.BoundingBox;

			if (ent1.Position.X <= ent2.Position.X) minMoveDist.X = -minMoveDist.X;
			if (ent1.Position.Y <= ent2.Position.Y) minMoveDist.Y = -minMoveDist.Y;

			Axes collidingAxes = GetCollidingAxes(bb1, bb2);

			if ((collidingAxes & Axes.Left) != Axes.Left && (collidingAxes & Axes.Right) != Axes.Right)
				minMoveDist.X = 0;

			if ((collidingAxes & Axes.Top) != Axes.Top && (collidingAxes & Axes.Bottom) != Axes.Bottom)
				minMoveDist.Y = 0;

			if (collidingAxes.Count() == 2)
			{
				if (Math.Abs(minMoveDist.X) > Math.Abs(minMoveDist.Y))
					minMoveDist.X = 0;
				else
					minMoveDist.Y = 0;
			}

			return minMoveDist;
		}

		public static Axes GetCollidingAxes(AABB box1, AABB box2)
		{
			Axes a = Axes.None;

			if (box1.Top <= box2.Top && box1.Top > box2.Bottom) a |= Axes.Bottom;
			if (box1.Bottom >= box2.Bottom && box1.Bottom < box2.Top) a |= Axes.Top;
			if (box1.Right >= box2.Right && box1.Right > box2.Left) a |= Axes.Right;
			if (box1.Left <= box2.Left && box1.Left < box2.Right) a |= Axes.Left;

			if ((a & (Axes.Top | Axes.Bottom)) == (Axes.Top | Axes.Bottom))
				a &= ~(Axes.Top | Axes.Bottom);
			if ((a & (Axes.Left | Axes.Right)) == (Axes.Left | Axes.Right))
				a &= ~(Axes.Left | Axes.Right);

			return a;
		}

		/// <summary>
		/// Counts the number of flags set. Thanks stackoverflow!
		/// </summary>
		/// <param name="axes"></param>
		/// <returns></returns>
		private static UInt32 Count(this Axes axes)
		{
			UInt32 v = (UInt32)axes;
			v = v - ((v >> 1) & 0x55555555); // reuse input as temporary
			v = (v & 0x33333333) + ((v >> 2) & 0x33333333); // temp
			UInt32 c = ((v + (v >> 4) & 0xF0F0F0F) * 0x1010101) >> 24; // count
			return c;
		}
	}
}

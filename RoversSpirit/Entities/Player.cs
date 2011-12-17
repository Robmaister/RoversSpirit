using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class Player : Entity
	{

		public Player()
			: this(new Vector2(0, 0))
		{
		}

		public Player(Vector2 position)
			: base(position, new Vector2(64, 64), Resources.Textures["player.png"], true)
		{
		}
	}
}

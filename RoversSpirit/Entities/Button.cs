using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class Button : Entity
	{
		public bool Activated { get; set; }

		public Button(Vector2 position)
			: base(position, Resources.Textures["button.png"].SizeVec, Resources.Textures["button.png"], Vector2.One, false)
		{
		}
	}
}

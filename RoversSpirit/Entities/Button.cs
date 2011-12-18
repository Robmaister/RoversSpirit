using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class Button : Entity
	{
		private bool activated;

		public bool Activated
		{
			get
			{
				return activated;
			}
			set
			{
				activated = value;

				if (activated)
					tex = Resources.Textures["buttonP.png"];
				else
					tex = Resources.Textures["button.png"];
			}
		}

		public Button(Vector2 position)
			: base(position, Resources.Textures["button.png"].SizeVec, Resources.Textures["button.png"], Vector2.One, false)
		{
		}
	}
}

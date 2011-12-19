using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class Note : Entity
	{
		public Note(Vector2 position)
			: base(position, Resources.Textures["note.png"].SizeVec, Resources.Textures["note.png"], Vector2.One, false)
		{
		}
	}
}

using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using RoversSpirit.Graphics;
using RoversSpirit.Entities;

namespace RoversSpirit
{
	public class AreaUpstairs : Area
	{
		public AreaUpstairs()
		{
			this.ClearColor = new Color4(0, 0, 0, 1.0f);
		}

		public override Vector2 SetPlayerStartLocation(Type previousArea)
		{
			throw new NotImplementedException();
		}

		public override void LoadContent(GameData data)
		{
			throw new NotImplementedException();
		}

		public override void Unload()
		{
			throw new NotImplementedException();
		}
	}
}

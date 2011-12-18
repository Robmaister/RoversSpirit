using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;

using RoversSpirit.Graphics;
using RoversSpirit.Entities;

namespace RoversSpirit
{
	public class AreaShip : Area
	{
		public override Vector2 SetPlayerStartLocation(Type previousArea)
		{
			return new Vector2(0, 0);
		}

		public override void LoadContent(GameData data)
		{
			EntList.Add(new BldgFloor(Vector2.Zero, new Vector2(256, 256)));
			EntList.Add(new BldgWall(new Vector2(0, 136), new Vector2(256, 16), 0));

			AreaChangeTriggers.Add(new TriggerChangeArea(new Vector2(0, -256), new Vector2(256, 256), new AreaMars()));

			Resources.StopAllAudio();
			Resources.Audio["shipStaticLoop.wav"].Play();
		}

		public override void Unload()
		{
			foreach (Entity ent in EntList)
				ent.Unload();

			EntList.Clear();
		}
	}
}

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
			return new Vector2(0, -32);
		}

		public override void LoadContent(GameData data)
		{
			EntList.Add(new SpaceshipInt(Vector2.Zero));
			Note n = new Note(new Vector2(-64, 40));
			EntList.Add(n);
			ReadingTriggers.Add(new TriggerReading(n.Position, new Vector2(64, 64), Notes.ShipNote));

			AreaChangeTriggers.Add(new TriggerChangeArea(new Vector2(0, 80), new Vector2(96, 48), new AreaMars()));

			EntList.Add(new Border(new Vector2(-128 - 16, 0), new Vector2(32, 256)));
			EntList.Add(new Border(new Vector2(128 + 16, 0), new Vector2(32, 256)));
			EntList.Add(new Border(new Vector2(0, -128 - 16), new Vector2(256, 32)));
			EntList.Add(new Border(new Vector2(0, 128 + 16), new Vector2(256, 32)));

			EntList.Add(new Border(new Vector2(-1, -96), new Vector2(88, 30)));


			if (data.CoreFound && data.FuseFound && data.SwitchFound)
			{
				EndgameTrigger.Add(new TriggerEndgame(new Vector2(0, 40), new Vector2(100, 100)));
			}

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

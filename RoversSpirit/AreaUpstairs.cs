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
		const float houseWidth = 640;
		const float houseHeight = 384;
		const float halfWidth = houseWidth / 2;
		const float halfHeight = houseHeight / 2;
		const float wallWidth = 16;
		const float wallHalfWidth = wallWidth / 2;

		const float stairsWidth = 96;
		const float stairsHeight = 64;

		const float stairsHalfWidth = stairsWidth / 2;
		const float stairsHalfHeight = stairsHeight / 2;

		public AreaUpstairs()
		{
			this.ClearColor = new Color4(0, 0, 0, 1.0f);
		}

		public override Vector2 SetPlayerStartLocation(Type previousArea)
		{
			return new Vector2(-halfWidth + 32, halfHeight - 32);
		}

		public override void LoadContent(GameData data)
		{
			LoadRoom(Vector2.Zero, data);

			Resources.StopAllAudio();
		}

		public override void Unload()
		{
			foreach (Entity ent in EntList)
				ent.Unload();

			EntList.Clear();
		}

		private void LoadRoom(Vector2 position, GameData data)
		{

			//floor
			EntList.Add(new BldgFloor(position, new Vector2(houseWidth, houseHeight)));

			//solid walls
			EntList.Add(new BldgWall(position + new Vector2(0, halfHeight + wallHalfWidth), new Vector2(houseWidth + 2 * wallWidth, wallWidth), 0));
			EntList.Add(new BldgWall(position - new Vector2(0, halfHeight + wallHalfWidth), new Vector2(houseWidth + 2 * wallWidth, wallWidth), 0));
			EntList.Add(new BldgWall(position - new Vector2(halfWidth + wallHalfWidth, 0), new Vector2(houseHeight, wallWidth), 3 * MathHelper.PiOver2));
			EntList.Add(new BldgWall(position + new Vector2(halfWidth + wallHalfWidth, 0), new Vector2(houseHeight, wallWidth), MathHelper.PiOver2));

			//switch
			if (!data.SwitchFound)
			{
				Switch s = new Switch(position + new Vector2(halfWidth - 32, halfHeight - 32));
				EntList.Add(s);
				PickupTriggers.Add(new TriggerPickup(s.Position, new Vector2(64, 64), s));
			}

			//stairs
			Stairs st = new Stairs(position - (new Vector2(halfWidth - stairsHalfWidth - 80, -halfHeight + stairsHalfHeight)));
			EntList.Add(st);
			EntList.Add(new Border(st.Position - new Vector2(0, st.Size.Y / 2 + 2), new Vector2(st.Size.X, 4)));
			EntList.Add(new Border(st.Position + new Vector2(st.Size.X / 2 + 2, 1), new Vector2(4, st.Size.Y + 2)));
			AreaChangeTriggers.Add(new TriggerChangeArea(st.Position, st.Size - new Vector2(10, 10), new AreaMars()));
		}
	}
}

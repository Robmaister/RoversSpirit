using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;

using RoversSpirit.Graphics;
using RoversSpirit.Entities;

namespace RoversSpirit
{
	public class AreaCave : Area
	{
		const float hallWidth = 128;
		const float hallHeight = 512;
		const float hallWidthHalf = hallWidth / 2;
		const float hallHeightHalf = hallHeight / 2;

		const float roomWidth = 384;
		const float roomHeight = 624;
		const float roomWidthHalf = roomWidth / 2;
		const float roomHeightHalf = roomHeight / 2;

		const float wallWidth = 16;
		const float wallWidthHalf = wallWidth / 2;

		const float doorWidth = 96;
		const float doorWidthHalf = doorWidth / 2;

		public AreaCave()
			: base()
		{
			this.ClearColor = Color4.Black;
		}

		public override Vector2 SetPlayerStartLocation(Type previousArea)
		{
			return new Vector2(0, -hallHeight + 96);
		}

		public override void LoadContent(GameData data)
		{
			//hall
			EntList.Add(new CaveFloor(new Vector2(0, -hallHeightHalf), new Vector2(hallWidth, hallHeight)));
			EntList.Add(new CaveWall(new Vector2(-hallWidthHalf - wallWidthHalf, -hallHeightHalf), new Vector2(hallHeight, wallWidth), MathHelper.PiOver2));
			EntList.Add(new CaveWall(new Vector2(hallWidthHalf + wallWidthHalf, -hallHeightHalf), new Vector2(hallHeight, wallWidth), 3 * MathHelper.PiOver2));
			AreaChangeTriggers.Add(new TriggerChangeArea(new Vector2(0, -hallHeight - hallWidthHalf), new Vector2(hallWidthHalf, hallWidthHalf), new AreaMars()));

			//room
			EntList.Add(new CaveFloor(new Vector2(0, roomHeightHalf), new Vector2(roomWidth, roomHeight)));
			EntList.Add(new CaveWall(new Vector2(0, roomHeight + wallWidthHalf), new Vector2(roomWidth + 2 * wallWidth, wallWidth), 0));
			EntList.Add(new CaveWall(new Vector2(-roomWidthHalf - wallWidthHalf, roomHeightHalf), new Vector2(roomHeight, wallWidth), MathHelper.PiOver2));
			EntList.Add(new CaveWall(new Vector2(roomWidthHalf + wallWidthHalf, roomHeightHalf), new Vector2(roomHeight, wallWidth), 3 * MathHelper.PiOver2));

			const float bottomWallSize = roomWidthHalf - hallWidthHalf;
			const float bottomWallX = roomWidthHalf + wallWidth - (bottomWallSize / 2);

			const float midWallSize = roomWidthHalf - doorWidthHalf;
			const float midWallX = roomWidthHalf - (midWallSize / 2);

			EntList.Add(new CaveWall(new Vector2(-bottomWallX, -wallWidthHalf), new Vector2(bottomWallSize, wallWidth), MathHelper.Pi));
			EntList.Add(new CaveWall(new Vector2(bottomWallX, -wallWidthHalf), new Vector2(bottomWallSize, wallWidth), MathHelper.Pi));
			EntList.Add(new CaveWall(new Vector2(-midWallX, roomHeightHalf), new Vector2(midWallSize, 3 * wallWidth), 0));
			EntList.Add(new CaveWall(new Vector2(midWallX, roomHeightHalf), new Vector2(midWallSize, 3 * wallWidth), 0));

			//doors
			Door d1 = new Door(new Vector2(0, roomHeightHalf - wallWidth), 0);
			Door d2 = new Door(new Vector2(0, roomHeightHalf), 0);
			Door d3 = new Door(new Vector2(0, roomHeightHalf + wallWidth), 0);

			d2.Visible = false;
			d2.Solid = false;

			d3.Visible = false;
			d3.Solid = false;

			EntList.Add(d1);
			EntList.Add(d2);
			EntList.Add(d3);

			Button b1 = new Button(new Vector2(-roomWidthHalf + 8 + 32, 19 + 32));
			Button b2 = new Button(b1.Position + new Vector2(0, 64 + 19));
			Button b3 = new Button(b2.Position + new Vector2(0, 64 + 19));
			Button b4 = new Button(new Vector2(-b3.Position.X, b3.Position.Y));

			EntList.Add(b1);
			EntList.Add(b2);
			EntList.Add(b3);
			EntList.Add(b4);

			TriggerButtonPress bp1 = new TriggerButtonPress(b1.Position, b1.Size, b1, d2, d3);
			TriggerButtonPress bp2 = new TriggerButtonPress(b2.Position, b2.Size, b2, d1, d2);
			TriggerButtonPress bp3 = new TriggerButtonPress(b3.Position, b3.Size, b3, d1, d3);
			TriggerButtonPress bp4 = new TriggerButtonPress(b4.Position, b4.Size, b4, d1, d2, d3);

			ButtonPressTriggers.Add(bp1);
			ButtonPressTriggers.Add(bp2);
			ButtonPressTriggers.Add(bp3);
			ButtonPressTriggers.Add(bp4);

			if (!data.CoreFound)
			{
				ShipCore core = new ShipCore(new Vector2(0, roomHeight - 96));
				PickupTriggers.Add(new TriggerPickup(core.Position, core.Size + new Vector2(16, 16), core));
				EntList.Add(core);
			}
		}

		public override void Unload()
		{
			foreach (Entity ent in EntList)
				ent.Unload();

			EntList.Clear();
		}
	}
}

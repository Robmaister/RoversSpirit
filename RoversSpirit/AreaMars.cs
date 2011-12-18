using System;

using OpenTK;
using OpenTK.Graphics;

using RoversSpirit.Graphics;
using RoversSpirit.Entities;

namespace RoversSpirit
{
	public class AreaMars : Area
	{
		public AreaMars()
			: base()
		{
			ClearColor = new Color4(183, 148, 106, 255);
		}

		public override void LoadContent(GameData data)
		{
			Random random = new Random();
			for (int i = 0; i < 80; i++)
			{
				EntList.Add(new Pebble(new Vector2((float)(random.NextDouble() * 3000) - 1500, (float)(random.NextDouble() * 3000) - 1500), (random.Next(0, 2) == 1) ? Resources.Textures["pebble1.png"] : Resources.Textures["pebble2.png"]));
			}

			EntList.Add(new Rock(new Vector2(-100, 50), Resources.Textures["rock1.png"]));

			GeneratePrison(new Vector2(512, 512), data);
			GenerateHouse1(new Vector2(-768, 256), data);

			AreaChangeTriggers.Add(new TriggerChangeArea(new Vector2(0, -200), new Vector2(500, 50), new AreaShip()));

			Resources.StopAllAudio();
			Resources.Audio["wind.wav"].Play();
		}

		public override Vector2 SetPlayerStartLocation(Type previousArea)
		{
			return new Vector2(0, 0);
		}

		public override void Unload()
		{
			foreach (Entity ent in EntList)
				ent.Unload();

			EntList.Clear();
		}

		public void GeneratePrison(Vector2 position, GameData data)
		{
			const float doorSize = 96;
			const float doorAscent = 64;
			const float prisonWidth = 576;
			const float prisonHeight = 576;
			const float halfWidth = prisonWidth / 2;
			const float halfHeight = prisonHeight / 2;
			const float wallWidth = 16;
			const float wallHalfWidth = wallWidth / 2;

			const float doorTopHeight = doorSize + doorAscent;
			const float wallAboveDoorSize = (prisonHeight - (doorAscent + doorSize));
			const float halfWallAboveDoorSize = wallAboveDoorSize / 2;

			const float cellSize = prisonWidth / 3;
			const float cellDoorSize = 96;
			float internalWallHeight = position.Y + halfHeight - cellSize - wallHalfWidth;
			float internalWallSize = cellSize - cellDoorSize;

			//floor
			EntList.Add(new BldgFloor(position, new Vector2(prisonWidth, prisonHeight)));

			//solid walls
			EntList.Add(new BldgWall(position + new Vector2(0, halfHeight + wallHalfWidth), new Vector2(prisonWidth + 2 * wallWidth, wallWidth), 0));
			EntList.Add(new BldgWall(position - new Vector2(0, halfHeight + wallHalfWidth), new Vector2(prisonWidth + 2 * wallWidth, wallWidth), 0));
			EntList.Add(new BldgWall(position + new Vector2(halfWidth + wallHalfWidth, 0), new Vector2(prisonHeight, wallWidth), 3 * MathHelper.PiOver2));

			//wall w/ entrance
			EntList.Add(new BldgWall(position - new Vector2(halfWidth + wallHalfWidth, halfHeight - (doorAscent / 2)), new Vector2(doorAscent, wallWidth), MathHelper.PiOver2));
			EntList.Add(new BldgWall(position - new Vector2(halfWidth + wallHalfWidth, halfHeight - doorTopHeight - halfWallAboveDoorSize), new Vector2(wallAboveDoorSize, wallWidth), MathHelper.PiOver2));

			//internal walls
			EntList.Add(new BldgWall(new Vector2(position.X - halfWidth + cellDoorSize + (internalWallSize / 2), internalWallHeight), new Vector2(cellSize - cellDoorSize, wallWidth), 0));
			EntList.Add(new BldgWall(new Vector2(position.X - halfWidth + cellSize + cellDoorSize + (internalWallSize / 2), internalWallHeight), new Vector2(cellSize - cellDoorSize, wallWidth), 0));
			EntList.Add(new BldgWall(new Vector2(position.X - halfWidth + 2 * cellSize + cellDoorSize + (internalWallSize / 2), internalWallHeight), new Vector2(cellSize - cellDoorSize, wallWidth), 0));
			EntList.Add(new BldgWall(new Vector2(position.X - halfWidth + cellSize - wallHalfWidth, internalWallHeight + (cellSize / 2) + wallHalfWidth), new Vector2(cellSize, wallWidth), 3 * MathHelper.PiOver2));
			EntList.Add(new BldgWall(new Vector2(position.X - halfWidth + 2 * cellSize - wallHalfWidth, internalWallHeight + (cellSize / 2) + wallHalfWidth), new Vector2(cellSize, wallWidth), 3 * MathHelper.PiOver2));

			//key
			if (!data.PrisonKeyFound)
			{
				DoorKey prisonKey = new DoorKey(position + new Vector2(halfWidth, -halfHeight) - new Vector2(32, -32), "prison door key");
				PickupTriggers.Add(new TriggerPickup(prisonKey.Position, new Vector2(64, 64), prisonKey));
				EntList.Add(prisonKey);
			}



			//doors
			Door prisonDoor = new Door(new Vector2(position.X - halfWidth + (cellDoorSize / 2), internalWallHeight), 0, "prison door");
			if (!data.PrisonDoorOpened)
			{
				EntList.Add(prisonDoor);
			}
			else
				prisonDoor.Unload();
			EntList.Add(new Door(new Vector2(position.X - halfWidth + cellSize + (cellDoorSize / 2), internalWallHeight), 0));
			EntList.Add(new Door(new Vector2(position.X - halfWidth + cellSize * 2 + (cellDoorSize / 2), internalWallHeight), 0));

			DoorOpenTriggers.Add(new TriggerDoorOpen(prisonDoor.Position, new Vector2(96, 96), prisonDoor, "prison door key"));

			//fuse
			if (!data.FuseFound)
			{
				Fuse f = new Fuse(new Vector2(position.X - halfWidth + (cellSize / 2), internalWallHeight + (cellSize / 2)));
				EntList.Add(f);
				PickupTriggers.Add(new TriggerPickup(f.Position, new Vector2(64, 64), f));

			}
		}

		public void GenerateHouse1(Vector2 position, GameData data)
		{
			const float doorSize = 96;
			const float doorAscent = 96;
			const float houseWidth = 512;
			const float houseHeight = 384;
			const float halfWidth = houseWidth / 2;
			const float halfHeight = houseHeight / 2;
			const float wallWidth = 16;
			const float wallHalfWidth = wallWidth / 2;

			const float doorTopHeight = doorSize + doorAscent;
			const float wallAboveDoorSize = (houseHeight - (doorAscent + doorSize));
			const float halfWallAboveDoorSize = wallAboveDoorSize / 2;

			const float stairsWidth = 96;
			const float stairsHeight = 64;

			const float stairsHalfWidth = stairsWidth / 2;
			const float stairsHalfHeight = stairsHeight / 2;

			//floor
			EntList.Add(new BldgFloor(position, new Vector2(houseWidth, houseHeight)));

			//solid walls
			EntList.Add(new BldgWall(position + new Vector2(0, halfHeight + wallHalfWidth), new Vector2(houseWidth + 2 * wallWidth, wallWidth), 0));
			EntList.Add(new BldgWall(position - new Vector2(0, halfHeight + wallHalfWidth), new Vector2(houseWidth + 2 * wallWidth, wallWidth), 0));
			EntList.Add(new BldgWall(position - new Vector2(halfWidth + wallHalfWidth, 0), new Vector2(houseHeight, wallWidth), 3 * MathHelper.PiOver2));

			//wall w/ entrance
			EntList.Add(new BldgWall(position - new Vector2(-halfWidth - wallHalfWidth, halfHeight - (doorAscent / 2)), new Vector2(doorAscent, wallWidth), MathHelper.PiOver2));
			EntList.Add(new BldgWall(position - new Vector2(-halfWidth - wallHalfWidth, halfHeight - doorTopHeight - halfWallAboveDoorSize), new Vector2(wallAboveDoorSize, wallWidth), MathHelper.PiOver2));

			//stairs
			Stairs s = new Stairs(position - new Vector2(halfWidth - stairsHalfWidth, -halfHeight + stairsHalfHeight));
			EntList.Add(s);
			AreaChangeTriggers.Add(new TriggerChangeArea(s.Position, s.Size, new AreaUpstairs()));
		}
	}
}

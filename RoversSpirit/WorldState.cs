﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using QuickFont;

using RoversSpirit.Entities;
using RoversSpirit.Graphics;
using RoversSpirit.Physics;

namespace RoversSpirit
{
	public class WorldState : IState
	{
		private QFont font;
		private Camera c;
		private Player player;
		private List<Entity> entList;
		private List<TriggerPickup> pickupTriggers;
		private string message;
		private Random random;

		private SoundPlayer wind;

		public WorldState()
		{
			c = new Camera();
			c.UnitScale = 1;

			random = new Random();
			message = String.Empty;
		}

		public void OnLoad(EventArgs e)
		{
			GL.ClearColor(new Color4(183, 148, 106, 255));
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.Texture2D);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			Resources.LoadAll();

			entList = new List<Entity>();
			pickupTriggers = new List<TriggerPickup>();

			font = new QFont("comic.ttf", 24);

			Resources.Textures["pebble1.png"].MagFilter = TextureMagFilter.Nearest;
			Resources.Textures["pebble1.png"].MinFilter = TextureMinFilter.Nearest;
			Resources.Textures["pebble2.png"].MagFilter = TextureMagFilter.Nearest;
			Resources.Textures["pebble2.png"].MinFilter = TextureMinFilter.Nearest;
			Resources.Textures["playerf1.png"].MagFilter = TextureMagFilter.Nearest;
			Resources.Textures["playerf1.png"].MinFilter = TextureMinFilter.Nearest;
			Resources.Textures["playerf2.png"].MagFilter = TextureMagFilter.Nearest;
			Resources.Textures["playerf2.png"].MinFilter = TextureMinFilter.Nearest;

			for (int i = 0; i < 30; i++)
			{
				entList.Add(new Pebble(new Vector2((float)(random.NextDouble() * 1000) - 500, (float)(random.NextDouble() * 1000) - 500), (random.Next(0, 2) == 1) ? Resources.Textures["pebble1.png"] : Resources.Textures["pebble2.png"]));
			}

			player = new Player();
			entList.Add(new Rock(new Vector2(-100, 50), Resources.Textures["rock1.png"]));
			//entList.Add(new BldgFloor(new Vector2(256, 256), new Vector2(256, 256)));
			//entList.Add(new BldgWall(new Vector2(112, 256), new Vector2(256, 32), MathHelper.PiOver2));

			GeneratePrison(new Vector2(512, 512));

			Resources.Audio["wind.wav"].Looping = true;
			Resources.Audio["wind.wav"].Play();
		}

		public void OnUpdateFrame(FrameEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse)
		{
			player.Moving = false;

			if (Keyboard[Key.Up])
			{
				player.MoveBy(new Vector2(0, (float)(e.Time * Player.MoveSpeed)));
				player.Moving = true;
			}

			if (Keyboard[Key.Down])
			{
				player.MoveBy(new Vector2(0, -(float)(e.Time * Player.MoveSpeed)));
				player.Moving = true;
			}

			if (Keyboard[Key.Left])
			{
				player.MoveBy(new Vector2(-(float)(e.Time * Player.MoveSpeed), 0));
				player.Moving = true;
			}

			if (Keyboard[Key.Right])
			{
				player.MoveBy(new Vector2((float)(e.Time * Player.MoveSpeed), 0));
				player.Moving = true;
			}

			c.Update(e.Time);
			player.Update(e.Time);

			List<TriggerPickup> pickedUpList = new List<TriggerPickup>();

			foreach (TriggerPickup trigger in pickupTriggers)
			{
				if (PhysicsManager.IsColliding(player.BoundingBox, trigger.BBox))
				{
					if (Keyboard[Key.Z])
					{
						player.Inventory.Add(trigger.Ent);
						entList.Remove(trigger.Ent);
						pickedUpList.Add(trigger);
						message = "Picked up the " + trigger.Ent.Name;
					}

					else
					{
						message = "Press Z to pick up the " + trigger.Ent.Name;
					}
				}
			}

			foreach (TriggerPickup trigger in pickedUpList)
				pickupTriggers.Remove(trigger);

			foreach (Entity ent in entList)
			{
				if (PhysicsManager.IsColliding(player, ent) && ent.Solid)
					player.MoveBy(PhysicsManager.ReactCollision(player, ent));
			}

			c.JumpTo(player.Position);
		}

		public void OnRenderFrame(FrameEventArgs e)
		{
			c.UseWorldProjection();

			GL.EnableClientState(ArrayCap.VertexArray);
			GL.EnableClientState(ArrayCap.TextureCoordArray);

			foreach (Entity ent in entList)
			{
				GL.PushMatrix();
				ent.Draw();
				GL.PopMatrix();
			}

			GL.PushMatrix();
			player.Draw();
			GL.PopMatrix();

			GL.DisableClientState(ArrayCap.VertexArray);
			GL.DisableClientState(ArrayCap.TextureCoordArray);

			c.UseUIProjection();
			GL.PushMatrix();
			GL.Translate(new Vector3(5, 50, 0));
			font.Print(message);
			GL.PopMatrix();

			GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		public void OnResize(EventArgs e, Size ClientSize)
		{
			GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

			c.OnResize(ClientSize);
		}

		public void OnKeyDown(object sender, KeyboardKeyEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse)
		{
			switch (e.Key)
			{
				case Key.Escape:
					MainWindow.exit = true;
					break;
				case Key.Left:
					player.Angle = MathHelper.PiOver2;
					break;
				case Key.Right:
					player.Angle = 3 * MathHelper.PiOver2;
					break;
				case Key.Up:
					player.Angle = 0;
					break;
				case Key.Down:
					player.Angle = 3 * MathHelper.Pi;
					break;
			}
		}

		public void OnKeyUp(object sender, KeyboardKeyEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse)
		{
			switch (e.Key)
			{
				case Key.Left:
					if (Keyboard[Key.Right]) player.Angle = 3 * MathHelper.PiOver2;
					if (Keyboard[Key.Up]) player.Angle = 0;
					if (Keyboard[Key.Down]) player.Angle = MathHelper.Pi;
					break;
				case Key.Right:
					if (Keyboard[Key.Left]) player.Angle = MathHelper.PiOver2;
					if (Keyboard[Key.Up]) player.Angle = 0;
					if (Keyboard[Key.Down]) player.Angle = MathHelper.Pi;
					break;
				case Key.Up:
					if (Keyboard[Key.Left]) player.Angle = MathHelper.PiOver2;
					if (Keyboard[Key.Right]) player.Angle = 3 * MathHelper.PiOver2;
					if (Keyboard[Key.Down]) player.Angle = MathHelper.Pi;
					break;
				case Key.Down:
					if (Keyboard[Key.Left]) player.Angle = MathHelper.PiOver2;
					if (Keyboard[Key.Right]) player.Angle = 3 * MathHelper.PiOver2;
					if (Keyboard[Key.Up]) player.Angle = 0;
					break;
			}
		}

		public void OnMouseDown(object sender, MouseEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse)
		{
			
		}

		public void OnMouseUp(object sender, MouseEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse)
		{
			
		}

		public void OnUnload(EventArgs e)
		{
			player.Unload();
			foreach (Entity ent in entList)
				ent.Unload();
		}

		public void GeneratePrison(Vector2 position)
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
			entList.Add(new BldgFloor(position, new Vector2(prisonWidth, prisonHeight)));

			//solid walls
			entList.Add(new BldgWall(position + new Vector2(0, halfHeight + wallHalfWidth), new Vector2(prisonWidth + 2 * wallWidth, wallWidth), 0));
			entList.Add(new BldgWall(position - new Vector2(0, halfHeight + wallHalfWidth), new Vector2(prisonWidth + 2 * wallWidth, wallWidth), 0));
			entList.Add(new BldgWall(position + new Vector2(halfWidth + wallHalfWidth, 0), new Vector2(prisonHeight, wallWidth), 3 * MathHelper.PiOver2));

			//wall w/ entrance
			entList.Add(new BldgWall(position - new Vector2(halfWidth + wallHalfWidth, halfHeight - (doorAscent / 2)), new Vector2(doorAscent, wallWidth), MathHelper.PiOver2));
			entList.Add(new BldgWall(position - new Vector2(halfWidth + wallHalfWidth, halfHeight - doorTopHeight - halfWallAboveDoorSize), new Vector2(wallAboveDoorSize, wallWidth), MathHelper.PiOver2));

			//internal walls
			entList.Add(new BldgWall(new Vector2(position.X - halfWidth + cellDoorSize + (internalWallSize / 2), internalWallHeight), new Vector2(cellSize - cellDoorSize, wallWidth), 0));
			entList.Add(new BldgWall(new Vector2(position.X - halfWidth + cellSize + cellDoorSize + (internalWallSize / 2), internalWallHeight), new Vector2(cellSize - cellDoorSize, wallWidth), 0));
			entList.Add(new BldgWall(new Vector2(position.X - halfWidth + 2 * cellSize + cellDoorSize + (internalWallSize / 2), internalWallHeight), new Vector2(cellSize - cellDoorSize, wallWidth), 0));
			entList.Add(new BldgWall(new Vector2(position.X - halfWidth + cellSize - wallHalfWidth, internalWallHeight + (cellSize / 2) + wallHalfWidth), new Vector2(cellSize, wallWidth), 3 * MathHelper.PiOver2));
			entList.Add(new BldgWall(new Vector2(position.X - halfWidth + 2 * cellSize - wallHalfWidth, internalWallHeight + (cellSize / 2) + wallHalfWidth), new Vector2(cellSize, wallWidth), 3 * MathHelper.PiOver2));

			DoorKey prisonKey = new DoorKey(position + new Vector2(halfWidth, -halfHeight) - new Vector2(16, -16), "prison door key");
			pickupTriggers.Add(new TriggerPickup(prisonKey.Position, new Vector2(64, 64), prisonKey));
			entList.Add(prisonKey);
		}
	}
}

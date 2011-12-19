using System;
using System.Collections.Generic;
using System.Drawing;

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
		const float fadeTime = 0.8f;

		private GameData data;
		private Area area;
		private Area tempNewArea;

		private QFont font;
		private Camera c;
		private Player player;
		private string message;

		private ColorBox fadeBox;
		private ColorBox InventoryBox;
		private bool fadingOut;
		private bool gameWon;

		public WorldState(Area area)
		{
			c = new Camera();
			c.UnitScale = 1;
			this.area = area;
			message = String.Empty;
			data = new GameData();
		}

		public void OnLoad(EventArgs e)
		{
			area.LoadContent(data);

			GL.ClearColor(area.ClearColor);
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.Texture2D);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			font = new QFont("Resources/Fonts/Cousine-Regular-Latin.ttf", 24);

			player = new Player();
			
			Resources.Audio["wind.wav"].Looping = true;
			Resources.Audio["shipStaticLoop.wav"].Looping = true;
			Resources.Audio["start.wav"].Play();
		}

		public void OnUpdateFrame(FrameEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse)
		{
			if (!fadingOut && fadeBox.Color.A > 0)
			{
				fadeBox.Color = new Color4(0, 0, 0, fadeBox.Color.A - (float)e.Time * fadeTime);
			}

			if (fadingOut)
			{
				if (fadeBox.Color.A < 1)
					fadeBox.Color = new Color4(0, 0, 0, fadeBox.Color.A + (float)e.Time * fadeTime);
				else
				{
					if (gameWon)
					{
						MainWindow.state = new EndMenuState();
						Resources.StopAllAudio();
						//Resources.UnloadAudioBuffers();
					}
					else
					{
						Type oldAreaType = area.GetType();
						area.Unload();
						area = tempNewArea;
						area.LoadContent(data);
						player.MoveTo(area.SetPlayerStartLocation(oldAreaType));
						GL.ClearColor(area.ClearColor);
						fadingOut = false;
					}
				}

			}

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

			message = string.Empty;


			foreach (TriggerChangeArea trigger in area.AreaChangeTriggers)
			{
				if (PhysicsManager.IsColliding(player.BoundingBox, trigger.BBox))
				{
					tempNewArea = trigger.Area;
					fadingOut = true;
					break;
				}
			}

			foreach (TriggerButtonPress trigger in area.ButtonPressTriggers)
			{
				if (PhysicsManager.IsColliding(player.BoundingBox, trigger.BBox))
				{
					trigger.Button.Activated = true;
				}

				else
				{
					if (trigger.Button.Activated)
					{
						trigger.Activate();
					}
				}
			}

			foreach (TriggerEndgame trigger in area.EndgameTrigger)
			{
				if (PhysicsManager.IsColliding(player.BoundingBox, trigger.BBox))
				{
					if (Keyboard[Key.Z])
					{
						fadingOut = true;
						gameWon = true;
					}
					else
					{
						message = "Press Z to repair the ship and leave!";
					}
				}
			}

			foreach (TriggerReading trigger in area.ReadingTriggers)
			{
				if (PhysicsManager.IsColliding(player.BoundingBox, trigger.BBox))
				{
					if (Keyboard[Key.Z])
					{
						trigger.Activate();
					}
					else
					{
						message = "Press Z to read the note";
					}
				}
			}

			List<TriggerDoorOpen> openedList = new List<TriggerDoorOpen>();
			foreach (TriggerDoorOpen trigger in area.DoorOpenTriggers)
			{
				if (PhysicsManager.IsColliding(player.BoundingBox, trigger.BBox))
				{
					Entity ent = player.FindNameInInventory(trigger.LockCode);
					if (ent != null)
					{
						if (Keyboard[Key.Z])
						{
							ChangeGameDataDoor(trigger.Door);
							player.Inventory.Remove(ent);
							area.EntList.Remove(trigger.Door);
							ent.Unload();
							trigger.Door.Unload();
						}
						else
							message = "Press Z to open the " + trigger.Door.Name;
					}
				}
			}


			List<TriggerPickup> pickedUpList = new List<TriggerPickup>();
			foreach (TriggerPickup trigger in area.PickupTriggers)
			{
				if (PhysicsManager.IsColliding(player.BoundingBox, trigger.BBox))
				{
					if (Keyboard[Key.Z])
					{
						ChangeGameDataItem(trigger.Ent);
						player.Inventory.Add(trigger.Ent);
						area.EntList.Remove(trigger.Ent);
						pickedUpList.Add(trigger);
					}
					else
						message = "Press Z to pick up the " + trigger.Ent.Name;
				}
			}
			foreach (TriggerPickup trigger in pickedUpList)
				area.PickupTriggers.Remove(trigger);


			foreach (Entity ent in area.EntList)
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

			foreach (Entity ent in area.EntList)
			{
				GL.PushMatrix();
				ent.Draw();
				GL.PopMatrix();
			}

			GL.PushMatrix();
			player.Draw();
			GL.PopMatrix();

			c.UseUIProjection();

			GL.PushMatrix();
			InventoryBox.Draw();
			GL.PopMatrix();

			GL.PushMatrix();
			player.DrawInventory();
			GL.PopMatrix();

			GL.DisableClientState(ArrayCap.VertexArray);
			GL.DisableClientState(ArrayCap.TextureCoordArray);

			GL.PushMatrix();
			GL.Translate(new Vector3(5, 50, 0));
			font.Print(message);
			GL.PopMatrix();

			GL.BindTexture(TextureTarget.Texture2D, 0);

			if (fadeBox.Color.A > 0)
			{
				GL.EnableClientState(ArrayCap.VertexArray);
				GL.PushMatrix();
				fadeBox.Draw();
				GL.PopMatrix();
				GL.DisableClientState(ArrayCap.VertexArray);
			}
		}

		public void OnResize(EventArgs e, Size ClientSize)
		{
			GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

			c.OnResize(ClientSize);

			if (InventoryBox != null)
				InventoryBox.Unload();
			InventoryBox = new ColorBox(new Vector2(0, 0), new Vector2(ClientSize.Width, 32), new Color4(96, 96, 96, 128));

			float a = 1.0f;

			if (fadeBox != null)
			{
				a = fadeBox.Color.A;
				fadeBox.Unload();
			}
			fadeBox = new ColorBox(new Vector2(0, 0), new Vector2(ClientSize.Width, ClientSize.Height), new Color4(0, 0, 0, a));
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
			area.Unload();
		}

		public void ChangeGameDataDoor(Door d)
		{
			switch (d.Name)
			{
				case "prison door":
					data.PrisonDoorOpened = true;
					break;
			}
		}

		public void ChangeGameDataItem(Entity e)
		{
			switch (e.Name)
			{
				case "prison door key":
					data.PrisonKeyFound = true;
					break;
				case "switch":
					data.SwitchFound = true;
					break;
				case "fuse":
					data.FuseFound = true;
					break;
				case "core":
					data.CoreFound = true;
					break;
			}
		}
	}
}

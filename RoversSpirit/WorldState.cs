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
		private QFont font;
		private Camera c;
		private Player player;
		private List<Entity> entList;
		private bool collisionDetected;
		private Random random;

		public WorldState()
		{
			c = new Camera();
			c.UnitScale = 1;

			random = new Random();
		}

		public void OnLoad(EventArgs e)
		{
			GL.ClearColor(new Color4(183, 148, 106, 255));
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.Texture2D);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			Resources.LoadAll();

			entList = new List<Entity>();

			font = new QFont("comic.ttf", 12);

			Resources.Textures["pebble1.png"].MagFilter = TextureMagFilter.Nearest;
			Resources.Textures["pebble1.png"].MinFilter = TextureMinFilter.Nearest;
			Resources.Textures["pebble2.png"].MagFilter = TextureMagFilter.Nearest;
			Resources.Textures["pebble2.png"].MinFilter = TextureMinFilter.Nearest;

			for (int i = 0; i < 30; i++)
			{
				entList.Add(new Pebble(new Vector2((float)(random.NextDouble() * 1000) - 500, (float)(random.NextDouble() * 1000) - 500), (random.Next(0, 2) == 1) ? Resources.Textures["pebble1.png"] : Resources.Textures["pebble2.png"]));
			}

			player = new Player();
			entList.Add(new Rock(new Vector2(-100, 50), Resources.Textures["rock1.png"]));
		}

		public void OnUpdateFrame(FrameEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse)
		{
			if (Keyboard[Key.Up])
				player.MoveBy(new Vector2(0, 2.9f));
			if (Keyboard[Key.Down])
				player.MoveBy(new Vector2(0, -2.9f));
			if (Keyboard[Key.Left])
				player.MoveBy(new Vector2(-2.9f, 0));
			if (Keyboard[Key.Right])
				player.MoveBy(new Vector2(2.9f, 0));

			c.Update(e.Time);

			c.JumpTo(player.Position);

			collisionDetected = false;
			foreach (Entity ent in entList)
			{
				if (PhysicsManager.IsColliding(player, ent) && ent.Solid)
				{
					collisionDetected = true;
					player.MoveBy(PhysicsManager.ReactCollision(player, ent));
				}
			}
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
			GL.Translate(new Vector3(100, 100, 0));
			font.Print(collisionDetected ? "COLLISION DETECTED" : "Hello, fonts!");
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
			
		}
	}
}

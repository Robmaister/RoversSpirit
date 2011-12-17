using System;
using System.Collections.Generic;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using QuickFont;

using RoversSpirit.Entities;
using RoversSpirit.Graphics;
using OpenTK.Graphics;
using RoversSpirit.Physics;

namespace RoversSpirit
{
	public class MainWindow : GameWindow
	{
		private QFont font;

		private Camera c;

		private Player player;

		private List<Entity> entList;

		private bool collisionDetected;

		public static void Main(string[] args)
		{
			using (MainWindow window = new MainWindow())
			{
				window.Run();
			}
		}

		public MainWindow()
			: base()
		{
			this.Title = "Rover's Spirit";
			c = new Camera();
			c.UnitScale = 16;

			Keyboard.KeyDown += OnKeyDown;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			GL.ClearColor(new Color4(183, 148, 106, 255));
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.Texture2D);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			Resources.LoadAll();

			entList = new List<Entity>();

			font = new QFont("comic.ttf", 12);
			player = new Player();
			entList.Add(new Rock(new Vector2(-100, 50), Resources.Textures["rock1.png"]));
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

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
				if (PhysicsManager.IsColliding(player, ent))
				{
					collisionDetected = true;
					player.MoveBy(PhysicsManager.ReactCollision(player, ent));
				}
			}
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

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
			GL.Translate(new Vector3(-100, -100, 0));
			font.Print(collisionDetected ? "COLLISION DETECTED" : "Hello, fonts!");
			GL.PopMatrix();

			GL.BindTexture(TextureTarget.Texture2D, 0);

			SwapBuffers();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

			c.OnResize(ClientSize);
		}

		private void OnKeyDown(object sender, KeyboardKeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Escape:
					this.Exit();
					break;
			}
		}

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
		}
	}
}

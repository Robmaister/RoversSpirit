using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using QuickFont;

using RoversSpirit.Graphics;

namespace RoversSpirit
{
	public class MainWindow : GameWindow
	{
		private QFont font;

		private Camera c;

		private Player player;

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

			GL.ClearColor(Color.CornflowerBlue);
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.Texture2D);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			Resources.LoadAll();

			font = new QFont("comic.ttf", 12);
			player = new Player();
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

			if (Keyboard[Key.Up])
				player.MoveBy(new Vector2(0, 0.9f));
			if (Keyboard[Key.Down])
				player.MoveBy(new Vector2(0, -0.9f));
			if (Keyboard[Key.Left])
				player.MoveBy(new Vector2(-0.9f, 0));
			if (Keyboard[Key.Right])
				player.MoveBy(new Vector2(0.9f, 0));

			c.Update(e.Time);
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			c.UseWorldProjection();

			GL.EnableClientState(ArrayCap.VertexArray);
			GL.EnableClientState(ArrayCap.TextureCoordArray);
			player.Draw();
			GL.DisableClientState(ArrayCap.VertexArray);
			GL.DisableClientState(ArrayCap.TextureCoordArray);

			c.UseUIProjection();
			GL.PushMatrix();
			font.Print("Hello, fonts!");
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

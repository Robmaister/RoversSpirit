using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using QuickFont;

using RoversSpirit.Graphics;

namespace RoversSpirit
{
	public class MenuState : IState
	{
		private QFont titleFont;
		private QFont itemFont;

		private bool fadeOut;

		private Size ClientSize;

		public void OnLoad(EventArgs e)
		{
			titleFont = new QFont("times.ttf", 72);
			itemFont = new QFont("times.ttf", 36);

			titleFont.Options.Colour = new Color4(183, 148, 106, 0);

			GL.ClearColor(Color.Black);
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.Texture2D);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}

		public void OnUpdateFrame(FrameEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse)
		{
			const float timeToFade = 0.8f;

			if (!fadeOut)
			{
				if (titleFont.Options.Colour.A < 1.0f)
					titleFont.Options.Colour = new Color4(titleFont.Options.Colour.R, titleFont.Options.Colour.G, titleFont.Options.Colour.B, titleFont.Options.Colour.A + (float)e.Time * timeToFade);
			}
			else
			{
				if (titleFont.Options.Colour.A <= 0.0f)
					MainWindow.state = new WorldState(new AreaMars());
				else
					titleFont.Options.Colour = new Color4(titleFont.Options.Colour.R, titleFont.Options.Colour.G, titleFont.Options.Colour.B, titleFont.Options.Colour.A - (float)e.Time * timeToFade);
			}

			itemFont.Options.Colour.A = titleFont.Options.Colour.A;
		}

		public void OnRenderFrame(FrameEventArgs e)
		{
			GL.PushMatrix();
			GL.Translate(0, -40, 0);
			titleFont.Print("Rover's Spirit", QFontAlignment.Centre);
			GL.PopMatrix();

			GL.PushMatrix();
			GL.Translate(0, (ClientSize.Height / 2) - 48, 0);
			itemFont.Print("Press any key to start...", QFontAlignment.Centre);
			GL.PopMatrix();
		}

		public void OnResize(EventArgs e, Size ClientSize)
		{
			GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

			Matrix4 proj = Matrix4.CreateOrthographic(ClientSize.Width, -ClientSize.Height, 0, 1);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref proj);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();

			this.ClientSize = ClientSize;
		}

		public void OnKeyDown(object sender, KeyboardKeyEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse)
		{
			fadeOut = true;
		}

		public void OnKeyUp(object sender, KeyboardKeyEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse)
		{
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

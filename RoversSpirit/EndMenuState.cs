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
	public class EndMenuState : IState
	{
		private QFont subtitleFont;

		private bool fadeOut;

		private Size ClientSize;

		public void OnLoad(EventArgs e)
		{
			subtitleFont = new QFont("Resources/Fonts/Cousine-Regular-Latin.ttf", 24);
			subtitleFont.Options.Colour = new Color4(183, 148, 106, 0);

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
				if (subtitleFont.Options.Colour.A < 1.0f)
					subtitleFont.Options.Colour = new Color4(subtitleFont.Options.Colour.R, subtitleFont.Options.Colour.G, subtitleFont.Options.Colour.B, subtitleFont.Options.Colour.A + (float)e.Time * timeToFade);
			}
			else
			{
				if (subtitleFont.Options.Colour.A <= 0.0f)
					MainWindow.state = new MenuState();
				else
					subtitleFont.Options.Colour = new Color4(subtitleFont.Options.Colour.R, subtitleFont.Options.Colour.G, subtitleFont.Options.Colour.B, subtitleFont.Options.Colour.A - (float)e.Time * timeToFade);
			}
		}

		public void OnRenderFrame(FrameEventArgs e)
		{
			GL.PushMatrix();
			GL.Translate(0, -80, 0);
			subtitleFont.Print("And so you leave Mars...\nTo the dark, lonely depths of space.", QFontAlignment.Centre);
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

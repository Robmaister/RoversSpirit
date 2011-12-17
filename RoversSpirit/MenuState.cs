using System;
using System.Drawing;

using OpenTK;
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

		public void OnLoad(EventArgs e)
		{
			titleFont = new QFont("times.ttf", 48);
			itemFont = new QFont("times.ttf", 36);

			titleFont.Options.Colour = Color.CornflowerBlue;

			GL.ClearColor(Color.Black);
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.Texture2D);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}

		public void OnUpdateFrame(FrameEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse)
		{
			
		}

		public void OnRenderFrame(FrameEventArgs e)
		{
			GL.PushMatrix();
			titleFont.Print("Rover's Spirit", QFontAlignment.Centre);
			GL.PopMatrix();

			GL.PushMatrix();
			GL.Translate(0, 200, 0);
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
		}

		public void OnKeyDown(object sender, KeyboardKeyEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse)
		{
			MainWindow.state = new WorldState();
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

using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RoversSpirit
{
	public class MainWindow : GameWindow
	{
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
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			GL.ClearColor(Color.CornflowerBlue);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			SwapBuffers();
		}

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
		}
	}
}

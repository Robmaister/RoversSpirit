using System;
using System.Collections.Generic;
using System.Drawing;

using OpenTK;
using OpenTK.Audio;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using QuickFont;

using RoversSpirit.Entities;
using RoversSpirit.Graphics;
using RoversSpirit.Physics;
using System.Threading;

namespace RoversSpirit
{
	public class MainWindow : GameWindow
	{
		/// <summary>
		/// set to true to exit game
		/// </summary>
		public static bool exit = false;
		public static IState state;

		/// <summary>
		/// set to true to start reading, will display whatever readingText is.
		/// </summary>
		public static bool reading = false;
		public static string readingText = string.Empty;

		private static IState curState;

		public ColorBox paper;
		public QFont ink;

		public static AudioContext context = new AudioContext();

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

			curState = new MenuState();
			state = curState;

			this.VSync = VSyncMode.On;

			Keyboard.KeyDown += OnKeyDown;
			Keyboard.KeyUp += OnKeyUp;
			Mouse.ButtonDown += OnMouseDown;
			Mouse.ButtonUp += OnMouseUp;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			Resources.LoadAll();

			curState.OnLoad(e);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

			Resources.UpdateAudioBuffers(e.Time);

			if (!reading)
				curState.OnUpdateFrame(e, Keyboard, Mouse);

			//exit the game
			if (exit)
				this.Exit();

			//switch states
			if (state != curState)
			{
				curState.OnUnload(new EventArgs());
				curState = state;
				curState.OnLoad(new EventArgs());
				curState.OnResize(new EventArgs(), ClientSize);
			}
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			curState.OnRenderFrame(e);

			//TODO render reading stuff here
			if (reading)
			{
				GL.PushMatrix();
				GL.EnableClientState(ArrayCap.VertexArray);
				paper.Draw();
				GL.DisableClientState(ArrayCap.VertexArray);
				GL.PopMatrix();
				GL.PushMatrix();
				RectangleF test = paper.GetBoundsWithBorder(16);
				ink.Print(readingText, paper.GetBoundsWithBorder(16), QFontAlignment.Left);
				GL.BindTexture(TextureTarget.Texture2D, 0);
				GL.Color4(1.0f, 1.0f, 1.0f, 1.0f);
				GL.PopMatrix();
			}

			SwapBuffers();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			curState.OnResize(e, ClientSize);

			if(paper != null)
				paper.Unload();

			float paperHeight = ClientSize.Height - 64;
			float paperWidth = 8.5f * paperHeight / 11.0f;

			paper = new ColorBox(new Vector2(ClientSize.Width / 2 - paperWidth / 2, ClientSize.Height / 2 - paperHeight / 2), new Vector2(paperWidth, paperHeight), Color4.White);
			ink = new QFont("Resources/Fonts/Graziano.ttf", ClientSize.Height / 16);
			ink.Options.Colour = Color.Black;
		}

		private void OnKeyDown(object sender, KeyboardKeyEventArgs e)
		{
			if (!reading)
				curState.OnKeyDown(sender, e, Keyboard, Mouse);
			else
				reading = false;
		}

		private void OnKeyUp(object sender, KeyboardKeyEventArgs e)
		{
			if (!reading)
				curState.OnKeyUp(sender, e, Keyboard, Mouse);
		}

		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			if (!reading)
				curState.OnMouseDown(sender, e, Keyboard, Mouse);
		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			if (!reading)
				curState.OnMouseUp(sender, e, Keyboard, Mouse);
		}

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);

			curState.OnUnload(e);

			Resources.UnloadTextures();
			Resources.UnloadAudioBuffers();
		}
	}
}

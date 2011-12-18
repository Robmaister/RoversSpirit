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

		private static IState curState;

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
			SwapBuffers();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			curState.OnResize(e, ClientSize);
		}

		private void OnKeyDown(object sender, KeyboardKeyEventArgs e)
		{
			curState.OnKeyDown(sender, e, Keyboard, Mouse);
		}

		private void OnKeyUp(object sender, KeyboardKeyEventArgs e)
		{
			curState.OnKeyUp(sender, e, Keyboard, Mouse);
		}

		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			curState.OnMouseDown(sender, e, Keyboard, Mouse);
		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
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

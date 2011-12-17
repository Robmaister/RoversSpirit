using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RoversSpirit.Graphics
{
	public class Camera
	{
		private Vector2 position;
		private Matrix4 view;
		private Matrix4 proj, uiProj;

		public Vector2 Position { get { return position; } }
		public Matrix4 ViewMatrix { get { return view; } }
		public Matrix4 WorldProjection { get { return proj; } }
		public Matrix4 UIProjection { get { return uiProj; } }

		public float UnitScale { get; set; }

		public Camera()
			: this(new Vector2(0, 0))
		{
		}

		public Camera(Vector2 position)
		{
			this.position = position;
			RebuildView();
		}

		/// <summary>
		/// Immediately moves camera to position
		/// </summary>
		/// <param name="position">position to move to</param>
		public void JumpTo(Vector2 position)
		{
			this.position = position;
			RebuildView();
		}

		/// <summary>
		/// Immediately offsets the camera by distance
		/// </summary>
		/// <param name="distance">The amount to offset the camera</param>
		public void JumpBy(Vector2 distance)
		{
			position += distance;
			RebuildView();
		}

		public void MoveTo(Vector2 position)
		{

		}

		public void MoveBy(Vector2 distance)
		{
		}

		public void Update(double time)
		{
		}

		public void OnResize(Size ClientSize)
		{
			proj = Matrix4.CreateOrthographic(ClientSize.Width / UnitScale, ClientSize.Height / UnitScale, 0, 1);
			RebuildView();

			//negative height because QuickFont is upside down otherwise.
			uiProj = Matrix4.CreateOrthographic(ClientSize.Width, -ClientSize.Height, 0, 1);
		}

		public void UseWorldProjection()
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref uiProj);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref view);
		}

		public void UseUIProjection()
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref uiProj);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
		}

		private void RebuildView()
		{
			view = Matrix4.CreateTranslation(position.X, position.Y, 0);
		}
	}
}

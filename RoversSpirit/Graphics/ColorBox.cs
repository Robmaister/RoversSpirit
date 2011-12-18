using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace RoversSpirit.Graphics
{
	public class ColorBox
	{
		private BufferSet bufSet;
		private Matrix4 model;
		private Vector2 position, size;

		public Color4 Color { get; set; }

		/// <summary>
		/// position is top left of box.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="size"></param>
		/// <param name="color"></param>
		public ColorBox(Vector2 position, Vector2 size, Color4 color)
		{
			this.position = position;
			this.size = size;
			model = Matrix4.CreateTranslation(position.X, position.Y, 0);
			Color = color;

			float[] vertices = new float[]
			{
				0, 0,
				0, size.Y,
				size.X, 0,
				size.X, size.Y
			};

			uint[] indices = new uint[]
			{
				0, 1, 2, 3
			};

			VBO vert = new VBO(), ind = new VBO();
			vert.SetData(ref vertices, BufferUsageHint.StaticDraw);
			ind.SetData(ref indices, BufferUsageHint.StaticDraw);

			bufSet = new BufferSet();
			bufSet.VertexBuffer = vert;
			bufSet.IndexBuffer = ind;
			bufSet.VertexSize = 2;
			bufSet.DrawMode = BeginMode.TriangleStrip;
			bufSet.SetDrawState(DrawStates.Vertex);
		}

		public void Draw()
		{
			GL.MultMatrix(ref model);
			GL.Color4(Color);
			bufSet.Draw();
			GL.Color4(1.0f, 1.0f, 1.0f, 1.0f);
		}

		public void Unload()
		{
			bufSet.Unload();
		}

		public RectangleF GetBoundsWithBorder(float borderSize)
		{
			return new RectangleF(position.X + borderSize, position.Y + borderSize, size.X - borderSize * 2, size.Y - borderSize * 2);
		}
	}
}

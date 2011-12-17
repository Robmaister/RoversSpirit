using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using RoversSpirit.Graphics;

namespace RoversSpirit
{
	public class Player
	{
		public BufferSet buffers;
		public Texture tex;

		private Matrix4 model;

		private Vector2 position;

		public Player()
			: this(new Vector2(0, 0))
		{
		}

		public Player(Vector2 position)
		{
			float[] vertices = 
			{
				-16, 16,
				-16, -16,
				16, 16,
				16, -16
			};

			float[] texcoords =
			{
				0, 0,
				0, 1,
				1, 0,
				1, 1
			};

			byte[] indices = 
			{
				0, 1, 2, 3
			};

			VBO verts = new VBO(), texC = new VBO(), ind = new VBO();
			verts.SetData(ref vertices, BufferUsageHint.StaticDraw);
			texC.SetData(ref texcoords, BufferUsageHint.StaticDraw);
			ind.SetData(ref indices, BufferUsageHint.StaticDraw);

			buffers = new BufferSet();
			buffers.VertexBuffer = verts;
			buffers.VertexSize = 2;
			buffers.TexCoordBuffer = texC;
			buffers.TexCoordSize = 2;
			buffers.IndexBuffer = ind;
			buffers.DrawMode = BeginMode.TriangleStrip;
			buffers.SetDrawState(DrawStates.Vertex | DrawStates.TexCoord);

			tex = Resources.Textures["player.png"];

			this.position = position;
			RebuildModelMatrix();
		}

		public void Draw()
		{
			GL.MultMatrix(ref model);

			GL.BindTexture(TextureTarget.Texture2D, tex);
			buffers.Draw();
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		public void MoveBy(Vector2 distance)
		{
			position += distance;
			RebuildModelMatrix();
		}

		public void RebuildModelMatrix()
		{
			model = Matrix4.CreateTranslation(position.X, -position.Y, 0);
		}
	}
}

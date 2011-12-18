using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using RoversSpirit.Graphics;
using RoversSpirit.Physics;

namespace RoversSpirit
{
	public class Entity
	{
		private BufferSet buffers;

		protected float angle;
		protected Vector2 position, size;
		protected Matrix4 model;

		protected Texture tex;

		protected bool solid;

		public AABB BoundingBox
		{
			get
			{
				Vector2 halfSize = size * .5f;
				if (angle == MathHelper.PiOver2 || angle == 3 * MathHelper.PiOver2)
					return new AABB(position.X - halfSize.Y, position.X + halfSize.Y, position.Y + halfSize.X, position.Y - halfSize.X);
				else
					return new AABB(position.X - halfSize.X, position.X + halfSize.X, position.Y + halfSize.Y, position.Y - halfSize.Y);
			}
		}

		public Vector2 Position { get { return position; } }
		public Vector2 Size
		{
			get
			{
				if (angle == MathHelper.PiOver2 || angle == 3 * MathHelper.PiOver2)
					return new Vector2(size.Y, size.X);
				else
					return size;
			}
		}

		public bool Solid { get { return solid; } }

		public bool AllowPickup { get; protected set; }

		public float Angle
		{
			get { return angle; }
			set
			{
				angle = value;
				//snap to nearest 90deg angle.
				angle %= MathHelper.TwoPi;
				if (angle > 7 * MathHelper.PiOver4 && angle <= MathHelper.PiOver4) angle = 0;
				if (angle > MathHelper.PiOver4 && angle <= 3 * MathHelper.PiOver4) angle = MathHelper.PiOver2;
				if (angle > 3 * MathHelper.PiOver4 && angle <= 5 * MathHelper.PiOver4) angle = MathHelper.Pi;
				if (angle > 5 * MathHelper.PiOver4 && angle <= 7 * MathHelper.PiOver4) angle = 3 * MathHelper.PiOver2;

				RebuildModelMatrix();
			}
		}

		public string Name { get; protected set; }

		public Entity(Vector2 position, Vector2 size, Texture tex, Vector2 texScale, bool solid)
		{
			float x = size.X / 2.0f;
			float y = size.Y / 2.0f;

			float[] vertices = 
			{
				-x, y,
				-x, -y,
				x, y,
				x, -y
			};

			float[] texcoords =
			{
				0, 1,
				0, 0,
				1, 1,
				1, 0
			};

			byte[] indices = 
			{
				0, 1, 2, 3
			};

			if (texScale != Vector2.One)
			{
				for (int i = 0; i < texcoords.Length; i += 2)
				{
					texcoords[i] *= texScale.X;
					texcoords[i + 1] *= texScale.Y;
				}
			}

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

			this.size = size;
			this.position = position;
			this.tex = tex;

			this.solid = solid;

			RebuildModelMatrix();
		}

		public void Draw()
		{
			GL.MultMatrix(ref model);

			GL.BindTexture(TextureTarget.Texture2D, tex);
			buffers.Draw();
		}

		public virtual void Update(double time)
		{
		}

		public virtual void Unload()
		{
			buffers.Unload();
		}

		public void MoveBy(Vector2 distance)
		{
			position += distance;
			RebuildModelMatrix();
		}

		public void MoveTo(Vector2 position)
		{
			this.position = position;
			RebuildModelMatrix();
		}

		public void RebuildModelMatrix()
		{
			model = Matrix4.CreateRotationZ(angle) * Matrix4.CreateTranslation(position.X, position.Y, 0);
		}
	}
}

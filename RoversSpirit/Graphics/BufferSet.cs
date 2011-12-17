using System;

using OpenTK.Graphics.OpenGL;

namespace Monocle.Graphics
{
	/// <summary>
	/// An bitfield of possible buffers to draw with.
	/// </summary>
	[Flags]
	public enum DrawStates
	{
		/// <summary>Draw nothing.</summary>
		None = 0x00,

		/// <summary>Draw with the vertex buffer.</summary>
		Vertex = 0x01,

		/// <summary>Draw with the texture coordinate buffer.</summary>
		TexCoord = 0x02,

		/// <summary>Draw with the normal buffer.</summary>
		Normal = 0x04
	}

	public class BufferSet
	{
		#region Fields

		private DrawStates state;

		private int vertexSize;
		private int texCoordSize;
		private int normalSize;

		#endregion

		#region Properties

		public bool Exists
		{
			get
			{
				//the BufferSet exists if any of the underlying Buffers exist, at least from an OpenGL resource standpoint.
				return (VertexBuffer != null && VertexBuffer.Exists)
						|| (TexCoordBuffer != null && TexCoordBuffer.Exists)
						|| (NormalBuffer != null && NormalBuffer.Exists)
						|| (IndexBuffer != null && IndexBuffer.Exists);
			}
		}

		public VBO VertexBuffer { get; set; }

		public VBO TexCoordBuffer { get; set; }

		public VBO NormalBuffer { get; set; }

		public VBO IndexBuffer { get; set; }

		/// <summary>
		/// Gets or sets the number of components each vertex has. Only 2, 3, and 4 are valid numbers.
		/// </summary>
		public int VertexSize
		{
			get { return vertexSize; }
			set
			{
				if (value < 2 || value > 4)
					throw new ArgumentOutOfRangeException("value", "Vertex size must be 2, 3, or 4.");
				else
					vertexSize = value;
			}
		}

		/// <summary>
		/// Gets or sets the number of components each texture coordinate has. Only 2, 3, and 4 are valid numbers.
		/// </summary>
		public int TexCoordSize
		{
			get { return texCoordSize; }
			set
			{
				if (value < 2 || value > 4)
					throw new ArgumentOutOfRangeException("value", "Vertex size must be 2, 3, or 4.");
				else
					texCoordSize = value;
			}
		}

		/// <summary>
		/// Gets or sets the number of components each normal has. Only 2, 3, and 4 are valid numbers.
		/// </summary>
		public int NormalSize
		{
			get { return normalSize; }
			set
			{
				if (value < 2 || value > 4)
					throw new ArgumentOutOfRangeException("value", "Vertex size must be 2, 3, or 4.");
				else
					normalSize = value;
			}
		}

		public int VertexStride { get; set; }

		public int TexCoordStride { get; set; }

		public int NormalStride { get; set; }

		public int VertexOffset { get; set; }

		public int TexCoordOffset { get; set; }

		public int NormalOffset { get; set; }

		public BeginMode DrawMode { get; set; }

		#endregion

		/// <summary>
		/// Sets which vertex attributes to draw. Must be called after all included buffers are set.
		/// This operation is fairly expensive and should only be called during initialization.
		/// </summary>
		/// <example>SetDrawState(DrawStates.Vertex | DrawStates.TexCoord);</example>
		/// <param name="state">All the buffers that will be rendered</param>
		public void SetDrawState(DrawStates state)
		{
			this.state = state;

			//if no buffers will be drawn, no need to validate any buffers.
			if (state == DrawStates.None)
				return;

			//validate the buffers
			try
			{
				if ((state & DrawStates.Vertex) == DrawStates.Vertex)
				{
					ValidateBuffer(VertexBuffer, "Vertex buffer");
					VertexSize = vertexSize;
				}

				if ((state & DrawStates.TexCoord) == DrawStates.TexCoord)
				{
					ValidateBuffer(TexCoordBuffer, "Texture coordinate buffer");
					TexCoordSize = texCoordSize;
				}

				if ((state & DrawStates.Normal) == DrawStates.Normal)
				{
					ValidateBuffer(NormalBuffer, "Normal buffer");
					NormalSize = normalSize;
				}

				ValidateBuffer(IndexBuffer, "Index buffer");
			}
			catch
			{
				throw;
			}
		}

		public void Draw()
		{
			if (state == DrawStates.None)
				return;

			if ((state & DrawStates.Vertex) == DrawStates.Vertex)
			{
				VertexBuffer.Bind(BufferTarget.ArrayBuffer);
				VertexBuffer.UseVertexPointer(vertexSize, VertexStride, VertexOffset);
			}

			if ((state & DrawStates.TexCoord) == DrawStates.TexCoord)
			{
				TexCoordBuffer.Bind(BufferTarget.ArrayBuffer);
				TexCoordBuffer.UseTexCoordPointer(texCoordSize, TexCoordStride, TexCoordOffset);
			}

			if ((state & DrawStates.Normal) == DrawStates.Normal)
			{
				NormalBuffer.Bind(BufferTarget.ArrayBuffer);
				NormalBuffer.UseNormalPointer(NormalStride, NormalOffset);
			}

			//TODO this needs to become static/part of some graphics context class.
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			IndexBuffer.Bind(BufferTarget.ElementArrayBuffer);
			GL.DrawElements(DrawMode, IndexBuffer.Length, (DrawElementsType)IndexBuffer.DataType, 0);
			IndexBuffer.Unbind(BufferTarget.ElementArrayBuffer);
		}

		public void Unload()
		{
			if (VertexBuffer != null)
				VertexBuffer.Unload();

			if (TexCoordBuffer != null)
				VertexBuffer.Unload();

			if (NormalBuffer != null)
				NormalBuffer.Unload();

			if (IndexBuffer != null)
				IndexBuffer.Unload();
		}

		private static void ValidateBuffer(VBO buffer, string name)
		{
			if (buffer == null || !buffer.Exists)
				throw new InvalidOperationException(name + " does not exist or has not been initialized properly.");
		}
	}
}

using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RoversSpirit.Graphics
{
	public class VBO
	{
		/// <summary>
		/// The OpenGL handle to this VBO.
		/// </summary>
		private int id;

		/// <summary>
		/// Gets the OpenGL handle to this VBO.
		/// </summary>
		public int ID { get { return id; } set { id = value; } }

		public bool Exists
		{
			get
			{
				if (id == 0)
					return false;

				//set id = 0 ASAP after the buffer no longer exists.
				//Optimizes multiple calls to Exists. (GL.IsBuffer only gets called the first time)
				if (!GL.IsBuffer(id))
				{
					id = 0;
					return false;
				}

				return true;
			}
		}

		public VertexAttribPointerType DataType { get; private set; }

		public int Length { get; private set; }

		public int ByteLength { get; private set; }

		#region GetData

		public unsafe void GetData(int offset, int size, out sbyte[] data)
		{
			data = new sbyte[size];

			fixed (sbyte* ptr = data)
			{
				GetData((IntPtr)(offset * sizeof(sbyte)), (IntPtr)(size * sizeof(sbyte)), (IntPtr)ptr);
			}
		}

		public unsafe void GetData(int offset, int size, out byte[] data)
		{
			data = new byte[size];

			fixed (byte* ptr = data)
			{
				GetData((IntPtr)(offset * sizeof(byte)), (IntPtr)(size * sizeof(byte)), (IntPtr)ptr);
			}
		}

		public unsafe void GetData(int offset, int size, out short[] data)
		{
			data = new short[size];

			fixed (short* ptr = data)
			{
				GetData((IntPtr)(offset * sizeof(short)), (IntPtr)(size * sizeof(short)), (IntPtr)ptr);
			}
		}

		public unsafe void GetData(int offset, int size, out ushort[] data)
		{
			data = new ushort[size];

			fixed (ushort* ptr = data)
			{
				GetData((IntPtr)(offset * sizeof(ushort)), (IntPtr)(size * sizeof(ushort)), (IntPtr)ptr);
			}
		}

		public unsafe void GetData(int offset, int size, out int[] data)
		{
			data = new int[size];

			fixed (int* ptr = data)
			{
				GetData((IntPtr)(offset * sizeof(int)), (IntPtr)(size * sizeof(int)), (IntPtr)ptr);
			}
		}

		public unsafe void GetData(int offset, int size, out uint[] data)
		{
			data = new uint[size];

			fixed (uint* ptr = data)
			{
				GetData((IntPtr)(offset * sizeof(uint)), (IntPtr)(size * sizeof(uint)), (IntPtr)ptr);
			}
		}

		public unsafe void GetData(int offset, int size, out float[] data)
		{
			data = new float[size];

			fixed (float* ptr = data)
			{
				GetData((IntPtr)(offset * sizeof(float)), (IntPtr)(size * sizeof(float)), (IntPtr)ptr);
			}
		}

		public unsafe void GetData(int offset, int size, out double[] data)
		{
			data = new double[size];

			fixed (double* ptr = data)
			{
				GetData((IntPtr)(offset * sizeof(double)), (IntPtr)(size * sizeof(double)), (IntPtr)ptr);
			}
		}

		public unsafe void GetData(int offset, int size, out Half[] data)
		{
			data = new Half[size];

			fixed (Half* ptr = data)
			{
				GetData((IntPtr)(offset * Half.SizeInBytes), (IntPtr)(size * Half.SizeInBytes), (IntPtr)ptr);
			}
		}

		#endregion

		#region SetData

		public unsafe void SetData(ref sbyte[] data, BufferUsageHint usageHint)
		{
			fixed (sbyte* ptr = data)
			{
				SetData((IntPtr)ptr, (IntPtr)(data.Length * sizeof(sbyte)), usageHint);
			}

			Length = data.Length;
			ByteLength = data.Length * sizeof(sbyte);
			DataType = VertexAttribPointerType.Byte;
		}

		public unsafe void SetData(ref byte[] data, BufferUsageHint usageHint)
		{
			fixed (byte* ptr = data)
			{
				SetData((IntPtr)ptr, (IntPtr)(data.Length * sizeof(byte)), usageHint);
			}

			Length = data.Length;
			ByteLength = data.Length * sizeof(byte);
			DataType = VertexAttribPointerType.UnsignedByte;
		}

		public unsafe void SetData(ref short[] data, BufferUsageHint usageHint)
		{
			fixed (short* ptr = data)
			{
				SetData((IntPtr)ptr, (IntPtr)(data.Length * sizeof(short)), usageHint);
			}

			Length = data.Length;
			ByteLength = data.Length * sizeof(short);
			DataType = VertexAttribPointerType.Short;
		}

		public unsafe void SetData(ref ushort[] data, BufferUsageHint usageHint)
		{
			fixed (ushort* ptr = data)
			{
				SetData((IntPtr)ptr, (IntPtr)(data.Length * sizeof(ushort)), usageHint);
			}

			Length = data.Length;
			ByteLength = data.Length * sizeof(ushort);
			DataType = VertexAttribPointerType.UnsignedShort;
		}

		public unsafe void SetData(ref int[] data, BufferUsageHint usageHint)
		{
			fixed (int* ptr = data)
			{
				SetData((IntPtr)ptr, (IntPtr)(data.Length * sizeof(int)), usageHint);
			}

			Length = data.Length;
			ByteLength = data.Length * sizeof(int);
			DataType = VertexAttribPointerType.Int;
		}

		public unsafe void SetData(ref uint[] data, BufferUsageHint usageHint)
		{
			fixed (uint* ptr = data)
			{
				SetData((IntPtr)ptr, (IntPtr)(data.Length * sizeof(uint)), usageHint);
			}

			Length = data.Length;
			ByteLength = data.Length * sizeof(uint);
			DataType = VertexAttribPointerType.UnsignedInt;
		}

		public unsafe void SetData(ref float[] data, BufferUsageHint usageHint)
		{
			fixed (float* ptr = data)
			{
				SetData((IntPtr)ptr, (IntPtr)(data.Length * sizeof(float)), usageHint);
			}

			Length = data.Length;
			ByteLength = data.Length * sizeof(float);
			DataType = VertexAttribPointerType.Float;
		}

		public unsafe void SetData(ref double[] data, BufferUsageHint usageHint)
		{
			fixed (double* ptr = data)
			{
				SetData((IntPtr)ptr, (IntPtr)(data.Length * sizeof(double)), usageHint);
			}

			Length = data.Length;
			ByteLength = data.Length * sizeof(double);
			DataType = VertexAttribPointerType.Double;
		}

		public unsafe void SetData(ref Half[] data, BufferUsageHint usageHint)
		{
			fixed (Half* ptr = data)
			{
				SetData((IntPtr)ptr, (IntPtr)(data.Length * Half.SizeInBytes), usageHint);
			}

			Length = data.Length;
			ByteLength = data.Length * Half.SizeInBytes;
			DataType = VertexAttribPointerType.HalfFloat;
		}

		#endregion

		#region UpdateData

		public unsafe void UpdateData(int offset, int size, ref sbyte[] data)
		{
			fixed (sbyte* ptr = data)
			{
				UpdateData((IntPtr)(offset * sizeof(sbyte)), (IntPtr)(size * sizeof(sbyte)), (IntPtr)ptr);
			}
		}

		public unsafe void UpdateData(int offset, int size, ref byte[] data)
		{
			fixed (byte* ptr = data)
			{
				UpdateData((IntPtr)(offset * sizeof(byte)), (IntPtr)(size * sizeof(byte)), (IntPtr)ptr);
			}
		}

		public unsafe void UpdateData(int offset, int size, ref short[] data)
		{
			fixed (short* ptr = data)
			{
				UpdateData((IntPtr)(offset * sizeof(short)), (IntPtr)(size * sizeof(short)), (IntPtr)ptr);
			}
		}

		public unsafe void UpdateData(int offset, int size, ref ushort[] data)
		{
			fixed (ushort* ptr = data)
			{
				UpdateData((IntPtr)(offset * sizeof(ushort)), (IntPtr)(size * sizeof(ushort)), (IntPtr)ptr);
			}
		}

		public unsafe void UpdateData(int offset, int size, ref int[] data)
		{
			fixed (int* ptr = data)
			{
				UpdateData((IntPtr)(offset * sizeof(int)), (IntPtr)(size * sizeof(int)), (IntPtr)ptr);
			}
		}

		public unsafe void UpdateData(int offset, int size, ref uint[] data)
		{
			fixed (uint* ptr = data)
			{
				UpdateData((IntPtr)(offset * sizeof(uint)), (IntPtr)(size * sizeof(uint)), (IntPtr)ptr);
			}
		}

		public unsafe void UpdateData(int offset, int size, ref float[] data)
		{
			fixed (float* ptr = data)
			{
				UpdateData((IntPtr)(offset * sizeof(float)), (IntPtr)(size * sizeof(float)), (IntPtr)ptr);
			}
		}

		public unsafe void UpdateData(int offset, int size, ref double[] data)
		{
			fixed (double* ptr = data)
			{
				UpdateData((IntPtr)(offset * sizeof(double)), (IntPtr)(size * sizeof(double)), (IntPtr)ptr);
			}
		}

		public unsafe void UpdateData(int offset, int size, ref Half[] data)
		{
			fixed (Half* ptr = data)
			{
				UpdateData((IntPtr)(offset * Half.SizeInBytes), (IntPtr)(size * Half.SizeInBytes), (IntPtr)ptr);
			}
		}

		#endregion

		public void Bind(BufferTarget target)
		{
			GL.BindBuffer(target, ID);
		}

		public void UseVertexPointer(int vertexSize, int stride, int offset)
		{
			GL.VertexPointer(vertexSize, (VertexPointerType)DataType, stride, offset);
		}

		public void UseTexCoordPointer(int texCoordSize, int stride, int offset)
		{
			GL.TexCoordPointer(texCoordSize, (TexCoordPointerType)DataType, stride, offset);
		}

		public void UseNormalPointer(int stride, int offset)
		{
			GL.NormalPointer((NormalPointerType)DataType, stride, offset);
		}

		public void UseVertexAttribPointer(int location, int vertexSize, int stride, int offset)
		{
			GL.VertexAttribPointer(location, vertexSize, DataType, false, stride, offset);
		}

		public void Unbind(BufferTarget target)
		{
			GL.BindBuffer(target, 0);
		}

		public void Unload()
		{
			if (Exists)
				GL.DeleteBuffers(1, ref id);
		}

		private void GetData(IntPtr offset, IntPtr size, IntPtr data)
		{
			if (ID == 0)
				throw new InvalidOperationException("A buffer can't be retreived before it is given any data.");

			Bind(BufferTarget.ArrayBuffer);
			GL.GetBufferSubData(BufferTarget.ArrayBuffer, offset, size, data);
			Unbind(BufferTarget.ArrayBuffer);
		}

		private void SetData(IntPtr data, IntPtr size, BufferUsageHint usageHint)
		{
			if (ID == 0)
				GL.GenBuffers(1, out id);

			Bind(BufferTarget.ArrayBuffer);
			GL.BufferData(BufferTarget.ArrayBuffer, size, data, usageHint);
			Unbind(BufferTarget.ArrayBuffer);
		}

		private void UpdateData(IntPtr offset, IntPtr size, IntPtr data)
		{
			if (ID == 0)
				throw new InvalidOperationException("A buffer can't be updated before it is given any data. Use SetData instead.");

			Bind(BufferTarget.ArrayBuffer);
			GL.BufferSubData(BufferTarget.ArrayBuffer, offset, size, data);
			Unbind(BufferTarget.ArrayBuffer);
		}
	}
}

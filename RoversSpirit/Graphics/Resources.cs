using System;
using System.Collections.Generic;

namespace Monocle.Graphics
{
	public static class Resources
	{
		public static Dictionary<string, Texture> Textures { get; private set; }

		public static List<VBO> buffers = new List<VBO>();
		public static List<BufferSet> bufferSets = new List<BufferSet>();

		static Resources()
		{
			Textures = new Dictionary<string, Texture>();
			buffers = new List<VBO>();
			bufferSets = new List<BufferSet>();
		}

		public static void UnloadTextures()
		{
			foreach (KeyValuePair<string, Texture> pair in Textures)
				pair.Value.Unload();

			Textures.Clear();
		}

		public static void UnloadBuffers()
		{
			foreach (VBO vbo in buffers)
				vbo.Unload();

			buffers.Clear();
		}

		public static void UnloadBufferSets()
		{
			foreach (BufferSet set in bufferSets)
				set.Unload();

			bufferSets.Clear();
		}
	}
}

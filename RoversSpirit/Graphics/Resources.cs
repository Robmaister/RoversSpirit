using System;
using System.Collections.Generic;
using System.IO;

namespace RoversSpirit.Graphics
{
	public static class Resources
	{
		public static Dictionary<string, Texture> Textures { get; private set; }

		static Resources()
		{
			Textures = new Dictionary<string, Texture>();
		}

		public static void LoadAll()
		{
			DirectoryInfo d = new DirectoryInfo("Resources/Textures");

			foreach (FileInfo f in d.GetFiles())
			{
				if (f.Extension == ".png")
				{
					Textures.Add(f.Name, new Texture(f.FullName));
				}
			}
		}

		public static void UnloadTextures()
		{
			foreach (KeyValuePair<string, Texture> pair in Textures)
				pair.Value.Unload();

			Textures.Clear();
		}
	}
}

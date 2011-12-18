using System;
using System.Collections.Generic;
using System.IO;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using RoversSpirit.Audio;

namespace RoversSpirit.Graphics
{
	public static class Resources
	{
		public static Dictionary<string, Texture> Textures { get; private set; }
		public static Dictionary<string, AudioBuffer> Audio { get; private set; }

		static Resources()
		{
			Textures = new Dictionary<string, Texture>();
			Audio = new Dictionary<string, AudioBuffer>();
		}

		public static void LoadAll()
		{
			DirectoryInfo dt = new DirectoryInfo("Resources/Textures");

			foreach (FileInfo f in dt.GetFiles())
			{
				if (f.Extension == ".png")
				{
					Textures.Add(f.Name, new Texture(f.FullName));
				}
			}

			DirectoryInfo da = new DirectoryInfo("Resources/Audio");

			foreach (FileInfo f in da.GetFiles())
			{
				if (f.Extension == ".wav")
				{
					Audio.Add(f.Name, new AudioBuffer(f.FullName));
				}
			}

			Resources.Textures["pebble1.png"].MagFilter = TextureMagFilter.Nearest;
			Resources.Textures["pebble1.png"].MinFilter = TextureMinFilter.Nearest;
			Resources.Textures["pebble2.png"].MagFilter = TextureMagFilter.Nearest;
			Resources.Textures["pebble2.png"].MinFilter = TextureMinFilter.Nearest;
			Resources.Textures["playerf1.png"].MagFilter = TextureMagFilter.Nearest;
			Resources.Textures["playerf1.png"].MinFilter = TextureMinFilter.Nearest;
			Resources.Textures["playerf2.png"].MagFilter = TextureMagFilter.Nearest;
			Resources.Textures["playerf2.png"].MinFilter = TextureMinFilter.Nearest;
		}

		public static void UpdateAudioBuffers(double time)
		{
			foreach (KeyValuePair<string, AudioBuffer> pair in Audio)
			{
				pair.Value.Update(time);
			}
		}

		public static void StopAllAudio()
		{
			foreach (KeyValuePair<string, AudioBuffer> pair in Audio)
			{
				pair.Value.Stop();
				pair.Value.Looping = false;
			}
		}

		public static void UnloadTextures()
		{
			foreach (KeyValuePair<string, Texture> pair in Textures)
				pair.Value.Unload();

			Textures.Clear();
		}

		internal static void UnloadAudioBuffers()
		{
			foreach (KeyValuePair<string, AudioBuffer> pair in Audio)
				pair.Value.Unload();

			Audio.Clear();
		}
	}
}

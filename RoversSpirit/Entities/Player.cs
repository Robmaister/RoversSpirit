using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Audio.OpenAL;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using RoversSpirit.Graphics;
using RoversSpirit.Audio;

namespace RoversSpirit.Entities
{
	public class Player : Entity
	{
		public const float MoveSpeed = 176;

		private Texture texAltFrame;

		private double animTime = 0.0;

		private AudioBuffer aBuf;

		public bool Moving { get; set; }

		public List<Entity> Inventory { get; set; }

		public Player()
			: this(new Vector2(0, 0))
		{
		}

		public Player(Vector2 position)
			: base(position, new Vector2(64, 64), Resources.Textures["playerf1.png"], Vector2.One, true)
		{
			texAltFrame = Resources.Textures["playerf2.png"];
			aBuf = Resources.Audio["move.wav"];
			aBuf.Looping = true;

			Inventory = new List<Entity>();

			AllowPickup = false;
		}

		public override void Update(double time)
		{
			base.Update(time);

			if (Moving)
			{
				if (aBuf.State != ALSourceState.Playing)
					aBuf.Play();
				animTime += time;

				//on animation time, swap frames
				if (animTime >= 0.1)
				{
					Texture temp = tex;
					tex = texAltFrame;
					texAltFrame = temp;
					animTime = 0;
				}
			}

			else
			{
				aBuf.Stop();
			}
		}

		public void DrawInventory()
		{
			GL.Scale(1, -1, 1);
			GL.Translate(16, -16, 0);

			foreach (Entity ent in Inventory)
			{
				GL.Translate(32, 0, 0);
				GL.BindTexture(TextureTarget.Texture2D, ent.tex);
				ent.buffers.Draw();
			}

			GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		public Entity FindNameInInventory(string item)
		{
			foreach (Entity ent in Inventory)
			{
				if (ent.Name == item)
					return ent;
			}

			return null;
		}
	}
}

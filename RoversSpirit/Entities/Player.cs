using System;
using System.Media;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using RoversSpirit.Graphics;

namespace RoversSpirit.Entities
{
	public class Player : Entity
	{
		private Texture texAltFrame;

		private double animTime = 0.0;

		public bool Moving { get; set; }

		private SoundPlayer moveSound;

		private bool soundPlaying;

		public Player()
			: this(new Vector2(0, 0))
		{
		}

		public Player(Vector2 position)
			: base(position, new Vector2(64, 64), Resources.Textures["playerf1.png"], true)
		{
			texAltFrame = Resources.Textures["playerf2.png"];

			moveSound = new SoundPlayer("Resources/Audio/move.wav");
			moveSound.Load();

			soundPlaying = false;
		}

		public override void Update(double time)
		{
			base.Update(time);

			if (Moving)
			{
				if (!soundPlaying)
				{
					moveSound.PlayLooping();
					soundPlaying = true;
				}

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
				if (soundPlaying)
				{
					moveSound.Stop();
					soundPlaying = false;
				}
			}
		}
	}
}

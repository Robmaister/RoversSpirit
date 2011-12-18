using System;

using OpenTK;

using RoversSpirit.Graphics;
using RoversSpirit.Physics;

namespace RoversSpirit
{
	public class TriggerReading
	{
		public string readingText;
		
		public AABB BBox { get; private set; }

		public TriggerReading(Vector2 position, Vector2 size, string text)
		{
			readingText = text;
			BBox = new AABB(position.X - size.X / 2, position.X + size.X / 2, position.Y + size.Y / 2, position.Y - size.Y / 2);
		}

		public void Activate()
		{
			Resources.Audio["move.wav"].Looping = false;
			Resources.Audio["move.wav"].Stop();
			MainWindow.readingText = readingText;
			MainWindow.reading = true;
		}
	}
}

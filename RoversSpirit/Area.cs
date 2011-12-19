using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;

using RoversSpirit.Graphics;
using RoversSpirit.Physics;

namespace RoversSpirit
{
	public abstract class Area
	{
		public Color4 ClearColor { get; protected set; }
		public List<Entity> EntList { get; protected set; }
		public List<TriggerPickup> PickupTriggers { get; protected set; }
		public List<TriggerDoorOpen> DoorOpenTriggers { get; protected set; }
		public List<TriggerChangeArea> AreaChangeTriggers { get; protected set; }
		public List<TriggerButtonPress> ButtonPressTriggers { get; protected set; }
		public List<TriggerReading> ReadingTriggers { get; protected set; }
		public List<TriggerEndgame> EndgameTrigger { get; protected set; }

		public Area()
		{
			EntList = new List<Entity>();
			PickupTriggers = new List<TriggerPickup>();
			DoorOpenTriggers = new List<TriggerDoorOpen>();
			AreaChangeTriggers = new List<TriggerChangeArea>();
			ButtonPressTriggers = new List<TriggerButtonPress>();
			ReadingTriggers = new List<TriggerReading>();
			EndgameTrigger = new List<TriggerEndgame>();
		}

		public abstract Vector2 SetPlayerStartLocation(Type previousArea);
		public abstract void LoadContent(GameData data);
		public abstract void Unload();
	}
}

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

		public Area()
		{
			EntList = new List<Entity>();
			PickupTriggers = new List<TriggerPickup>();
			DoorOpenTriggers = new List<TriggerDoorOpen>();
			AreaChangeTriggers = new List<TriggerChangeArea>();
		}

		public abstract Vector2 SetPlayerStartLocation(Type previousArea);
		public abstract void LoadContent(GameData data);
		public abstract void Unload();
	}
}

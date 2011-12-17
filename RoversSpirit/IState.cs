using System;
using System.Drawing;

using OpenTK;
using OpenTK.Input;

namespace RoversSpirit
{
	public interface IState
	{
		void OnLoad(EventArgs e);
		void OnUpdateFrame(FrameEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse);
		void OnRenderFrame(FrameEventArgs e);
		void OnResize(EventArgs e, Size ClientSize);
		void OnKeyDown(object sender, KeyboardKeyEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse);
		void OnKeyUp(object sender, KeyboardKeyEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse);
		void OnMouseDown(object sender, MouseEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse);
		void OnMouseUp(object sender, MouseEventArgs e, KeyboardDevice Keyboard, MouseDevice Mouse);
		void OnUnload(EventArgs e);
	}
}

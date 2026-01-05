using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
	[AddComponentMenu("Trigger/Level/Trigger Effect")]
	[TriggerDoc("When Triggered, Plays an effect such as screen fade, screen shake, audio event, etc.")]
	public partial class TriggerEffect : TriggerReceiver
	{
		public AnimationEventReceiver.EventGroup.FadeEvent fade = AnimationEventReceiver.EventGroup.FadeEvent.Default;
		public AnimationEventReceiver.EventGroup.ScreenShakeEvent shake;
		new public AnimationEventReceiver.EventGroup.AudioEvent audio;
	}
}
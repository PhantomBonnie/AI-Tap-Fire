using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
	[AddComponentMenu("Trigger/Flow/Trigger Debug Message")]
	[TriggerDoc("When triggered, prints a message and trigger counter to the console. The message can optionally display the signal context.\n\n<b>Make sure your trigger debug level is set to OnlyTriggered or higher to see the output!</b>")]
	public partial class TriggerDebugMessage : TriggerReceiver
	{
		public string message;
		public bool printSignalContext;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
	[AddComponentMenu("Trigger/Flow/Trigger Named Signal Receiver")]
	[TriggerDoc("Receives named signals sent from TriggerNamedSignalSender. Can optionally receive global signals.")]
	public partial class TriggerNamedSignalReceiver : TriggerReceiver
	{
		public string signalName;
		public bool receiveGlobalSignals;

		[SignalDoc("Sent when a signal with the specified signal name is received.", actor = "Same as received signal", squad = "Same as received signal", vehicle = "Same as received signal", weapon = "Same as received signal")]
		[AutoPopulateChildReceivers]
		public TriggerSend onSignalReceived;
	}
}
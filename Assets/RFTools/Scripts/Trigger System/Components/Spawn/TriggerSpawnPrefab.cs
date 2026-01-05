using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
	[AddComponentMenu("Trigger/Spawn/Trigger Spawn Prefab")]
	[TriggerDoc("When triggered, Instantiates a prefab at the specified SpawnPoint. If PropagateSignalToPrefab is true, propagates the signal to a trigger receiver on the prefab root GameObject. Propagated signal can optionally be sent as a named signal to TriggerNamedSignalReceiver components on the prefab or its' children.")]
	public partial class TriggerSpawnPrefab : TriggerReceiver
	{
		public enum PropagateType
		{
			RootTriggerReceiver,
			SendNamedSignal,
		}

		public GameObject prefab;
		public Transform spawnPoint;
		public bool attachToSpawnPointParentTransform = false;

		public bool propagateSignalToPrefab = false;

		[ConditionalField("propagateSignalToPrefab", true)]
		public PropagateType propagateType = PropagateType.RootTriggerReceiver;

		[ConditionalField("propagateType", PropagateType.SendNamedSignal)]
		public string signalName;

		[SignalDoc("Sent when the prefab is spawned")]
		public TriggerSend onSpawnCompleteTrigger;
	}
}
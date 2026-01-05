using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
    [AddComponentMenu("Trigger/Events/Trigger On Actor Enter Volume")]
    [TriggerDoc("Sends OnEnter/OnExit when an actor passing the filter enters/exits the trigger volume.")]
    public partial class TriggerOnActorEnter : TriggerBaseComponent
    {
        public TriggerVolume triggerVolume;

        public ActorFilter filter;

        [SignalDoc("Sent when an actor enters the volume", actor = "The entering actor", contextSource = SignalDocAttribute.ContextSource.Actor)]
        public TriggerSend onEnter;

        [SignalDoc("Sent when an actor exits the volume", actor = "The exiting actor", contextSource = SignalDocAttribute.ContextSource.Actor)]
        public TriggerSend onExit;

	}
}
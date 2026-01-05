using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
    [AddComponentMenu("Trigger/Events/Trigger On Loadout Accept")]
    [TriggerDoc("Sends a signal when the player accepts their loadout thru the built in UI.")]
    public partial class TriggerOnLoadoutAccept : TriggerBaseComponent
    {
        [SignalDoc("Sent when the player accepts their loadout.")]
        public TriggerSend onLoadoutAccept;
    }
}
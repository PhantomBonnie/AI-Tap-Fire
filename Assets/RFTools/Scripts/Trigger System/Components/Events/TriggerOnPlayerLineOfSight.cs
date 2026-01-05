using Ravenfield.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
    [AddComponentMenu("Trigger/Events/Trigger On Player Line Of Sight")]
    [TriggerDoc("Sends OnEnter/OnExit when this object enters/exits players line of sight. DirectLookAmount value can be used to shrink the screen area that is considered being inside FOV, with 0 being the entire screen, 1 being a very small area at the center of the screen.")]
    public partial class TriggerOnPlayerLineOfSight : TriggerBaseComponent
    {
        [Range(0f, 1f)] public float directLookAmount = 0f;

        [Header("Vis Ray Block")]
        public bool blockedByAIVisionOccluder = false;
        public bool blockedByVehicles = false;

        [Header("Sends")]
        [SignalDoc("Sent when this transform enters line of sight")]
        public TriggerSend onEnterLineOfSight;
        [SignalDoc("Sent when this transform exits line of sight")]
        public TriggerSend onExitLineOfSight;
    }
}
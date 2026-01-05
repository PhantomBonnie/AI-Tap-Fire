using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
    [AddComponentMenu("Actor/Trigger Change Stance")]
    [TriggerDoc("When Triggered, Changes the stance of the actor.")]
    public partial class TriggerChangeStance : TriggerReceiver
    {
        public ActorReference actor;
        public Actor.Stance stance;
    }
}
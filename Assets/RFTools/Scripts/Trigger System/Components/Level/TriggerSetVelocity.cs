using Ravenfield.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
    [AddComponentMenu("Trigger/Level/Trigger Set Velocity")]
    [TriggerDoc("When triggered, Sets the velocity of a Rigidbody.")]
    public partial class TriggerSetVelocity : TriggerReceiver
    {
        public enum TargetType
        {
            RigidBody,
            Vehicle,
        }
        public enum AccelerationMode
        {
            SetVelocity,
            Accelerate,
            InstantAddForce,
        }

        public TargetType type;
        public AccelerationMode mode;

        [ConditionalField("type", TargetType.RigidBody)] new public Rigidbody rigidbody;
        [ConditionalField("type", TargetType.Vehicle)] public VehicleReference vehicle;

        public bool isLocalVelocity = false;
        public Vector3 velocityAmount;
    }
}
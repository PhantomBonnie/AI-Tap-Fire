using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
    [AddComponentMenu("Trigger/Flow/Trigger Named Signal Sender")]
    [TriggerDoc("When triggered, Propagates the signal to all active TriggerNamedSignalReceiver components with the same signal name that are attached to the target GameObject or its' children. Can optionally send the signal to all global receivers, which can be used for signaling between game modes, maps and mutators.")]
    public partial class TriggerNamedSignalSender : TriggerReceiver
    {
        public enum TargetType
		{
            GameObject,
            Vehicle,
            Weapon,
            Global,
		}

        public string signalName;

        public TargetType target;

        [ConditionalField("target", TargetType.GameObject)]
        public GameObject targetGameObject;

        [ConditionalField("target", TargetType.Vehicle)]
        public VehicleReference targetVehicle;

        [ConditionalField("target", TargetType.Weapon)]
        public WeaponReference targetWeapon;
    }
}
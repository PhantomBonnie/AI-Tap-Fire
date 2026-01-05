using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
    [AddComponentMenu("Trigger/Level/Trigger Damage Vehicle")]
    [TriggerDoc("When triggered, Damages or destroys the target vehicle.")]
    public partial class TriggerDamageVehicle : TriggerReceiver
    {
        public enum DamageType
		{
            DealDamage,
            Disable,
            InstantlyDestroy,
		}

        public VehicleReference vehicle;
        public DamageType type;

        [ConditionalField("type", DamageType.DealDamage)] public int damageAmount;


        public bool useDamageCredit = false;
        [ConditionalField("useDamageCredit", true)] public ActorReference creditedActor;


    }
}
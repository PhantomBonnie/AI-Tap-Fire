using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
    [System.Serializable]
    public struct VehicleFilter
    {
        public Filter[] filters;

        [System.Serializable]
        public struct Filter
        {
            public enum Type
            {
                PlayerIsInside,
                OwnedByTeam,
                HasPlayerDriver,
                HasDriver,
                HasAnyoneSeated,
                AllSeatsTaken,
                IsVehicle,
            }

            public Type type;
            [ConditionalField("type", Type.OwnedByTeam)] public Team team;
            [ConditionalField("type", Type.IsVehicle)] public VehicleReference vehicle;

            public bool invertFilter;
		}
    }
}
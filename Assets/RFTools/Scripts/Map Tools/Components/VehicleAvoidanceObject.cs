using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAvoidanceObject : MonoBehaviour
{
	public enum Layer
	{
		Ground,
		Water,
	}

    public float radius = 2f;
	public Layer layer;
}

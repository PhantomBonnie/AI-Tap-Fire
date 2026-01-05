using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VehicleAvoidanceObject))]
public class VehicleAvoidanceObjectEditor : Editor
{

	private void OnSceneGUI() {
		var avoidance = (VehicleAvoidanceObject)this.target;
		Handles.color = Color.red;
		Handles.CircleHandleCap(-1, avoidance.transform.position, Quaternion.AngleAxis(90f, Vector3.right), avoidance.radius/2f, EventType.Repaint);

		Handles.color = Color.white;
	}
}

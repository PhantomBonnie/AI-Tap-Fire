using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleSpawner : MonoBehaviour {

	static readonly string[] meshSources = new string[] {
		"Preview/jeep",
		"Preview/jeep_machinegun",
		"Preview/quad",
		"Preview/tank",
		"Preview/helicopter",
		"Preview/plane",
		"Preview/rhib",
		"Preview/attackboat",
		"Preview/bomber",
		"Preview/helicopter_transport",
        "Preview/apc",
    };

	static Mesh[] previewMeshes;

	static void SetupPreviewMeshes() {
		previewMeshes = new Mesh[meshSources.Length];

		for(int i = 0; i < meshSources.Length; i++) {
			GameObject prefab = Resources.Load(meshSources[i]) as GameObject;
			if(prefab != null) {
				previewMeshes[i] = prefab.GetComponentInChildren<MeshFilter>().sharedMesh;
			}
		}
	}

	public enum VehicleSpawnType {Jeep, JeepMachineGun, Quad, Tank, AttackHelicopter, AttackPlane, Rhib, AttackBoat, BombPlane, TransportHelicopter, Apc};

	public enum SpawnBehaviour { Default, OnlyScripted }

	public SpawnBehaviour spawnBehaviour = SpawnBehaviour.Default;
	public float spawnTime = 16f;
	public enum RespawnType {AfterDestroyed, AfterMoved, Never};
	public RespawnType respawnType = RespawnType.AfterDestroyed;

	public VehicleSpawnType typeToSpawn;
	public GameObject prefab;

	public byte priority = 0;

	public bool isRelevantPathfindingPointForBoats = true;

	void OnValidate() {
		if(previewMeshes == null) {
			SetupPreviewMeshes();
		}

		try {
			MeshFilter meshFilter = GetComponent<MeshFilter>();
			try {
				meshFilter.mesh = previewMeshes[(int) this.typeToSpawn];
			}
			catch {
				meshFilter.mesh = previewMeshes[0];
			}
		}
		catch(System.Exception) { }
	}

	public enum VehicleCategory
	{
		Ground,
		Air,
		Naval,
		Unknown,
	}

	public static readonly Dictionary<VehicleSpawnType, VehicleCategory> TYPE_TO_CATEGORY = new Dictionary<VehicleSpawnType, VehicleCategory>() {
		{ VehicleSpawnType.Jeep, VehicleCategory.Ground},
		{ VehicleSpawnType.JeepMachineGun, VehicleCategory.Ground},
		{ VehicleSpawnType.Quad, VehicleCategory.Ground},
		{ VehicleSpawnType.Tank, VehicleCategory.Ground},
		{ VehicleSpawnType.Apc, VehicleCategory.Ground},

		{ VehicleSpawnType.AttackHelicopter, VehicleCategory.Air},
		{ VehicleSpawnType.AttackPlane, VehicleCategory.Air},
		{ VehicleSpawnType.BombPlane, VehicleCategory.Air},
		{ VehicleSpawnType.TransportHelicopter, VehicleCategory.Air},

		{ VehicleSpawnType.Rhib, VehicleCategory.Naval},
		{ VehicleSpawnType.AttackBoat, VehicleCategory.Naval},
	};

	public VehicleCategory GetVehicleCategory() {
		try {
			if (this.prefab != null) {
				return GetPrefabCategory(this.prefab.GetComponent<Vehicle>());
			}
			else {
				return TYPE_TO_CATEGORY[this.typeToSpawn];
			}
		}
		catch(System.Exception e) {
			Debug.LogException(e);
			return VehicleCategory.Unknown;
		}
	}

	public VehicleCategory GetPrefabCategory(Vehicle vehicle) {
		if(vehicle == null) {
			return VehicleCategory.Unknown;
		}

		if(vehicle is Helicopter || vehicle is Airplane) {
			return VehicleCategory.Air;
		}
		else if(vehicle is Boat) {
			return VehicleCategory.Naval;
		}
		else {
			return VehicleCategory.Ground;
		}
	}
}

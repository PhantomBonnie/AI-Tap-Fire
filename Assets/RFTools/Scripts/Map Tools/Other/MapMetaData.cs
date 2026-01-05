using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public struct MapMetaData
{
	public enum MapBiome
	{
		Unknown,
		Grass,
		Desert,
		Jungle,
		Snow,
		Gloom,
	}

	public enum MapTerrain
	{
		Unknown,
		Plains,
		Forest,
		Mountains,
		Coastline,
		Island,
		Ocean,
	}

	public enum MapDecorator
	{
		Unknown,
		Nothing,
		Settlement,
		City,
		Airport,
		Factory,
		Base,
		Temple,
	}

	public static MapMetaData Default {
		get {
			return new MapMetaData() {
				hasBuiltInGameMode = false,
				visibleInInstantAction = true,
				suggestedBots = 60,
				loadingScreenFOV = 20f,
			};
		}
	}


	// Instant action info
	public bool hasBuiltInGameMode;
	public bool usesTriggerSystem;
	public bool visibleInInstantAction;
	public int suggestedBots;
	public string displayName;

	// Auto gameplay tags
	public bool hasNavalVehicles;
	public bool hasGroundVehicles;
	public bool hasAirVehicles;

	// Conquest presentation
	public MapBiome mapBiome;
	public MapTerrain mapTerrain;
	public MapDecorator mapDecorator;

	// Loading screen
	public string loadingScreenBackgroundImage;
	public string loadingScreenModel;
	public string loadingScreenTexture;
	public bool loadingScreenUseArtworkShader;
	public float loadingScreenFOV;

	public static void AutoGenerateMetadataInfo(Scene scene, ref MapMetaData metadata) {

		metadata.hasBuiltInGameMode = FindInScene<GameModeBase>(scene).Count() > 0;
		metadata.usesTriggerSystem = FindInScene<Ravenfield.Trigger.TriggerBaseComponent>(scene).Count() > 0;

		var vehicleSpawners = FindInScene<VehicleSpawner>(scene);

		metadata.hasGroundVehicles = vehicleSpawners.Any(s => s.GetVehicleCategory() == VehicleSpawner.VehicleCategory.Ground);
		metadata.hasAirVehicles = vehicleSpawners.Any(s => s.GetVehicleCategory() == VehicleSpawner.VehicleCategory.Air);
		metadata.hasNavalVehicles = vehicleSpawners.Any(s => s.GetVehicleCategory() == VehicleSpawner.VehicleCategory.Naval);
	}

	static IEnumerable<T> FindInScene<T>(Scene scene) where T : Component {
		return scene.GetRootGameObjects().SelectMany(go => go.GetComponentsInChildren<T>(true));
	}
}

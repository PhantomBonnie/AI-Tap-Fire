using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class MetaDataWindow : EditorWindow
{
	const string PREVIEW_PATH = "Preview/Maps/";

	[MenuItem("Ravenfield Tools/Map/Metadata Editor")]
	public static void PublishToWorkshop() {
		MetaDataWindow window = EditorWindow.GetWindow<MetaDataWindow>();
		window.titleContent.text = "Scene Metadata";
		window.Show();
	}

	private void OnEnable() {

		this.soldierTexture = Resources.Load<Texture2D>($"{PREVIEW_PATH}preview_Soldiers");
		this.previewMaterial = Resources.Load<Material>($"{PREVIEW_PATH}preview_Material");

		LoadMetadata();
		EditorSceneManager.activeSceneChangedInEditMode += OnActiveSceneChange;
		MapExport.onBeforeMapExport += MapExport_onBeforeMapExport;
		MapExport.onAfterMapExport += MapExport_onAfterMapExport;
	}

	private void MapExport_onAfterMapExport() {
		// Metadata may have been generated during export so reload it
		LoadMetadata();
	}

	private void MapExport_onBeforeMapExport() {
		// Metadata will be generated on export so no need to do it twice
		SaveMetadata(autogenerateInfo: false);
	}

	private void OnDisable() {
		EditorSceneManager.activeSceneChangedInEditMode -= OnActiveSceneChange;
		MapExport.onBeforeMapExport -= MapExport_onBeforeMapExport;
		MapExport.onAfterMapExport -= MapExport_onAfterMapExport;
	}

	void OnActiveSceneChange(Scene a, Scene b) {
		LoadMetadata();
	}

	bool hasLoadedMetaData;
	MapMetaData metadata;
	bool showMapInfo = true;
	bool showLoadingScreenInfo = false;
	bool showGeneratedInfo = false;

	Texture2D mapPreviewTexture, decoratorPreviewTexture, soldierTexture;
	Material previewMaterial;

	void LoadOrCreateMetadata() {
		var currentScene = EditorSceneManager.GetSceneAt(0);

		if (!MetaDataUtils.MetaDataExistsFor(currentScene.path)) {
			MetaDataUtils.WriteMetaData(currentScene.path, MapMetaData.Default);
		}

		LoadMetadata();
	}

	void LoadMetadata() {
		var currentScene = EditorSceneManager.GetSceneAt(0);
		this.hasLoadedMetaData = MetaDataUtils.ReadMetaData(currentScene.path, out this.metadata, MapMetaData.Default);
		UpdatePreviewTextures();
	}

	void SaveMetadata(bool autogenerateInfo) {
		var currentScene = EditorSceneManager.GetSceneAt(0);

		if (autogenerateInfo) {
			MapMetaData.AutoGenerateMetadataInfo(currentScene, ref this.metadata);
		}

		MetaDataUtils.WriteMetaData(currentScene.path, this.metadata);
		Debug.Log($"Saved {currentScene.name} metadata!");

		MetaDataUtils.CopyMetaData(currentScene.path, MapExport.GetExportFilePath(currentScene));
	}

	void UpdatePreviewTextures() {

		if(this.metadata.mapBiome == MapMetaData.MapBiome.Unknown || this.metadata.mapTerrain == MapMetaData.MapTerrain.Unknown) {
			this.mapPreviewTexture = Resources.Load<Texture2D>($"{PREVIEW_PATH}preview_Unknown");
		}
		else {
			this.mapPreviewTexture = Resources.Load<Texture2D>($"{PREVIEW_PATH}preview_{this.metadata.mapBiome}_{this.metadata.mapTerrain}");
		}

		if(this.metadata.mapDecorator == MapMetaData.MapDecorator.Unknown || this.metadata.mapDecorator == MapMetaData.MapDecorator.Nothing) {
			this.decoratorPreviewTexture = Texture2D.blackTexture;
		}
		else {
			this.decoratorPreviewTexture = Resources.Load<Texture2D>($"{PREVIEW_PATH}preview_{this.metadata.mapDecorator}");
		}
		

		if(this.mapPreviewTexture == null) {
			this.mapPreviewTexture = Texture2D.blackTexture;
		}
		if (this.decoratorPreviewTexture == null) {
			this.decoratorPreviewTexture = Texture2D.blackTexture;
		}
	}

	void OnGUI() {
		if(GUILayout.Button("Create/Load Metadata")) {
			LoadOrCreateMetadata();
			GUI.FocusControl(null);
			Repaint();
		}

		if (!this.hasLoadedMetaData) return;

		EditorGUILayout.LabelField("Metadata:");

		this.metadata.displayName = EditorGUILayout.TextField("Display Name", this.metadata.displayName);
		this.metadata.suggestedBots = EditorGUILayout.IntField("Suggested Bots", this.metadata.suggestedBots);
		this.metadata.visibleInInstantAction = EditorGUILayout.Toggle("Visible in Instant Action", this.metadata.visibleInInstantAction);



		showMapInfo = EditorGUILayout.BeginFoldoutHeaderGroup(showMapInfo, "Map Preview");

		if(showMapInfo) {
			EditorGUI.indentLevel++;

			EditorGUI.BeginChangeCheck();
			this.metadata.mapBiome = (MapMetaData.MapBiome)EditorGUILayout.EnumPopup("Biome", this.metadata.mapBiome);
			this.metadata.mapTerrain = (MapMetaData.MapTerrain)EditorGUILayout.EnumPopup("Terrain", this.metadata.mapTerrain);
			this.metadata.mapDecorator = (MapMetaData.MapDecorator)EditorGUILayout.EnumPopup("Decorator", this.metadata.mapDecorator);
			bool updatePreview = EditorGUI.EndChangeCheck();

			if(updatePreview) {
				UpdatePreviewTextures();
			}

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("", GUILayout.Width(512f), GUILayout.Height(256f));
			Rect imageRect = GUILayoutUtility.GetLastRect();

			EditorGUI.DrawPreviewTexture(imageRect, this.mapPreviewTexture, this.previewMaterial);
			EditorGUI.DrawPreviewTexture(imageRect, this.decoratorPreviewTexture, this.previewMaterial);
			EditorGUI.DrawPreviewTexture(imageRect, this.soldierTexture, this.previewMaterial);

			EditorGUI.indentLevel--;
		}

		EditorGUILayout.EndFoldoutHeaderGroup();

		showLoadingScreenInfo = EditorGUILayout.BeginFoldoutHeaderGroup(showLoadingScreenInfo, "Loading Screen");

		if (showLoadingScreenInfo) {
			EditorGUI.indentLevel++;
			this.metadata.loadingScreenBackgroundImage = EditorGUILayout.TextField("Background (png/jpg)", this.metadata.loadingScreenBackgroundImage);
			this.metadata.loadingScreenModel = EditorGUILayout.TextField("Model (obj)", this.metadata.loadingScreenModel);
			this.metadata.loadingScreenTexture = EditorGUILayout.TextField("Model Texture (png/jpg)", this.metadata.loadingScreenTexture);
			this.metadata.loadingScreenFOV = EditorGUILayout.FloatField("Field Of View", this.metadata.loadingScreenFOV);
			this.metadata.loadingScreenUseArtworkShader = EditorGUILayout.Toggle("Use 2D Artwork Shader", this.metadata.loadingScreenUseArtworkShader);
			EditorGUI.indentLevel--;
		}

		EditorGUILayout.EndFoldoutHeaderGroup();

		showGeneratedInfo = EditorGUILayout.BeginFoldoutHeaderGroup(showGeneratedInfo, "Generated Info");

		if (showGeneratedInfo) {
			EditorGUI.indentLevel++;
			EditorGUI.BeginDisabledGroup(true);

			EditorGUILayout.LabelField("These values are generated when saving metadata or exporting the map");

			EditorGUILayout.Toggle("Has built in game mode", this.metadata.hasBuiltInGameMode);
			EditorGUILayout.Toggle("Uses trigger system", this.metadata.usesTriggerSystem);

			EditorGUILayout.LabelField("Vehicle info");

			EditorGUILayout.Toggle("Has Ground Vehicles", this.metadata.hasGroundVehicles);
			EditorGUILayout.Toggle("Has Air Vehicles", this.metadata.hasAirVehicles);
			EditorGUILayout.Toggle("Has Naval Vehicles", this.metadata.hasNavalVehicles);

			EditorGUI.EndDisabledGroup();
			EditorGUI.indentLevel--;
		}

		EditorGUILayout.EndFoldoutHeaderGroup();

		if (GUILayout.Button("Save Changes")) {
			SaveMetadata(true);
		}
	}
}

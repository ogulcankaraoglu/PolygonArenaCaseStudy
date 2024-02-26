using UnityEditor;
using UnityEngine;

public class MapSetupWizard : EditorWindow
{
    public static string StaticMeshName = "static_meshes";
    public static string PlayerCollisionsName = "player_collisions";
    public static string BulletCollisionsName = "bullet_collisions";

    private string _MapName = "TestMap01";
    private GameObject _StaticMesh;
    private GameObject _PlayerCollisionsMesh;
    private GameObject _BulletCollisionsMesh;

    [MenuItem("Vertigo/Tools/Map Setup Wizard")]
    public static void ShowWindow()
    {
        MapSetupWizard window = GetWindow<MapSetupWizard>();
        window.titleContent = new GUIContent("Map Setup Wizard");
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();

        _MapName = EditorGUILayout.TextField("Map Name", _MapName);
        _StaticMesh = (GameObject)EditorGUILayout.ObjectField("Static Meshes (sm_) ", _StaticMesh, typeof(GameObject), false);
        _PlayerCollisionsMesh = (GameObject)EditorGUILayout.ObjectField("Box Collider Meshes (ubt_) ", _PlayerCollisionsMesh, typeof(GameObject), false);
        _BulletCollisionsMesh = (GameObject)EditorGUILayout.ObjectField("Bullet Collider Meshes (ubx_) ", _BulletCollisionsMesh, typeof(GameObject), false);

        if (GUILayout.Button("Create Map"))
        {
            CreateMap();
        }
    }

    private void CreateMap()
    {
        if (_StaticMesh == null || _PlayerCollisionsMesh == null || _BulletCollisionsMesh == null)
        {
            EditorUtility.DisplayDialog("Map Creation Problem!", "Assign All Of The Needed Meshes!", "OK");
            return;
        }

        GameObject mapParent = new GameObject();
        mapParent.name = _MapName;

        GameObject go_static = Instantiate(_StaticMesh);
        go_static.transform.SetParent(mapParent.transform);
        go_static.name = StaticMeshName;
        StaticMeshesSetup(go_static);

        GameObject go_boxcol = Instantiate(_PlayerCollisionsMesh);
        go_boxcol.transform.SetParent(mapParent.transform);
        go_boxcol.name = PlayerCollisionsName;
        BoxCollidersSetup(go_boxcol);

        GameObject go_physcol = Instantiate(_BulletCollisionsMesh);
        go_physcol.transform.SetParent(mapParent.transform);
        go_physcol.name = BulletCollisionsName;
        MeshCollidersSetup(go_physcol);
    }

    private void StaticMeshesSetup(GameObject _staticMeshes)
    {
        var staticFlags = StaticEditorFlags.ContributeGI | StaticEditorFlags.OccluderStatic | StaticEditorFlags.OccludeeStatic | StaticEditorFlags.BatchingStatic;
        var childTransform = _staticMeshes.transform.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer _renderer in childTransform)
        {
            if (_renderer.gameObject.name.Contains("csh"))
            {
                _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
            else
            {
                _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }
            GameObjectUtility.SetStaticEditorFlags(_renderer.gameObject, staticFlags);
        }
    }

    private void BoxCollidersSetup(GameObject _playerCollisions)
    {
        var childTransform = _playerCollisions.transform.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer item in childTransform)
        {
            item.gameObject.AddComponent<BoxCollider>();
            item.gameObject.layer = 6;
            MeshFilter meshFilter = item.GetComponent<MeshFilter>();
            DestroyImmediate(meshFilter);
            DestroyImmediate(item);
        }
    }

    private void MeshCollidersSetup(GameObject _bulletCollisions)
    {
        var childTransform = _bulletCollisions.transform.GetComponentsInChildren<MeshRenderer>(true);
        foreach (MeshRenderer item in childTransform)
        {
            item.gameObject.AddComponent<MeshCollider>();
            item.gameObject.layer = 7;
            MeshFilter meshFilter = item.GetComponent<MeshFilter>();
            DestroyImmediate(meshFilter);
            DestroyImmediate(item);
        }
    }



}

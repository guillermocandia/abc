using System;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class PrefabTile : TileBase
{
    [SerializeField] private GameObject prefab;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = prefab;
        tileData.sprite = prefab.GetComponent<SpriteRenderer>().sprite; ;
    }

    
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject gameObject)
    {
        gameObject.transform.position += Vector3.up * 0.16f + Vector3.right * 0.32f;
        return true;
    }


#if UNITY_EDITOR
    [MenuItem("Assets/Create/Prefab Tile")]
    public static void CreatePrefabTile()
    {
        string path = EditorUtility.SaveFilePanelInProject(
            "Save Prefab Tile",
            "New Prefab Tile",
            "asset",
            "Save Prefab Tile",
            "Assets"
        );

        if (path == "")
        {
            return;
        }

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<PrefabTile>(), path);
    }
#endif
}

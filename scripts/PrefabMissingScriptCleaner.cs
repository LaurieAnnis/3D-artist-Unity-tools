using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PrefabMissingScriptCleaner : EditorWindow
{
    private DefaultAsset selectedFolder;
    private int removedComponentsCount = 0;
    private int processedPrefabsCount = 0;

    [MenuItem("Tools/Clean Missing Scripts from Prefabs")]
    public static void ShowWindow()
    {
        GetWindow<PrefabMissingScriptCleaner>("Prefab Cleaner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Remove Missing Scripts from Prefabs", EditorStyles.boldLabel);
        
        selectedFolder = (DefaultAsset)EditorGUILayout.ObjectField(
            "Select Folder:", 
            selectedFolder, 
            typeof(DefaultAsset), 
            false
        );

        if (selectedFolder != null && !AssetDatabase.IsValidFolder(AssetDatabase.GetAssetPath(selectedFolder)))
        {
            EditorGUILayout.HelpBox("Please select a folder, not a file.", MessageType.Warning);
            selectedFolder = null;
        }

        EditorGUI.BeginDisabledGroup(selectedFolder == null);
        
        if (GUILayout.Button("Clean Missing Scripts"))
        {
            CleanMissingScripts();
        }
        
        EditorGUI.EndDisabledGroup();

        if (processedPrefabsCount > 0)
        {
            GUILayout.Space(10);
            GUILayout.Label($"Processed {processedPrefabsCount} prefabs");
            GUILayout.Label($"Removed {removedComponentsCount} missing components");
        }
    }

    private void CleanMissingScripts()
    {
        if (selectedFolder == null) return;

        removedComponentsCount = 0;
        processedPrefabsCount = 0;

        string folderPath = AssetDatabase.GetAssetPath(selectedFolder);
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { folderPath });
        var visitedPrefabs = new HashSet<Object>();

        for (int i = 0; i < prefabGuids.Length; i++)
        {
            string prefabPath = AssetDatabase.GUIDToAssetPath(prefabGuids[i]);
            
            EditorUtility.DisplayProgressBar(
                "Cleaning Prefabs", 
                $"Processing: {Path.GetFileName(prefabPath)}", 
                (float)i / prefabGuids.Length
            );

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab != null)
            {
                CleanPrefabRecursively(prefab, visitedPrefabs);
            }
        }

        EditorUtility.ClearProgressBar();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void CleanPrefabRecursively(GameObject prefabRoot, HashSet<Object> visitedPrefabs)
    {
        if (!visitedPrefabs.Add(prefabRoot)) return;

        var allObjects = prefabRoot.GetComponentsInChildren<Transform>(true)
            .Select(t => t.gameObject);

        foreach (var go in allObjects)
        {
            if (PrefabUtility.IsPartOfAnyPrefab(go))
            {
                var source = PrefabUtility.GetCorrespondingObjectFromSource(go);
                if (source != null && visitedPrefabs.Add(source))
                {
                    CleanGameObject(source);
                }
            }

            CleanGameObject(go);
        }

        processedPrefabsCount++;
    }

    private void CleanGameObject(GameObject go)
    {
        int missingCount = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
        if (missingCount > 0)
        {
            Undo.RegisterCompleteObjectUndo(go, "Remove missing scripts");
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
            removedComponentsCount += missingCount;
        }
    }
}

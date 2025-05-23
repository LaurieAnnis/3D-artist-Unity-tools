using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public static class RemoveMissingScriptsTool
{
    [MenuItem("Tools/Cleanup/Remove Missing Scripts in Scene")]
    public static void RemoveMissingScriptsInScene()
    {
        int removedCount = 0;
        int gameObjectCount = 0;

        // Loop through all open scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (!scene.isLoaded) continue;

            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject root in rootObjects)
            {
                GameObject[] allObjects = root.GetComponentsInChildren<Transform>(true)
                                              .Select(t => t.gameObject)
                                              .ToArray();
                foreach (GameObject go in allObjects)
                {
                    gameObjectCount++;
                    int before = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
                    if (before > 0)
                    {
                        Undo.RegisterCompleteObjectUndo(go, "Remove missing scripts");
                        GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                        removedCount += before;
                    }
                }
            }
        }

        EditorSceneManager.MarkAllScenesDirty();
        Debug.Log($"✅ Done. Removed {removedCount} missing script(s) from {gameObjectCount} GameObject(s).");
    }
}

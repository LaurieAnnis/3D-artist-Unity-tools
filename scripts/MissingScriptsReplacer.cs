using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.SceneManagement;

public class MissingScriptReplacer : EditorWindow
{
    [MenuItem("Tools/Replace Missing Scripts")]
    public static void ShowWindow()
    {
        GetWindow<MissingScriptReplacer>("Missing Script Replacer");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find and Replace Missing Scripts in Scene"))
        {
            FindAndReplaceMissingScripts();
        }
    }

    private void FindAndReplaceMissingScripts()
    {
        int replacedCount = 0;
        GameObject[] allGameObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject go in allGameObjects)
        {
            int numComponents = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
            if (numComponents > 0)
            {
                // Add placeholder components first (before removing the missing ones)
                for (int i = 0; i < numComponents; i++)
                {
                    go.AddComponent<MissingScriptPlaceholder>();
                }
                
                // Remove missing script components
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                replacedCount += numComponents;
                
                // Mark object as dirty
                EditorUtility.SetDirty(go);
            }
        }

        if (replacedCount > 0)
        {
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Debug.Log($"Replaced {replacedCount} missing scripts with placeholders.");
        }
        else
        {
            Debug.Log("No missing scripts found.");
        }
    }
}

// Placeholder component to indicate where a missing script was
public class MissingScriptPlaceholder : MonoBehaviour
{
    [SerializeField]
    private string missingScriptNote = "This component replaced a missing script";
}

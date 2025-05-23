using UnityEngine;
using UnityEditor;

public static class RemoveMissingScriptPlaceholders
{
    [MenuItem("Tools/Cleanup/Remove Missing Script Placeholders")]
    public static void RemovePlaceholders()
    {
        int removedCount = 0;
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject go in allObjects)
        {
            var placeholders = go.GetComponents<MissingScriptPlaceholder>();
            foreach (var placeholder in placeholders)
            {
                Undo.DestroyObjectImmediate(placeholder);
                removedCount++;
            }
        }

        if (removedCount > 0)
        {
            Debug.Log($"🧼 Removed {removedCount} MissingScriptPlaceholder component(s).");
        }
        else
        {
            Debug.Log("No MissingScriptPlaceholder components found.");
        }
    }
}

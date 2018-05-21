using UnityEditor;
using UnityEngine;
 
public static class GroupCommand
{
    [MenuItem("GameObject/Group Selected %g")]
    private static void GroupSelected()
    {
        if (!Selection.activeTransform) return;
        var go = new GameObject(Selection.activeTransform.name + " Group");
        Undo.RegisterCreatedObjectUndo(go, "Group Selected");
        go.transform.SetParent(Selection.activeTransform.parent, false);
        foreach (var transform in Selection.transforms) Undo.SetTransformParent(transform, go.transform, "Group Selected");
        Selection.activeGameObject = go;
        go.AddComponent<UnrootChildrenAndDeleteOnRunTime>();
    }
    
    public class UnrootChildrenAndDeleteOnRunTime : MonoBehaviour {
        private void Awake() {
            while (transform.childCount > 0) {
                transform.GetChild(0).parent = null;
            }
            Destroy(this.gameObject);
        }
    }
}
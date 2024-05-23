using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCActor)), CanEditMultipleObjects]
public class NPCEditor : Editor
{
    private void OnSceneGUI()
    {
        var actor = (NPCActor)target;
        if (actor == null)
        {
            return;
        }

        Handles.color = Color.blue;
        Handles.Label(actor.transform.position + (Vector3.up * 2), $"{actor.npcName}\n{actor.transform.position}");
    }
}

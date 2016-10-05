using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class EditorDisableMEshRenderer : Editor
{
    [MenuItem("MyTools/Disable Mesh Renderers on Triggers")]
    public static void ShowWindow()
    {
        GameObject startingWorld;
        startingWorld = GameObject.Find("StartingWorld");
        Collider[] go;
        go = startingWorld.GetComponentsInChildren<Collider>();

        foreach (Collider item in go)
        {
            if (item.isTrigger && (item.name == "turnCollider" || item.name == "SOUTH" || item.name == "NORTH" || item.name == "WEST" || item.name == "EAST"))
            {
                if (item.GetComponent<MeshRenderer>().enabled)
                    item.GetComponent<MeshRenderer>().enabled = false;
                else
                    item.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

}

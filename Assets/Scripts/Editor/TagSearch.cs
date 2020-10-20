using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TagSearch : MonoBehaviour
{
    private static string SelectedTag = "Spawn";
 
    [MenuItem("Helpers/Select By Tag")]
    public static void SelectObjectsWithTag()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(SelectedTag);
        Selection.objects = objects;
    }
}

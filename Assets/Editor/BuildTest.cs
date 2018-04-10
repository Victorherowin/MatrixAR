using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[InitializeOnLoad]
public class FinishCompiling
{
	const string compilingKey = "Compiling";
	static bool compiling;
	static FinishCompiling()
	{
		compiling = EditorPrefs.GetBool(compilingKey, false);
		EditorApplication.update += Update;
	}

	static void Update()
	{
		if(compiling && !EditorApplication.isCompiling)
		{
			Debug.Log(string.Format("Compiling DONE {0}", DateTime.Now));
			compiling = false;
			EditorPrefs.SetBool(compilingKey, false);
		}
		else if (!compiling && EditorApplication.isCompiling)
		{
			Debug.Log(string.Format("Compiling START {0}", DateTime.Now));
			compiling = true;
			EditorPrefs.SetBool(compilingKey, true);
		}
	}

    [MenuItem("Edit/Cleanup Missing Scripts")]
    static void CleanupMissingScripts()
    {
        List<GameObject> game_objects = new List<GameObject>();
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            game_objects.Add(Selection.gameObjects[i]);
            foreach(var obj in Selection.gameObjects[i].GetComponentsInChildren<Transform>())
            {
                game_objects.Add(obj.gameObject);
            }
        }

        foreach(var obj in game_objects)
        {
            DeleteMissScript(obj);
        }

    }

     static void DeleteMissScript(GameObject gameObject)
     {
        // We must use the GetComponents array to actually detect missing components
        var components = gameObject.GetComponents<Component>();

        // Create a serialized object so that we can edit the component list
        var serializedObject = new SerializedObject(gameObject);
        // Find the component list property
        var prop = serializedObject.FindProperty("m_Component");

        // Track how many components we've removed
        int r = 0;

        // Iterate over all components
        for (int j = 0; j < components.Length; j++)
        {
            // Check if the ref is null
            if (components[j] == null)
            {
                // If so, remove from the serialized component array
                prop.DeleteArrayElementAtIndex(j - r);
                // Increment removed count
                r++;
            }
        }

        // Apply our changes to the game object
        serializedObject.ApplyModifiedProperties();
        //这一行一定要加！！！
        EditorUtility.SetDirty(gameObject);
    }
 }

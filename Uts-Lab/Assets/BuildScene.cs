using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class BuildScene
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        int indexOfSceneIfExist = -1;

        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            if (EditorBuildSettings.scenes[i].path == "Assets/Scenes/SceneRuangan.unity")
            {
                indexOfSceneIfExist = i;
                break;
            }
        }

        EditorBuildSettingsScene[] newScenes;

        if (indexOfSceneIfExist == -1)
        {
            newScenes = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length + 1];

            //Seems inefficent to add scene to build settings after creating each scene (rather than doing it all at once
            //after they are all created, however, it's necessary to avoid memory issues.
            int i = 0;
            for (; i < EditorBuildSettings.scenes.Length; i++)
                newScenes[i] = EditorBuildSettings.scenes[i];

            newScenes[i] = new EditorBuildSettingsScene("Assets/Scenes/SceneRuangan.unity", true);
        }
        else
        {
            newScenes = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length];

            int i = 0, j = 0;
            for (; i < EditorBuildSettings.scenes.Length; i++)
            {
                //skip over the scene that is a duplicate
                //this will effectively delete it from the build settings
                if (i != indexOfSceneIfExist)
                    newScenes[j++] = EditorBuildSettings.scenes[i];
            }
            newScenes[j] = new EditorBuildSettingsScene("Assets/Scenes/SceneRuangan.unity", true);
        }

        EditorBuildSettings.scenes = newScenes;
    }
}

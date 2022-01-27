#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace MyNameSpace._Editor
{
    [InitializeOnLoad]
    public static class EditorAutoSave
    {
        private static float timer = 300;
        public static float resetTimer = 300; //The amount of seconds it takes before it saves. Adjust the amount to your preference

        static EditorAutoSave()
        {
            //Subscribe SaveOnPlay mechanic to the playModeStateChanged event
            EditorApplication.playModeStateChanged += SaveOnPlay;

            //Subscribe SaveOnPlay mechanic to the update event
            EditorApplication.update += SaveOnTime;

            //Subscribe SaveOnPlay mechanic to the quit event
            EditorApplication.quitting += SaveOnClose;
        }

        //Saves your open scenes whenever you exit edit mode
        public static void SaveOnPlay(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                Debug.Log("Auto Save : Exited Edit Mode");

                EditorSceneManager.SaveOpenScenes();
                AssetDatabase.SaveAssets();
            }
        }

        //Saves your open scenes
        public static void SaveOnClose()
        {
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
        }

        //Saves your open scenes based on a timer
        [ExecuteInEditMode]
        public static void SaveOnTime()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }

            else if (timer <= 0 && !EditorApplication.isPlaying)
            {
                Debug.Log("Auto Save : Timed");
                EditorSceneManager.SaveOpenScenes();
                AssetDatabase.SaveAssets();
                timer = resetTimer;
            }
        }
    }
}
#endif
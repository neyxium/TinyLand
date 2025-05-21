#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class EditorStartFromMenu
{
    private static readonly string MenuScenePath = "Assets/Scenes/MainMenu.unity"; // spremeni po potrebi

    static EditorStartFromMenu()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            if (SceneManager.GetActiveScene().path != MenuScenePath)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().path);

                    EditorSceneManager.OpenScene(MenuScenePath);
                }
                else
                {
                    EditorApplication.isPlaying = false;
                }
            }
        }
        else if (state == PlayModeStateChange.EnteredEditMode)
        {
            if (EditorPrefs.HasKey("PreviousScene"))
            {
                string previousScene = EditorPrefs.GetString("PreviousScene");
                EditorSceneManager.OpenScene(previousScene);
                EditorPrefs.DeleteKey("PreviousScene");
            }
        }
    }
}
#endif

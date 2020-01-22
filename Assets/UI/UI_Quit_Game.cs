using UnityEngine;
using UnityEditor;

public class UI_Quit_Game : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quitting Game. Goodbye");
        // save any game data here
#if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
     EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

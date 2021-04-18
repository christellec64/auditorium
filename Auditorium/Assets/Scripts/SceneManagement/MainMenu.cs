using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour {
    public void ClickOnPlay( ) {
        //SceneManager.LoadScene("Level01") pour le nom de scene
        SceneManager.LoadScene(1);
    }

    public void ClickOnExit( ) {
        Application.Quit( );

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode( );
#endif
    }

    public void ClickOnLevel( string levelName ) {
        /* int success = PlayerPrefs.GetInt(levelName, 0);

        if(success == 1) {
        SceneManager.LoadScene(levelName);  
        }*/
        SceneManager.LoadScene(levelName);
    }
}

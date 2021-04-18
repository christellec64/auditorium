using UnityEngine.SceneManagement;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelMap : MonoBehaviour
{
    [SerializeField] private string _nextLevelScene;

    public void NextLevel( ) {
        SceneManager.LoadScene(_nextLevelScene);
    }

    public void MainMenu( ) {
        SceneManager.LoadScene("MainMenu");
    }

    public void ClickOnExit( ) {
        Application.Quit( );

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode( );
#endif
    }
}

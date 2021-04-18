using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelManager : MonoBehaviour
{
    [SerializeField] private WinCondition _conditions;
    [SerializeField] private string _nextLevelScene;
    [SerializeField] private string _currentLevel;
    [SerializeField] private float _winDelay =2f;
    // [SerializeField] double _startMusicDelay = 2f;
    private float _counter;
    private MusicBox[ ] _musicBoxes;
    private bool _isWin = false;

  private void Awake() {
        _musicBoxes = FindObjectsOfType<MusicBox>( );
        _counter = 0f;
    }

    private void Start( ) {
        StartMusicBoxes( );
    }

    // Update is called once per frame
    void Update()
    {
        Win( );
    }

    void Win() {
        _isWin = true;
        foreach ( MusicBox box in _musicBoxes ) {
            if ( !box.IsFull ) {
                _isWin = false;
            } 

        }
            

        if(_isWin) {
            _counter += Time.deltaTime;
        } else {
            _counter = 0f;
        }
        
        if(_counter >= _winDelay) {
            _conditions.ShowWinCanvas( );
            SaveProgression( );
        }
    }
    void StartMusicBoxes () {
        foreach(MusicBox box in _musicBoxes) {
            AudioSource audioSource = box.GetComponent<AudioSource>( );
            audioSource.Play( );
            // pour lancer la musique avec un délai :
           // audioSource.PlayScheduled(AudioSettings.dspTime + _startMusicDelay);
        }
    }
    void SaveProgression() {
        string sceneNAme = SceneManager.GetActiveScene( ).name;
        PlayerPrefs.SetInt(sceneNAme, 1);
    }
    public void NextLevel( ) {
        SceneManager.LoadScene(_nextLevelScene);
    }
    public void RestartLevel( ) {
        SceneManager.LoadScene(_currentLevel);
    }
    public void LevelMap( ) {
        SceneManager.LoadScene("LevelMap");
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

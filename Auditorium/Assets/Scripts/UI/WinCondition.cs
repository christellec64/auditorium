using UnityEngine;

public class WinCondition : MonoBehaviour {
    [SerializeField] private GameObject _winCanvas;
    private MusicBox[ ] _musicBoxes;
    private void Awake( ) {

        _winCanvas.SetActive(false);
        _musicBoxes = FindObjectsOfType<MusicBox>( );

    }

    public void ShowWinCanvas( ) {
        _winCanvas.SetActive(true);

        foreach ( MusicBox box in _musicBoxes ) {
            AudioSource audioSource = box.GetComponent<AudioSource>( );
            if ( box.IsFull ) {
                audioSource.mute = true;
            }
        }
    }
}

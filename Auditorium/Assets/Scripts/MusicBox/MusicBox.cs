using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicBox : MonoBehaviour {
    #region Private Members
    [Header("Volume Raise")]
    [Tooltip("Volume amount added for each colliding particles")]
    [SerializeField] private float _incrementPerParticle;

    [Header("Volume Decay")]

    [Range(0f, 4f)]
    [SerializeField] private float _speedVolumDecay;

    [Range(0f, 5f)]
    [SerializeField] private float _delayVolumDecay;

    [Header("Materials")]
    [SerializeField] private Material _volumOnMaterial;
    [SerializeField] private Material _volumOffMaterial;

    [Header("Renderers")]
    [SerializeField] private SpriteRenderer[ ] _spriteRenderers;


    [SerializeField] private ParticlesColor.ColorEnum _musicBoxColor;

    private float _timeBeginDecay;
    private AudioSource _audioSource;

    public bool IsFull {
        get {  return _audioSource.volume >= 1f; }
    }


    #endregion

    #region System

    private void Awake( ) {
        _audioSource = GetComponent<AudioSource>( );
        _audioSource.volume = 0f;
    }

    // Update is called once per frame
    void Update( ) {
        VolumeDecay( );
        UpdateBarsRenderers( );
    }
    private void OnTriggerEnter2D( Collider2D collision ) {
        ColorManager colorManager = collision.GetComponent<ColorManager>( );

        if (colorManager.CurrentColor == _musicBoxColor) {

            _audioSource.volume += Mathf.Clamp01(_incrementPerParticle);
            // _audioSource.volume = Mathf.Min(_audioSource.volume + _incrementPerParticle, 1f);

            _timeBeginDecay = Time.time + _delayVolumDecay;

        }

    }
    #endregion

    #region Main Methods
    void UpdateBarsRenderers( ) {
            int numBarsToTunOn = Mathf.FloorToInt(_spriteRenderers.Length * _audioSource.volume);

            for ( int i = 0 ; i < _spriteRenderers.Length ; i++ ) {
                _spriteRenderers[ i ].material = i < numBarsToTunOn ? _volumOnMaterial : _volumOffMaterial;
            }


    }

    void VolumeDecay( ) {
        if ( Time.time > _timeBeginDecay) {
            _audioSource.volume = Mathf.Clamp01(_audioSource.volume - _speedVolumDecay * Time.deltaTime);
        }

    }
    #endregion

}

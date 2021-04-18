using UnityEngine;

public class ParticlesGenerator : MonoBehaviour {
    #region Private Members

    [SerializeField] private Transform _particlePrefab;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;
    [SerializeField] private float _drag;
    [SerializeField] private ParticlesColor.ColorEnum _spawnColor;

    private Transform _transform;

    float _nextGenerationTime;
    #endregion

    // Start is called before the first frame update
    void Start( ) {
        _transform = GetComponent<Transform>( );
    }

    void Update( ) {
        if ( Time.time >= _nextGenerationTime ) {
            GenerateParticle( );
            _nextGenerationTime = Time.time + 1f / _spawnRate;
        }
    }

    void GenerateParticle( ) {
        Vector2 position = Random.insideUnitCircle * _radius + ( Vector2 ) _transform.position;

        // Rigidbody2D rigidbody2D = Instantiate(_particlePrefab, position, _transform.rotation, _container).GetComponent<Rigidbody2D>( );

        GameObject newParticle = ParticlesPooling.Instance.GetParticle( );

        if ( newParticle != null ) {

            newParticle.transform.position = position;
            newParticle.transform.rotation = _transform.rotation;
            newParticle.GetComponent<TrailRenderer>( ).Clear();

            newParticle.SetActive(true);

            Rigidbody2D rigidbody2D = newParticle.GetComponent<Rigidbody2D>( );
            rigidbody2D.velocity = _transform.right * _speed;
            rigidbody2D.drag = _drag;

            ColorManager colorManager = rigidbody2D.GetComponent<ColorManager>( );
            colorManager.ChangeParticleColor(_spawnColor);


            // Transform particle = Instantiate(_particlePrefab, _container);
            // particle.position = Random.insideUnitCircle * _radius + ( Vector2 ) _transform.position;
            // particle.rotation = _transform.rotation;

        } 
    }

    private void OnDrawGizmos( ) {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}

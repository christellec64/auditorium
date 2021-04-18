using UnityEngine;

public class ParticleDestruction : MonoBehaviour {
    [SerializeField] private Rigidbody2D _rigibbody2d;
    [SerializeField] private float _speed;

    // Update is called once per frame
    void Update( ) {
        if ( _rigibbody2d.velocity.magnitude <= _speed ) {
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}

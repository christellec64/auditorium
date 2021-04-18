using System.Collections.Generic;
using UnityEngine;

public class ParticlesPooling : MonoBehaviour
{

    [SerializeField] private int _particleCount;
    [SerializeField] private GameObject _particlePrefab;

    private List<GameObject> _particles;

    public static ParticlesPooling Instance;

    private void Awake( ) {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform parent = new GameObject( ).transform;
        parent.name = "Particles Container";

        _particles = new List<GameObject>( );

        for (int i = 0 ; i < _particleCount ; i++ ) {
          GameObject particle =  Instantiate(_particlePrefab, parent);
           particle.SetActive(false);
            _particles.Add(particle);
        }
    }

    public GameObject GetParticle() {
        foreach (GameObject particle in _particles) {
           if ( !particle.activeInHierarchy ) {
                return particle;
            }
        }

        return null;

    }
}

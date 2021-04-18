using UnityEngine;

public class EffectorColor : MonoBehaviour
{
    [SerializeField] ParticlesColor.ColorEnum _newColor;

    private void OnTriggerEnter2D( Collider2D collision ) {

        ColorManager colorManager = collision.GetComponent<ColorManager>( );
        colorManager.ChangeParticleColor(_newColor);

    }
}

using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public ParticlesColor.ColorEnum CurrentColor;
    private TrailRenderer _trailRenderer;

    private void Awake( ) {
        _trailRenderer = GetComponent<TrailRenderer>( );
    }

    public void ChangeParticleColor (ParticlesColor.ColorEnum color) {

        Color newColor = ParticlesColor.GetColor(color);
        _trailRenderer.material.color = newColor;
        CurrentColor = color;

    }
}

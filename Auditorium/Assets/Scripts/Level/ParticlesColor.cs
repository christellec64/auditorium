using UnityEngine;

public class ParticlesColor : MonoBehaviour
{
    public enum ColorEnum {
        Pink,
        Green,
        Yellow,
        Blue
    }

    public  static Color GetColor (ColorEnum color) { 
        switch (color) {
            case ( ColorEnum.Pink ):
            return new Color(251f, 0f, 255f);
            case ( ColorEnum.Green ):
            return new Color(0f, 162f, 0f);
            case ( ColorEnum.Yellow ):
            return new Color(221f, 245f, 0f);
            case ( ColorEnum.Blue ):
            return new Color(0f, 162f, 7f);
            default:
            return Color.black;
        }
    }
}

using UnityEngine;

[System.Serializable]
[CreateAssetMenuAttribute (fileName = "Lighting Preset", menuName = "Scriptables/Lighting")]
public class LightingPreset : ScriptableObject
{
    public Gradient ambientColor;
    public Gradient directionColor;
    public Gradient fontColor;
}

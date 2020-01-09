using UnityEngine;


[ExecuteAlways]
public class LightingManager : MonoBehaviour
{

    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;
    [SerializeField, Range(0, 100)] private float timeOfDay;



    private void Update()
    {
        if (preset == null)
            return;

        if(Application.isPlaying)
        {
            timeOfDay += Time.deltaTime;
            timeOfDay %= 100;
            UpdateLighting(timeOfDay / 100);
        }
        else
        {
            UpdateLighting(timeOfDay / 100);
        }
    }



    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fontColor.Evaluate(timePercent);
        
        if(directionalLight != null)
        {
            directionalLight.color = preset.directionColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360) -90, -170, 0));
        }
    }

    private void OnValidate()
    {
        if(directionalLight != null)
            return;

        if(RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }

        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}

using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //Referenser
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset preset;
    //Variabler
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    [SerializeField] private bool FrozenTime;

    private void Update()
    {
        if (FrozenTime)
        {
            return;
        }
        
        if (preset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 24; //Fäster vid 24
            UpdateLighting(TimeOfDay/24f);
        }
        else
        {
            UpdateLighting(TimeOfDay/24f);
        }
    }
    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f)-90f,-170f,0));
        }
    }
    private void OnValidate()
    {
        if (DirectionalLight != null)
        {
            return;
        }

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }

        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}

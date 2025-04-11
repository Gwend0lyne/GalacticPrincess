using UnityEngine;

public class SkyboxDisabler : MonoBehaviour
{
    public Color ambientColor = new Color(1f, 0.5f, 0f); // Orange
    public Transform playerTransform;

    void Start()
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = new Color(0.5f, 0.9f, 1f, 1f);
        AddWarmAmbientLight();
    }
    
    void AddWarmAmbientLight()
    {
        GameObject lightObj = new GameObject("Ambient Orange Light");
        Light light = lightObj.AddComponent<Light>();
        light.type = LightType.Directional;
        light.color = ambientColor;
        light.intensity = 0.3f;
        light.shadows = LightShadows.None;
        light.transform.position = new Vector3(-40f, 190f, 65f);
        light.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        if (playerTransform != null)
        {
            lightObj.transform.SetParent(playerTransform, false); // attache au joueur
        }
        else
        {
            Debug.LogWarning("PlayerTransform non assigné à SkyboxDisabler !");
        }
    }
}
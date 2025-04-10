using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform centerObject;         
    public float speed = 20f;             
    public Vector3 axis = Vector3.up;
    
    public bool isActive = true;

    void Update()
    {
        if (centerObject != null && isActive)
        {
            transform.RotateAround(centerObject.position, axis, speed * Time.deltaTime);
        }
    }

    public void SetOrbitActive(bool active)
    {
        isActive = active;
    }
}

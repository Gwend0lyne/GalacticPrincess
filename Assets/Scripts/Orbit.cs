using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform centerObject;         
    public float speed = 20f;             
    public Vector3 axis = Vector3.up;     

    void Update()
    {
        if (centerObject != null)
        {
            transform.RotateAround(centerObject.position, axis, speed * Time.deltaTime);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;  // La voiture
    public Transform planet;  // Le centre de la planète
    public float distance = 6f; // Distance derrière la voiture
    public float height = 3f; // Hauteur par rapport à la voiture
    public float tiltAngle = 20f; // Inclinaison de la caméra (pour voir plus vers l'avant)
    public float smoothSpeed = 5f; 
    void LateUpdate()
    {
        // Direction de la gravité (du joueur vers la planète)
        Vector3 gravityDirection = (player.position - planet.position).normalized;

        // Calculer la position cible de la caméra (derrière + hauteur)
        Vector3 targetPosition = player.position + (-player.forward * distance) + (gravityDirection * height);

        // Déplacer la caméra en douceur vers la position cible
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Ajuster l'orientation pour incliner la caméra vers l'avant
        Quaternion lookRotation = Quaternion.LookRotation(player.forward + gravityDirection * tiltAngle, gravityDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, smoothSpeed * Time.deltaTime);
    }
}

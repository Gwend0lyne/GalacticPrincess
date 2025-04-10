using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToPlanet2 : MonoBehaviour
{
    // Le nom de la scène à charger (assure-toi qu'elle est dans le Build Settings)
    public string sceneName = "Planet2";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered! Loading scene: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }
}
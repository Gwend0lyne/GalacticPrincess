using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStartManager : MonoBehaviour
{
    public CarMovement playerCar;
    public List<AIControls> aiCars;

    void Start()
    {
        StartCoroutine(StartRaceCountdown());
    }

    IEnumerator StartRaceCountdown()
    {
        Debug.Log("3");
        yield return new WaitForSeconds(1f);
        Debug.Log("2");
        yield return new WaitForSeconds(1f);
        Debug.Log("1");
        yield return new WaitForSeconds(1f);

        // Lancer les voitures
        playerCar.canMove = true;
        foreach (var ai in aiCars)
        {
            ai.canMove = true;
        }

        Debug.Log("GO!");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LapManager : MonoBehaviour
{
    public List<Checkpoint> checkpoints;
    public UIManager uIManager;
    public int totalLaps = 3;
    private int lastPlayerCheckpoint = -1;
    private int currentPlayerLap = 0;

    public List<AITracker> aiTrackers; 

    public string nextSceneName = "NextScene"; 
    public float delayBeforeSceneChange = 3f;

    void Start()
    {
        ListenCheckpoints(true);
        uIManager.UpdateLapText("");
    }

    private void ListenCheckpoints(bool subscribe)
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (subscribe) checkpoint.onCheckpointEnter.AddListener(CheckpointActivated);
            else checkpoint.onCheckpointEnter.RemoveListener(CheckpointActivated);
        }
    }

    public void CheckpointActivated(GameObject car, Checkpoint checkpoint)
    {
        if (!checkpoints.Contains(checkpoint)) return;

        int checkpointNumber = checkpoints.IndexOf(checkpoint);

        bool isPlayer = car.CompareTag("Player");

        if (isPlayer)
        {
            bool startingFirstLap = checkpointNumber == 0 && lastPlayerCheckpoint == -1;
            bool lapIsFinished = checkpointNumber == 0 && lastPlayerCheckpoint >= checkpoints.Count - 1;

            if (startingFirstLap || lapIsFinished)
            {
                currentPlayerLap += 1;
                lastPlayerCheckpoint = 0;

                if (currentPlayerLap > totalLaps)
                {
                    bool playerWon = true;

                    foreach (var ai in aiTrackers)
                    {
                        if (ai.currentLap > totalLaps)
                        {
                            playerWon = false;
                            break;
                        }
                    }

                    if (playerWon)
                    {
                        uIManager.UpdateLapText("YOU WON");
                        StartCoroutine(LoadSceneAfterDelay(nextSceneName));
                    }
                    else
                    {
                        uIManager.UpdateLapText("TRY AGAIN");
                        StartCoroutine(ReloadCurrentSceneAfterDelay());
                    }
                }
                else
                {
                    if (currentPlayerLap > 0)
                    {
                        uIManager.UpdateLapText("LAP " + currentPlayerLap + "/" + totalLaps);
                    }
                }
            }
            else if (checkpointNumber == lastPlayerCheckpoint + 1)
            {
                lastPlayerCheckpoint += 1;
            }
        }
        else
        {
            // IA checkpoint tracking
            Debug.Log("JE SUIS L IA");
            var aiTracker = car.GetComponent<AITracker>();
            if (aiTracker == null) return;

            bool startingFirstLap = checkpointNumber == 0 && aiTracker.lastCheckpoint == -1;
            bool lapIsFinished = checkpointNumber == 0 && aiTracker.lastCheckpoint >= checkpoints.Count - 1;

            if (startingFirstLap || lapIsFinished)
            {
                aiTracker.currentLap += 1;
                aiTracker.lastCheckpoint = 0;
            }
            else if (checkpointNumber == aiTracker.lastCheckpoint + 1)
            {
                aiTracker.lastCheckpoint += 1;
            }
        }
    }

    IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeSceneChange);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator ReloadCurrentSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeSceneChange);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

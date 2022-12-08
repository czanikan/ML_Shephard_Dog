using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class GameManager : MonoBehaviour
{
    public GameObject dogAgent;
    public GameObject sheep;

    private Vector3 sheepStartPos;

    public int MaxEnvironmentSteps = 2500;
    private int resetTimer = 0;

    private void Start()
    {
        sheepStartPos = sheep.transform.position;
        ResetEpisode();
    }
    private void ResetEpisode()
    {
        dogAgent.transform.position = dogAgent.GetComponent<DogAgentController>().startPos;
        Vector3 sheepNewPos = sheepStartPos + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
        sheep.GetComponent<CharacterController>().enabled = false;
        sheep.transform.position = sheepNewPos;
        Debug.Log(sheepNewPos);
        dogAgent.GetComponent<Rigidbody>().velocity = Vector3.zero;

        sheep.GetComponent<CharacterController>().enabled = true;
        // erre az�rt volt sz�ks�g, mert egyes esetekben a birka mozg�sa fel�l�rta a kezd� poz�ci�ra 
        // val� helyez�st, �gy technikailag ott maradt, ahol volt (a kar�mban).

        resetTimer = 0;
    }

    void FixedUpdate()
    {
        resetTimer += 1;
        if (resetTimer >= MaxEnvironmentSteps && MaxEnvironmentSteps > 0)
        {
            dogAgent.GetComponent<DogAgentController>().EpisodeInterrupted();
            ResetEpisode();
        }
    }

    public void RoundOver(bool isWin)
    {
        if(isWin)
        {
            dogAgent.GetComponent<DogAgentController>().AddReward(1 - (float)resetTimer / MaxEnvironmentSteps);
        }

        dogAgent.GetComponent<DogAgentController>().EndEpisode();

        ResetEpisode();
    }
}

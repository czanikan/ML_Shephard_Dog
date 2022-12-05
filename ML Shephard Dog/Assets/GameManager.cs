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
        sheep.transform.position = sheepStartPos + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));

        dogAgent.GetComponent<Rigidbody>().velocity = Vector3.zero;

        resetTimer = 0;
    }

    void FixedUpdate()
    {
        resetTimer += 1;
        if (resetTimer >= MaxEnvironmentSteps && MaxEnvironmentSteps > 0)
        {
            dogAgent.GetComponent<Agent>().EpisodeInterrupted();
            ResetEpisode();
        }
    }

    public void RoundOver()
    {
        dogAgent.GetComponent<Agent>().AddReward(1 - (float)resetTimer / MaxEnvironmentSteps);
        dogAgent.GetComponent<Agent>().EndEpisode();

        ResetEpisode();
    }
}

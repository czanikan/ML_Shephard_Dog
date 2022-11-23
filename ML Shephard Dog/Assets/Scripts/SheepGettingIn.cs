using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepGettingIn : MonoBehaviour
{
    public int points = 0;
    private GameManagement gameManagerScript;

    private void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagement>();
        points = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!gameManagerScript.gameOver)
        {
            if (other.gameObject.tag == "Sheep")
            {
                points++;
                //Debug.Log("Sheep got in! You've earned " + points + " points!");
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (!gameManagerScript.gameOver)
        {
            if (other.gameObject.tag == "Sheep")
            {
                points--;
                Debug.Log("You lost a sheep! You have " + points + " points now!");
            }
        }
        
    }
}


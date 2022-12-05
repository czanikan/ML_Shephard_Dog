using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWinManager : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sheep")
        {
            gameManager.RoundOver();
        }
    }
}

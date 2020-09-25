using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if(gameController.currentMultiplier == 1)
        {

        }
        if (gameController.currentMultiplier == 2)
        {

        }
        if (gameController.currentMultiplier == 3)
        {

        }
        if (gameController.currentMultiplier > 3)
        {

        }
    }
}

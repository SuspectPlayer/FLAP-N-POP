using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI txt;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bubble")
        {
            txt.gameObject.SetActive(true);
            txt.SetText("GAME OVER");

        }
    }
}

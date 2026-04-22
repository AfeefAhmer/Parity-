using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player1;
    public PlayerController2 player2;

    public Text resultText;

    void Start()
    {
        resultText.text = "";
    }

    void Update()
    {
        if (player1.GetAmmo() <= 0 && player2.GetAmmo() <= 0)
        {
            resultText.text = "DRAW";
            Time.timeScale = 0f;
        }
    }
}
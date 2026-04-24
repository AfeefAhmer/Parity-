using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerController player1;
    public PlayerController2 player2;

    public TextMeshProUGUI resultText;

    private bool gameEnded = false;

    void Start()
    {
        resultText.text = "";
    }

    void Update()
    {
        if (gameEnded) return;

        //  Voitto
        if (player1 == null || player1.IsDead())
        {
            resultText.text = "RED WINS!";
            EndGame();
        }
        else if (player2 == null || player2.IsDead())
        {
            resultText.text = "BLUE WINS!";
            EndGame();
        }
        //  Draw
        else if (player1.GetAmmo() <= 0 && player2.GetAmmo() <= 0)
        {
            resultText.text = "DRAW!";
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        Time.timeScale = 0f;
    }
}
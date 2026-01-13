using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI buttonText;
    

    private GameController gameController;

    public void SetSpace()
    {
        if (gameController.playerMove == true)
        {
            buttonText.text = gameController.GetPlayerSide();
            button.interactable = false;
            gameController.EndTurn();
            gameController.playerMove = false; //khong cho player di khi vua di
        }
    }

    public void SetGameControllerReference(GameController Controller)
    {
        gameController = Controller;
    }





}

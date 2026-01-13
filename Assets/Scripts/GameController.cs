using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI[] buttonList;
    public GameObject gameOverPanel;
    public GameObject restartGameButton;
    public TextMeshProUGUI gameOverText;
    private int moveCount ;

    public Player playerX;
    public Player playerO;

    public PlayerColor activatePanel;
    public PlayerColor inActivatePanel;

    bool gameEnded = false;
    private string playerSide;

    private string computerSide;
    public bool playerMove;
    public float delay;
    public int value;

    public GameObject startingInfor;

    private void Awake()
    {
        SetGameControllerRefOnButtons();
        moveCount = 0;
        restartGameButton.SetActive(false);
        playerMove = true;
    }

    private void Update()
    {
        if (playerMove == false)
        {
            delay += delay * Time.deltaTime;
            if(delay >= 100)
            {
                value = Random.Range(0, 8);
                if (buttonList[value].GetComponentInParent<Button>().interactable == true)
                {
                    buttonList[value].text=GetComputerSide();
                    buttonList[value].GetComponentInParent<Button>().interactable = false;
                    playerMove = true;
                    EndTurn();
                }
            }
        }
    }

    private int[][] winPattern = new int[][]
    {
         new int[] {0, 1, 2},
         new int[] {3, 4, 5},
         new int[] {6, 7, 8},
         new int[] {0, 3, 6},
         new int[] {1, 4, 7},
         new int[] {2, 5, 8},
         new int[] {0, 4, 8},
         new int[] {2, 4, 6}
    };

    private void SetGameControllerRefOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public string GetComputerSide()
    {
        return computerSide;
    }

    public void EndTurn()
    {
        moveCount++;
        
        foreach (var index in winPattern)
        {
            if (buttonList[index[0]].text == playerSide &&
                buttonList[index[1]].text == playerSide &&
                buttonList[index[2]].text == playerSide && playerSide != ""
                )
            {
                GameOver("win");
                gameEnded = true;
                break;
            }
            else if(buttonList[index[0]].text == computerSide &&
                    buttonList[index[1]].text == computerSide &&
                    buttonList[index[2]].text == computerSide && computerSide != "")
            {
                GameOver("lost");
                gameEnded = true;
                break;
            }
        }

        if (gameEnded) return;
        
        ChangePlayerSide();
        delay = 5;
        

        if (moveCount >= 9)
        {
            GameOver("draw");
        }
    }


    void GameOver(string playerWin)
    {
        SetBoardInteracable(false);
        if (playerWin == "draw")
        {
            SetGameOverText("It's a draw !");
            SetPlayerColorInactivate();
        }
        else 
        {
            SetGameOverText(playerSide + "  WIN!");
        }
        restartGameButton.SetActive(true);
    }

    public void ChangePlayerSide()
    {
    //    playerSide = (playerSide == "X") ? "O" : "X";
    //    if (playerSide == "X")
        if(playerMove== true)
        {
            SetPlayerColor(playerX, playerO);
        }
        else
        {
            SetPlayerColor(playerO, playerX);
        }
        

    }

    void StartGame()
    {
        SetBoardInteracable(true);
        SetPlayerButtons(false);
        startingInfor.SetActive(false);
    }

    public void SetStartingPlayerSide(string startingSide)
    {
        playerSide = startingSide;
        if (playerSide == "X")
        {
            computerSide = "O";
            SetPlayerColor(playerX, playerO);
        }
        else
        {
            computerSide = "X";
            SetPlayerColor(playerO, playerX);
        }
        StartGame();
    }

    void SetGameOverText(string _text) 
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = _text;
        restartGameButton.SetActive(true);
    } 

    public void RestartGame()
    {
        
        gameOverPanel.SetActive(false);
        moveCount = 0;
        SetPlayerButtons(true);
        SetPlayerColorInactivate();
        startingInfor.SetActive(true);
        playerMove = true;
        delay = 5;
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
        restartGameButton.SetActive(false);
        gameEnded = false;
        
    }

    public void SetBoardInteracable(bool value)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = value;
        }
    }

    public void SetPlayerColor(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activatePanel.panelColor;
        newPlayer.Text.color = activatePanel.textColor;
        oldPlayer.panel.color = inActivatePanel.panelColor;
        oldPlayer.Text.color = inActivatePanel.textColor;
    }

    public void SetPlayerButtons(bool value)
    {
        playerX.button.interactable = value;
        playerO.button.interactable = value;
    }

    void SetPlayerColorInactivate()
    {
        playerX.panel.color = inActivatePanel.panelColor;
        playerX.Text.color = inActivatePanel.textColor;
        playerO.panel.color = inActivatePanel.panelColor;
        playerO.Text.color = inActivatePanel.textColor;
    }
}

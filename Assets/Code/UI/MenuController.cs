using Assets.Code.Controllers;
using Assets.Code.Seralization;
using Assets.Code.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject GameTemplate;
    public GameObject Buttons;
    public GameObject Results;
    public GameObject Records;
    public GameObject GameUI;

    private GameController Game;


    public void NewGame()
    {
        if (this.Game)
            GameObject.Destroy(this.Game);

        this.Game = this.GameTemplate.Create<GameController>(Vector2.zero, null);
        this.Game.MainMenu = this;
        this.ShowGameUI();
    }

    public void ShowGameUI()
    {
        this.Buttons.SetActive(false);
        this.Results.SetActive(false);
        this.Records.SetActive(false);
        this.GameUI.SetActive(true);
    }

    public void ShowResults(int coins, float time, GameEndReason reason)
    {
        this.Buttons.SetActive(false);
        this.Results.SetActive(true);
        this.Records.SetActive(false);
        this.GameUI.SetActive(false);

        var resutls = this.Results.GetComponent<ResultsController>();
        resutls.SetCoins(coins);
        resutls.SetTime(time);
        resutls.SetFinishReason(reason);
        resutls.MainMenu = this;
    }

    public void ShowRecords()
    {
        this.Buttons.SetActive(false);
        this.Results.SetActive(false);
        this.Records.SetActive(true);
        this.GameUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

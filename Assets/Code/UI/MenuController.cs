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
    public GameObject ChangeName;
    public GameObject Records;

    private GameController Game;

    private void Start()
    {
        this.ShowMainMenu();
    }

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
        this.ChangeName.SetActive(false);
        this.Records.SetActive(false);
    }

    public void ShowChangeName()
    {
        this.Buttons.SetActive(false);
        this.ChangeName.SetActive(true);
        this.Records.SetActive(false);

        var resutls = this.ChangeName.GetComponent<ChangeNameController>();
        resutls.MainMenu = this;

        resutls.UpdateValues();
    }

    public void ShowRecords()
    {
        this.Buttons.SetActive(false);
        this.ChangeName.SetActive(false);
        this.Records.SetActive(true);

        using (var repository = new UserRepository())
        {
            var recordsController = this.Records.GetComponent<RecordsController>();
            recordsController.SetUsers(repository.Records.ToArray());
            recordsController.Menu = this;
        }
    }

    public void ShowMainMenu()
    {
        this.Buttons.SetActive(true);
        this.ChangeName.SetActive(false);
        this.Records.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

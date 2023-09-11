﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuManager : BaseMonoBehavior
{
    private ApplicationManager _applicationManager;
    [SerializeField]
    private Button _startButton1;
    [SerializeField]
    private Button _startButton2;
    [SerializeField]
    private Button _startButton3;
    [SerializeField]
    private Button _settingsButton;
    private void  Start()
    {
        _applicationManager = SafeFindObjectOfType<ApplicationManager>();
        SafeAssigned(_startButton1);
        SafeAssigned(_startButton2);
        SafeAssigned(_startButton3);
        SafeAssigned(_settingsButton);

        _startButton1.onClick.AddListener(() => { OnStartButton(1); });
        _startButton2.onClick.AddListener(() => { OnStartButton(2); });
        _startButton3.onClick.AddListener(() => { OnStartButton(3); });

    }

    public void OnStartButton(int saveSlot)
    {
        _applicationManager.SaveSlot = saveSlot;
        _applicationManager.StartGame();
    }
    public void OnSettingsButton()
    {
        _applicationManager.OpenSettings();
    }
}


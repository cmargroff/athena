using System;
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
    private Button _startButton;
    [SerializeField]
    private Button _settingsButton;
    private void  Start()
    {
        _applicationManager = SafeFindObjectOfType<ApplicationManager>();
        SafeAssigned(_startButton);
        SafeAssigned(_settingsButton);
        _startButton.onClick.AddListener( () =>
        {
            Debug.Log("Starting Game");
            _applicationManager.StartGame();
        });

        _startButton.onClick.AddListener(() =>
        {
            Debug.Log("Not yet implemented");
        });
    }
}


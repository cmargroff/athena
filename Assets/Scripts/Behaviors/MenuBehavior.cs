using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
[RequireComponent(typeof(UIDocument))]
public class MenuBehavior : MonoBehaviour
{

    UIDocument _uiDocument;
    private @PlayerInputActions _controls;

    // Start is called before the first frame update
    void Start()
    {
        _controls = new @PlayerInputActions();
        _controls.Menues.Enable();
        _uiDocument =GetComponent<UIDocument>();
        var button=_uiDocument.rootVisualElement.Q<Button>("StartButton");
        button.RegisterCallback<ClickEvent>(OnStartButtonClick);
        button.RegisterCallback<NavigationSubmitEvent>(OnStartButtonSubmit);
    }

    private void OnStartButtonSubmit(NavigationSubmitEvent evt)
    {
        StartGame();
    }

    private void OnStartButtonClick(ClickEvent evt)
    {
        StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

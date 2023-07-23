using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        button.clicked += () => OnStartLabelClick();
        button.RegisterCallback<NavigationSubmitEvent>(OnStartButtonClick);
    }

    private void OnStartButtonClick(NavigationSubmitEvent evt)
    {
        Debug.Log("Button clicked");
    }

    private void OnStartLabelClick()
    {
        Debug.Log("Label clicked");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

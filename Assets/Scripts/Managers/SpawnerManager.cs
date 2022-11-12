using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    UserInput inputAction;

    public Canvas spawnerUI;

    void Start()
    {
        inputAction = InputManager.controller.inputAction;

        inputAction.Player.EnableUI.performed += cntxt => SwapUI();
    }

    private void SwapUI()
    {
        spawnerUI.enabled = !spawnerUI.enabled;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour
{
    public GameObject UI_canvas;
    GraphicRaycaster UI_raycaster;

    PointerEventData click_data;
    List<RaycastResult> click_results;

    void Start()
    {
        UI_raycaster = UI_canvas.GetComponent<GraphicRaycaster>();
        click_data = new PointerEventData(EventSystem.current);
        click_results = new List<RaycastResult>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            GetUiElementsClicked();
        }
    }

    void GetUiElementsClicked()
    {
        click_data.position = Mouse.current.position.ReadValue();
        click_results.Clear();

        UI_raycaster.Raycast(click_data, click_results);

        //debug console
        /*
        foreach(RaycastResult result in click_results)
        {
            GameObject UI_element = result.gameObject;

            Debug.Log(UI_element.name);
        }
        */
    }
}

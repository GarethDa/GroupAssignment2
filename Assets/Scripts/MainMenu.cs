using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Serializable]
    private struct InputInfoStruct
    {
        public InputActionReference actionReference;
        public GameObject actionButton;
    }

    [Header("Scene/Screen switching")]
    [SerializeField] private string lobbyLevel;
    [SerializeField] private GameObject settingsScreen;

    [Header("Control rebinding")]
    [SerializeField] private List<InputInfoStruct> inputInfoList;
    
    //[SerializeField] private PlayerInput pInput;
    private List<TMP_Text> bindingTexts = new List<TMP_Text>();
    private List<GameObject> rebindTextObjects = new List<GameObject>();
    private List<GameObject> waitingTextObjects = new List<GameObject>();

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inputInfoList.Count; i++)
        {
            bindingTexts.Add(inputInfoList[i].actionButton.transform.Find("ControlText").GetComponent<TMP_Text>());
            rebindTextObjects.Add(inputInfoList[i].actionButton.transform.Find("ControlText").gameObject);
            waitingTextObjects.Add(inputInfoList[i].actionButton.transform.Find("InputText").gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(lobbyLevel);
    }

    public void OpenOptions()
    {
        settingsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        settingsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void StartRebinding(int index)
    {
        //Hide the control text and show the waiting for input text

        rebindTextObjects[index].SetActive(false);
        waitingTextObjects[index].SetActive(true);

        rebindingOperation = inputInfoList[index].actionReference.action.PerformInteractiveRebinding()
            .OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindComplete(index)).Start();
    }

    private void RebindComplete(int index)
    {
        int bindingIndex = inputInfoList[index].actionReference.action.GetBindingIndexForControl(inputInfoList[index].actionReference.action.controls[0]);

        bindingTexts[index].text = InputControlPath.ToHumanReadableString(inputInfoList[index].actionReference.action.bindings[bindingIndex].effectivePath, 
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingOperation.Dispose();

        rebindTextObjects[index].SetActive(true);
        waitingTextObjects[index].SetActive(false);


    }

    //private void RebindControl(string controlPath, input)
}

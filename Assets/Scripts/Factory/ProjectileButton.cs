using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectileButton : MonoBehaviour
{
    private ProjectileFactory factory;

    TextMeshProUGUI btnText;

    // Start is called before the first frame update
    void Start()
    {
        factory = GameObject.Find("GameManager").GetComponent<ProjectileFactory>();

        btnText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnClickSpawn()
    {
        switch (btnText.text)
        {
            case "dodgeball":
                factory.GetProjectile("dodgeball").Create(factory.projectilePrefab1).transform.position = 
                    GameObject.FindGameObjectsWithTag("Player")[0].transform.position + GameObject.FindGameObjectsWithTag("Player")[0].transform.forward * 5;
                break;

            case "pachinko":
                factory.GetProjectile("pachinko").Create(factory.projectilePrefab2).transform.position =
                    GameObject.FindGameObjectsWithTag("Player")[0].transform.position + GameObject.FindGameObjectsWithTag("Player")[0].transform.forward * 5;
                break;

            default:
                break;
        }
    }
}

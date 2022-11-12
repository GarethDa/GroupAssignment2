using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
   

    private void Start()
    {
        SpawnMyPlayer();
    }

    void SpawnMyPlayer()
    {
        Vector3 randomPosition = transform.position;
        GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        myPlayer.transform.SetParent(GameManager.instance.playerManager.transform);//please dont remove this
        //ENABLED SO THAT EACH CLIENT HAS THEIR OWN VERSION
        myPlayer.GetComponent<TpMovement>().enabled = true;
        myPlayer.GetComponent<PlayerInput>().enabled = true;
        myPlayer.GetComponent<CharacterAiming>().enabled = true;
        myPlayer.GetComponent<CameraSettings>().enabled = true;
        myPlayer.transform.Find("Camera Holder").gameObject.SetActive(true);
        myPlayer.transform.Find("Zoomed Camera").gameObject.SetActive(true);
        myPlayer.transform.Find("Virtual Camera").gameObject.SetActive(true);
    }
}

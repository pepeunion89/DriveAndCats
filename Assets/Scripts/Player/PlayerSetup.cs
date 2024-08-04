using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour {

    public Player player;
    public GameObject playerCamera;

    public void IsLocalPlayer() {

        player.enabled = true;
        playerCamera.SetActive(true);

    }

}

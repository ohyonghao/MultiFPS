using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class NetworkManager : MonoBehaviour {

    [SerializeField]
    bool OfflineMode = false;
    public Camera standbyCamera;

	// Use this for initialization
	void Start () {
        Connect();
	}
	
	// Update is called once per frame
	void Connect () {
        PhotonNetwork.offlineMode = OfflineMode;
        PhotonNetwork.ConnectUsingSettings("MultiFPS 0.0.1");
	}

    void OnGUI() {
        if(PhotonNetwork.inRoom)
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString() + " - " + (PhotonNetwork.inRoom? PhotonNetwork.room.name : "No Room"));
    }

    void OnJoinedLobby() {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed() {
        PhotonNetwork.CreateRoom("Room 1");
    }

    void OnJoinedRoom() {
        Debug.Log("Room Joined");

        SpawnMyPlayer();
    }

    void SpawnMyPlayer() {
        GameObject myPlayer = PhotonNetwork.Instantiate("FPSController", RandomSpawnVector3(), Quaternion.identity, 0);
        FirstPersonController myPlayerController = myPlayer.GetComponent<FirstPersonController>();
        myPlayerController.enabled = true;
        myPlayerController.GetComponentInChildren<Camera>().enabled = true;
        myPlayerController.GetComponentInChildren<AudioListener>().enabled = true;

        standbyCamera.enabled = false;
        standbyCamera.GetComponent<AudioListener>().enabled = false;
    }

    Vector3 RandomSpawnVector3() {
        GameObject[] respawn = GameObject.FindGameObjectsWithTag("Respawn");
        return respawn[Random.Range(0,respawn.Length)].transform.position;
    }
}

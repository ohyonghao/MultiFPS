using UnityEngine;
using System.Collections;

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
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby() {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed() {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom() {
        Debug.Log("Room Joined");

        SpawnMyPlayer();
    }

    void SpawnMyPlayer() {
        PhotonNetwork.Instantiate("FPSController", RandomSpawnVector3(), Quaternion.identity, 0);
        standbyCamera.enabled = false;
        standbyCamera.GetComponent<AudioListener>().enabled = false;
    }

    Vector3 RandomSpawnVector3() {
        GameObject[] respawn = GameObject.FindGameObjectsWithTag("Respawn");
        return respawn[Random.Range(0,respawn.Length)].transform.position;
    }
}

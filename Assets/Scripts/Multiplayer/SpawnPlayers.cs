using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject Player;
    public float minX, minZ, maxX, maxZ;

    private void Start()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        Vector3 randomPosition = new Vector3(x, 0.96f, z);
        GameObject player = PhotonNetwork.Instantiate(Player.name, randomPosition, Quaternion.identity);
        
        var a = FindObjectOfType<Cinemachine.CinemachineFreeLook>();
        a.Follow = player.transform;
        a.LookAt = player.transform;
    }
}

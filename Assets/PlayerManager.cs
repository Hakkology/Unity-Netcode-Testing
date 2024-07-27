using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public GameObject playerPrefab;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsServer)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        Vector3 olusmaNoktasý = new Vector3(
            UnityEngine.Random.Range(-5f, 5f),
            0,
            UnityEngine.Random.Range(-5f, 5f));

        GameObject player = Instantiate(playerPrefab, olusmaNoktasý, Quaternion.identity);
        NetworkObject playerNetObject = player.GetComponent<NetworkObject>();
        playerNetObject.SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);
    }
}

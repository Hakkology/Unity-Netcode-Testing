using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour
{
    public Button hostButton;
    public Button clientButton;

    public TextMeshProUGUI connectionStatus;

    private void Start()
    {
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            connectionStatus.text = "Hosting...";
        });

        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            connectionStatus.text = "Connecting as Client...";
        });

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;


        void OnClientConnected(ulong clientId)
        {
            connectionStatus.text = $"Client {clientId} connected";
        }

        void OnClientDisconnected(ulong clientId)
        {
            connectionStatus.text = $"Client {clientId} disconnected";
        }
    }


}

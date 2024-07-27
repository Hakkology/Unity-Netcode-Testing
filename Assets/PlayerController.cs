using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5;
    private NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        if (IsOwner)
        {
            transform.position = networkPosition.Value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");

            float yatayHareket = yatay * moveSpeed * Time.deltaTime;
            float dikeyHareket = dikey * moveSpeed * Time.deltaTime;

            transform.Translate(new Vector3(yatayHareket, 0, dikeyHareket));
            UpdateServerPositionServerRpc(transform.position);
        }
    }

    [ServerRpc]
    private void UpdateServerPositionServerRpc(Vector3 newPosition)
    {
        networkPosition.Value = newPosition;
    }

    public override void OnNetworkSpawn()
    {
        networkPosition.OnValueChanged += OnPositionChanged;
    }

    public override void OnNetworkDespawn()
    {
        networkPosition.OnValueChanged -= OnPositionChanged;
    }

    private void OnPositionChanged(Vector3 oldPosition, Vector3 newPosition)
    {
        if (!IsOwner)
        {
            transform.position = newPosition;
        }
    }
}

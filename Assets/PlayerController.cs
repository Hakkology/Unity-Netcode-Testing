using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        if (IsOwner)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
    }
}

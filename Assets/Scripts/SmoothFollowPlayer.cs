using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowPlayer : MonoBehaviour
{
    public float damping = 4.0f;
    public Transform player;

    float zPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        zPosition = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
            {
                player = playerGO.transform;
            }
        } else {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, zPosition);
            transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
        }
    }
}

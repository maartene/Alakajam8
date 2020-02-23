using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BoatText : MonoBehaviour
{
    Player player;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null) {
                player = playerGO.GetComponent<Player>();
            }
        } else
        {
            int playerDist = Mathf.Abs(player.x) + player.y;
            text.text = playerDist + "m ("+player.maxDistanceFromBoat+"m max)";
        }
    }
}

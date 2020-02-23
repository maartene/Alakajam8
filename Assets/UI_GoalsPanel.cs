using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GoalsPanel : MonoBehaviour
{

    public Toggle reachBottomGoal;
    public Toggle maxDistanceGoal;
    public Toggle meetFishGoal;
    public Toggle avoidEelGoal;

    GameManager gameManager;
    Player player;

    void CheckGoals()
    {
        if (player == null)
        {
            return;
        }

        // did player reach bottom?
        if (player.y == gameManager.depth - 1)
        {
            reachBottomGoal.isOn = true;
        }

        // did player reach maxDistance?
        if (Mathf.Abs(player.x) + player.y >= player.maxDistanceFromBoat)
        {
            maxDistanceGoal.isOn = true;
        }

        // did player meet fish?
        foreach (Fish fish in gameManager.fishies)
        { 
            if (fish.x == player.x && fish.y == player.y)
            {
                meetFishGoal.isOn = true;
            }
        }

        // did player meet an eel?
        foreach (Fish eel in gameManager.eels)
        {
            if (eel.x == player.x && eel.y == player.y)
            {
                avoidEelGoal.isOn = false;
            }
        }
    }

    private void ResetGoals()
    {
        reachBottomGoal.isOn = false;
        maxDistanceGoal.isOn = false;
        meetFishGoal.isOn = false;
        avoidEelGoal.isOn = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onStepDone += CheckGoals;        
    }

    private void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
            ResetGoals();
        }
    }
}

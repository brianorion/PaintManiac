using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float minimumDepthTillDeath = 100f;
    [SerializeField]
    private float timeTillDeath = 3f;

    public static bool changedLevel = false;
    public static bool canMove = true;

    public static int score = 0;
    public static int hitPoints = 3;

    public static Vector3 spawnLocation = Vector3.zero;

    private GameObject instantiatedPlayer;

    private void Start()
    {
        // the player will always spawn at the origin of the world
        instantiatedPlayer = Instantiate(player, spawnLocation, Quaternion.identity);
    }
    private void Update()
    {
        if (changedLevel)
            ResetVariables();
        PlayerUpdate();
        GameOver();
    }

    private void PlayerUpdate()
    {
        // if the player has not been destroyed in any way, shape, or form
        if (instantiatedPlayer != null)
        {
            float playerDepth = instantiatedPlayer.transform.position.y;
            // if the player is below the depth limit
            if (playerDepth <= -minimumDepthTillDeath)
            {
                Destroy(instantiatedPlayer, timeTillDeath);
            }
        }
        // if it has been destroyed
        else
        {
            hitPoints -= 1;
            canMove = true;
            instantiatedPlayer = Instantiate(player, spawnLocation, Quaternion.identity);
        }
    }

    private void ResetVariables()
    {
        // these are variables that resets when changing levels
        changedLevel = false;
        spawnLocation = Vector3.zero;
        hitPoints = 3;
        canMove = true;
    }

    private void GameOver()
    {
        if (hitPoints <= 0)
        {
            // when it is game over:
            // the player cannot move
            canMove = false;
            // the mesh renderer should be disabled
            instantiatedPlayer.GetComponent<MeshRenderer>().enabled = false;
            // there should be an UI that prompts the player to either restart or go to main menu

            Debug.Log("Game Over");
        }
    }
}

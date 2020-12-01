using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            // we are going to switch to the next level
            GameManager.changeLevel = true;
            Debug.Log($"Finished Level: {GameManager.changeLevel}");
        }
    }
}

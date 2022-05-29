using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Code : MonoBehaviour
{
    public GameObject player;
    private PlayerMovementController playerMovementController;

    public int numCodeBlocks = 5;
    public bool forward = true;
    public bool turnLeft = true;
    public bool turnRight = true;

    List<string> code = new List<string>();

    private void Start()
    {
        playerMovementController = player.GetComponent<PlayerMovementController>();

        for (int i = 0; i < numCodeBlocks; i++)
        {

        }
    }

    public void addCommand(string command)
    {
        code.Add(command);
    }

    public void run()
    {
        StartCoroutine(runCoRoutine());
    }

    IEnumerator runCoRoutine()
    {
        for (int i = 0; i < code.Count; i++)
        {
            switch (code[i])
            {
                case "Move Forward":
                    playerMovementController.moveForward();
                    break;
                case "Turn Right":
                    playerMovementController.turnRight();
                    break;
                case "Turn Left":
                    playerMovementController.turnLeft();
                    break;
            }

            yield return new WaitForSeconds(0.5f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

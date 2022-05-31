using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Code : MonoBehaviour
{
    public GameObject player;
    public GameObject commandSquares;
    public GameObject panel;
    private PlayerMovementController playerMovementController;
    public GameObject emptySquare, forwardSquare, leftSquare, rightSquare, openSquare;

    public int numCodeBlocks = 5;
    public bool forward = true;
    public bool turnLeft = true;
    public bool turnRight = true;

    private int gap = 20;
    private int squaresPerRow = 9;
    private int rows;
    private float stepDelay = 0.25f;

    public static List<string> code = new List<string>();
    List<GameObject> squares = new List<GameObject>();

    private void Start()
    {
        playerMovementController = player.GetComponent<PlayerMovementController>();

        rows = (int)Mathf.Ceil((float)numCodeBlocks / squaresPerRow);

        RectTransform panelTransform = panel.GetComponent<RectTransform>();
        panelTransform.sizeDelta = new Vector2(panelTransform.sizeDelta.x, panelTransform.sizeDelta.y + (rows - 1) * (emptySquare.GetComponent<RectTransform>().sizeDelta.x * 2 + gap));

        instantiateSquares();
    }

    private void instantiateSquares()
    {
        for (int i = 0; i < squares.Count; i++)
        {
            Destroy(squares[i]);
        }

        squares.Clear();

        for (int i = 0; i < numCodeBlocks; i++) {
            GameObject prefab = emptySquare;

            if (i < code.Count)
            {
                switch (code[i])
                {
                    case "Move Forward":
                        prefab = forwardSquare;
                        break;
                    case "Turn Right":
                        prefab = rightSquare;
                        break;
                    case "Turn Left":
                        prefab = leftSquare;
                        break;
                    case "Open":
                        prefab = openSquare;
                        break;
                }
            }

            GameObject square = Instantiate(prefab);
            square.transform.SetParent(commandSquares.transform, false);
            float squareX = (i % squaresPerRow) * (square.GetComponent<RectTransform>().sizeDelta.x * 2 + gap);
            float squareY = -(int)(i / squaresPerRow) * (square.GetComponent<RectTransform>().sizeDelta.y * 2 + gap);
            square.transform.localPosition = new Vector3(squareX, squareY, 0);
            squares.Add(square);
        }
    }

    public void addCommand(string command)
    {
        if (code.Count == numCodeBlocks) return;

        code.Add(command);

        instantiateSquares();
    }

    public void run()
    {
        StartCoroutine(runCoRoutine());
    }

    void setChildrenActive(bool active)
    {
        foreach (Button button in transform.GetComponentsInChildren<Button>())
        {
            button.interactable = active;
        }
    }

    IEnumerator runCoRoutine()
    {
        setChildrenActive(false);

        bool won = false;

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

            yield return new WaitForSeconds(stepDelay);

            if (playerMovementController.drankMilk)
            {
                won = true;
                code.Clear();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (!won) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void clear()
    {
        code.Clear();
        instantiateSquares();
    }

    public void delete()
    {
        if (code.Count == 0) return;
        code.RemoveAt(code.Count - 1);
        instantiateSquares();
    }
}

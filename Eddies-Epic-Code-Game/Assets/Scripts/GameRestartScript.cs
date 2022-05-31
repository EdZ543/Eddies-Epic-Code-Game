using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestartScript : MonoBehaviour
{
    public void restartTheDarnGame()
    {
        Code.code.Clear();
        SceneManager.LoadScene(0);
    }
}

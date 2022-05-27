using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Code : MonoBehaviour
{
    public int numCodeBlocks = 5;

    public bool forward = true;
    public bool turnLeft = true;
    public bool turnRight = true;

    List<string> code = new List<string>();
}

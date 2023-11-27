using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public static bool shouldStartingGame;

    private void Awake()
    {
        shouldStartingGame = false;
    }
}

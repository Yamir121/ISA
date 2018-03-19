﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState { Zoomed, OnView };

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SwitchState(GameManager.GameState state)
    {
        switch (state) {
            case GameManager.GameState.OnView:
                break;
            case GameManager.GameState.Zoomed:
                break;
            default: 
                break;
        }
    }
}

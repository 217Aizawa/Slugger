using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public enum GameState { free, play, throwing, batting};

    public GameState gameState = GameState.free;

    public BatController batController;
    public CameraController camereController;
    public ThrowController throwController;
    public PhsController phsController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameState)
        {
            case GameState.free:

                break;

            case GameState.play:

                break;

            case GameState.throwing:

                break;

            case GameState.batting:

                break;
        }
    }
}

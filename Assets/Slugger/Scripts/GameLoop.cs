using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;


public class GameLoop : MonoBehaviour
{
    public enum GameState { free, play, throwing, batting};

    public GameState gameState = GameState.free;
    public GameObject ball;

    public BatController batController;
    public CameraController camereController;
    public ThrowController throwController;
    public PhsController phsController;
    public bool vrMode;//VRモードの切り替え
    public bool leftBatter;

    
    void Awake()//Startよりも先に呼び出される
    {
        /*if (!vrMode)
        {
            XRSettings.enabled = false;
            Debug.Log("VR is not enable");
        
        }
        else
            XRSettings.enabled = true;*/
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!vrMode)
        {
            XRSettings.enabled = false;
            Debug.Log("VR is not enable");

        }
        else
            XRSettings.enabled = true;


        if (leftBatter)
        {
            Debug.Log("left");
        }
        else
        {
            Debug.Log("right");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            UnityEngine.Application.Quit();
        }
        //空振りと打撃時のステートを用意しておいて、
        //それに応じて投球するプログラムを作成する

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

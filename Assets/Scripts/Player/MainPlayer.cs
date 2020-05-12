using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    #region Information of Player
    public static MainPlayer s_instance;

    //movement
    private PlayerInputHandler m_playerInput;
    //private bool isMouseClick = false;


    //state of player
    public enum E_StatePlayer {Idle, Move, Attack, None};
    public E_StatePlayer m_currentState = E_StatePlayer.Move;
    #endregion  

    #region UNITY
    private void Awake()
    {
        if(s_instance != null)
        {
            return;
        }
        s_instance = this;

    }

    private void Start()
    {
        m_playerInput = this.GetComponent<PlayerInputHandler>();
    }

    private void FixedUpdate()
    {
        this.MovementUpdate();

        switch(m_currentState)
        {
            case E_StatePlayer.Idle:
            {

                break;
            }
            case E_StatePlayer.Move:
            {
                m_playerInput.StatePlayerMove();
                break;
            }
            case E_StatePlayer.Attack:
            {

                break;
            }
            case E_StatePlayer.None:
            {

                break;
            }
        }
    }



    #endregion

    private void MovementUpdate()
    {
        // if(Input.GetMouseButtonDown(0))
        // {
        //     m_mouseClick = 0;
        //     //isMouseClick = true; 
        // }
        // if(Input.GetMouseButtonDown(1))
        // {
        //     m_mouseClick = 1;
        //     //isMouseClick = true; 
        // }
    }

    public Transform GetPlayerTransform()
    {
        return this.transform;
    }

}

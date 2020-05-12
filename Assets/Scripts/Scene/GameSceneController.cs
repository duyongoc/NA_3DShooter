using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    #region Init
    public static GameSceneController s_intance;

    private void Awake()
    {
        if(s_intance != null)
        {
            return;
        }
        s_intance = this;
    }
    #endregion

    public GameObject m_menuScene;
    public GameObject m_inGameScene;
    public GameObject m_overGameScene;


    #region UNITY
    private void Start()
    {
        m_menuScene.SetActive(true);
        m_inGameScene.SetActive(false);
        m_overGameScene.SetActive(false);
    }
    #endregion


    public void OnButtonPlayInMenuScene()
    {
        m_menuScene.SetActive(false);
        m_inGameScene.SetActive(true);
        m_overGameScene.SetActive(false);
    }

}

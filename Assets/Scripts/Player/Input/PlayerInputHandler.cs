using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    #region Field
    //component
    public Animator m_animator;

    [Header("Layer Mask")]
    public LayerMask m_layerMask;

    [Header("Speed of Player")]
    public int m_moveSpeed = 5;
    private Rigidbody m_rigidbody;
    private Vector3 m_vectorMovement;
    private float m_moveHorizontal;
    private float m_moveVertical;

    [Header("Movement JoyStick")]
    public VJHandle m_movementVJ;
    #endregion

    #region ANDROID
    Detection m_dectection;
    public List<Detection> enemies;

    private Detection currentEnemy;
    #endregion

    //current platform game is running
    public IPlatformInput platformInput;

    
    private void Start()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_dectection = this.GetComponent<Detection>();        
    }

    public void StatePlayerMove()
    {
        platformInput.GetPlayerMove(m_rigidbody, this.transform, m_movementVJ);
        platformInput.GetPlayerShoot();

        if(Platform.ANDROID)
            UpdateAutoDetectionEnemy();
    }

    public int GetSwitchWeaponInput()
    {
        if(true)
        {
            bool isGamepad = Input.GetAxis(GameInputConstants.k_ButtonNameGamepadSwitchWeapon) != 0f;
            string axisName = GameInputConstants.k_ButtonNameGamepadSwitchWeapon;

            if(Input.GetAxis(axisName) > 0f)
                return -1;
            else if(Input.GetAxis(axisName) < 0f)
                return 1;

        }
        return 0;
    }

    public int GetSelectWeaponInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            return 1;
        if(Input.GetKeyDown(KeyCode.Alpha2))
            return 2;
        if(Input.GetKeyDown(KeyCode.Alpha3))
            return 3;
        if(Input.GetKeyDown(KeyCode.Alpha4))
            return 4;
        if(Input.GetKeyDown(KeyCode.Alpha5))
            return 5;

        return 0;
    }

    //implement auto shotting enemies on android
    private void UpdateAutoDetectionEnemy()
    {
        enemies.Clear();
        foreach(Detection ene_detected in m_dectection.detected)
        {
            if(ene_detected != null)
            {
                if(ene_detected.tag.Contains("Enemy"))
                {
                    enemies.Add(ene_detected);
                }
            }
        }
        if(enemies.Count > 0)
        {
            currentEnemy = enemies[0];
            foreach(var enemy in enemies)
            {
                float dist1 = Vector3.Distance(transform.position, currentEnemy.transform.position);
                float dist2 = Vector3.Distance(transform.position, enemy.transform.position);

                if(dist2 < dist1)
                    currentEnemy = enemy;
            }
            if(Vector3.Distance(transform.position, currentEnemy.transform.position) < m_dectection.m_distance)
            {
                var gunTransform = this.GetComponent<PlayerWeaponHandler>().GetCurrentWeapon().transform;
                gunTransform.LookAt(currentEnemy.transform.position);
            }
        }

    }

    
}

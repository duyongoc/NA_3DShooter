using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    #region Field
    //component
    public Animator m_animator;

    [Header("Run on Android")]
    [ExecuteInEditMode] public bool isTestOnAndroid = true;

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

    //the target need to follow
    //private Transform m_target;
    #endregion


    #region ANDROID
    Detection m_dectection;
    public List<Detection> enemies;

    private Detection currentEnemy;
    #endregion


    #region UNITY
    private void Start()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_dectection = this.GetComponent<Detection>();
    }

    private void Update()
    {
        //StatePlayerMove(0);
    }
    #endregion

    public void StatePlayerMove()
    {
        // run game on Windows
        if(Platform.WINDOWS)
            GetMoveInputWindows();
        // run game on Android
        if(Platform.ANDROID)
            GetMoveInputAndroid();
    }


    private void GetMoveInputWindows()
    {
        m_moveHorizontal = Input.GetAxisRaw("Horizontal");
        m_moveVertical = Input.GetAxisRaw("Vertical");
        m_vectorMovement.Set(m_moveHorizontal, 0, m_moveVertical);
        m_vectorMovement = m_vectorMovement.normalized * Time.deltaTime * m_moveSpeed;
        m_rigidbody.MovePosition(this.transform.position + m_vectorMovement);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, m_layerMask))
        {
            Vector3 playerToMouse = hit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(playerToMouse);
            m_rigidbody.MoveRotation(rotation);
        }
    }

    private void GetMoveInputAndroid()
    {
        m_moveHorizontal = m_movementVJ.InputDirection.x;
        m_moveVertical = m_movementVJ.InputDirection.y;
        m_vectorMovement.Set(m_moveHorizontal, 0, m_moveVertical);
        m_vectorMovement = m_vectorMovement.normalized * Time.deltaTime * m_moveSpeed;
        m_rigidbody.MovePosition(this.transform.position + m_vectorMovement);

        UpdateAutoDetection();
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

    private void UpdateAutoDetection()
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
                var gunTransform = this.GetComponent<PlayerWeaponManager>().GetCurrentWeapon().transform;
                gunTransform.LookAt(currentEnemy.transform.position);
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponManager : MonoBehaviour
{
    public WeaponController[] m_WeaponSlots = new WeaponController[9];

    public Transform weaponMuzzle;
    public PlayerInputHandler m_InputHandler;

    public UnityAction<WeaponController> onSwitchedToWeapon;

    public int activeWeaponIndex = 1;

    private WeaponController currentWeapon;

    void Start()
    {
        onSwitchedToWeapon += OnWeaponSwitched;
        SwitchToWeaponIndex(activeWeaponIndex);
    }

    void Update()
    {
        currentWeapon = GetCurrentWeapon();

        if(Input.GetMouseButtonDown(0) && Platform.WINDOWS)
        {
            currentWeapon.HandleShootInputs();
        }
        else if(Input.GetKey(KeyCode.Space) && Platform.ANDROID)
        {
            currentWeapon.HandleShootInputs();
        }
        
        int switchWeaponInput = m_InputHandler.GetSelectWeaponInput();
        if(switchWeaponInput != 0)
        {
            if(GetWeaponAtSlotIndex(switchWeaponInput) != null)
                SwitchToWeaponIndex(switchWeaponInput);
        }
        
    }

    public WeaponController GetCurrentWeapon()
    {
        return GetWeaponAtSlotIndex(activeWeaponIndex);
    }

    public WeaponController GetWeaponAtSlotIndex(int index)
    {
        if(index >=0 && index < m_WeaponSlots.Length)
        {
            return m_WeaponSlots[index - 1];
        }

        return null;
    }

    void SwitchToWeaponIndex(int index)
    {
        activeWeaponIndex = index;
        WeaponController newWeapon = GetWeaponAtSlotIndex(activeWeaponIndex);

        if(onSwitchedToWeapon != null)
        {
            onSwitchedToWeapon.Invoke(newWeapon);
        }
    }

    void OnWeaponSwitched(WeaponController newWeapon)
    {
        foreach(WeaponController wp in m_WeaponSlots)
        {   
            if(wp != null)
            {
                if(wp.name == newWeapon.name)
                {
                    wp.ShowWeapon(true);
                }
                else
                {
                    wp.ShowWeapon(false);
                } 
            }
        }
    }

}

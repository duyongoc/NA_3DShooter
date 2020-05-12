using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{
    public GameObject weaponRoot;
    public UnityAction onShoot;
    public GameObject owner { get; set; }
    
    public ProjectTileBase projectTilePrefab;
    public Transform weaponMuzzle;
    
    private void Start()
    {
        owner = this.GetComponentInParent<PlayerWeaponHandler>().gameObject;
    }

    void Update()
    {
        
    }

    public void HandleShootInputs()
    {
        TryShoot();
    }

    private void TryShoot()
    {
        HandleShoot();
    }

    private void HandleShoot()
    {
        ProjectTileBase newProjectTile = Instantiate(projectTilePrefab, weaponMuzzle.position, weaponMuzzle.rotation );
        newProjectTile.Shoot(this);

        if (onShoot != null)
        {
            onShoot();
        }
    }


    public void ShowWeapon(bool show)
    {
        weaponRoot.SetActive(show);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform gunPos;
    public float weaponPickupDistance;

    GameObject currentWeapon;
    GameObject hoverWeapon;
    WeaponController currentWeaponController;
    
    Camera cam;

    bool isShooting = false;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (isShooting && currentWeaponController)
        {
            currentWeaponController.OnPerformFire();
        }

        Ray camRay = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit rayHit;

        if (Physics.Raycast(camRay, out rayHit, weaponPickupDistance))
        {
            if (rayHit.collider.CompareTag("Weapon"))
            {
                hoverWeapon = rayHit.collider.gameObject;
            }
            else
            {
                hoverWeapon = null;
            }
        }
    }

    void OnFire ()
    {
        isShooting = !isShooting;
    }
    
    void OnReload ()
    {
        currentWeaponController.OnReload();
    }

    //OnAimDownSights()

    void OnPickup ()
    {
        if (hoverWeapon)
        {
            if (currentWeapon)
            {
                currentWeapon.transform.SetParent(null);
                currentWeapon.transform.position = hoverWeapon.transform.position;
                currentWeapon.transform.rotation = hoverWeapon.transform.rotation;
                currentWeaponController.OnThrow();
            }

            currentWeapon = hoverWeapon;
            currentWeapon.transform.SetParent(gunPos);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
            currentWeaponController = currentWeapon.GetComponent<WeaponController>();
            currentWeaponController.OnPickup();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform gunPos;
    public float weaponPickupDistance;

    public TextMeshProUGUI ammoText;

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
            ammoText.text = currentWeaponController.currAmmo.ToString();
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
        ammoText.text = currentWeaponController.currAmmo.ToString();
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
                currentWeapon.transform.Rotate(Vector3.up, Random.Range(0, 180), Space.World);
                currentWeaponController.OnThrow();
            }

            currentWeapon = hoverWeapon;
            currentWeapon.transform.SetParent(gunPos);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
            currentWeaponController = currentWeapon.GetComponent<WeaponController>();
            currentWeaponController.OnPickup();
            ammoText.text = currentWeaponController.currAmmo.ToString();
        }
    }
}

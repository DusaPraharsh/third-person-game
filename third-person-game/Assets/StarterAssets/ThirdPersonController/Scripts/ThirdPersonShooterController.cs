using Cinemachine;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using System;

public class ThirdPersonShooterController : MonoBehaviour
{
    public CinemachineVirtualCamera aimVirCam;
    public GameObject crosshair;
    public float normalSensitivity;
    public float aimSensitivity;
    public LayerMask aimColliderLayerMask = new LayerMask();
    public Transform debugTransform;
    public Transform pfBulletProjectile;
    public Transform spawnBulletPosition;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (starterAssetsInputs.aim)
        {
            aimVirCam.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            crosshair.SetActive(true);
            thirdPersonController.SetRotationOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirCam.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            crosshair.SetActive(false);
            thirdPersonController.SetRotationOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (starterAssetsInputs.shoot)
        {
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.shoot = false;
        }
    }
}

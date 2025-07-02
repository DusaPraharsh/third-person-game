using Cinemachine;
using UnityEngine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    public CinemachineVirtualCamera aimVirCam;
    public GameObject crosshair;
    public float normalSensitivity;
    public float aimSensitivity;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        if (starterAssetsInputs.aim)
        {
            aimVirCam.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            crosshair.SetActive(true);
        }
        else
        {
            aimVirCam.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            crosshair.SetActive(false);
        }
    }
}

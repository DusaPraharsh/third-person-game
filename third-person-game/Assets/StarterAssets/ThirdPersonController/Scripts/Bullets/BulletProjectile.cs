using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    public Transform vfxHitGreen;
    public Transform vfxHitRed;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 50f;
        bulletRigidbody.linearVelocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>() != null)
        {
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private WeaponType weaponType;

    private float rotationSpeed = 720f;

    public float timetoLive;
    public Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(weaponType == WeaponType.knife || weaponType == WeaponType.arrow)
        {
            return;
        }
        else if(rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));
        }
    }

    public void AttackToDirection(Vector3 direction)
    {
        rb.velocity = direction * moveSpeed;
        StartCoroutine(SelfDestroy(timetoLive));
    }

    public IEnumerator SelfDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(rb.velocity != Vector3.zero && other.gameObject.layer == LayerMask.NameToLayer("characterLayer"))
        {
            Destroy(gameObject);
            other.GetComponent<Character>().OnDeath();
        }
    }
}

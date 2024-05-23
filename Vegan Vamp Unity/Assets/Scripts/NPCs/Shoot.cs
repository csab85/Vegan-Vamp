using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject bulletPrefab;
    FieldOfView fov;
    bool canShoot = true; 

    IEnumerator ShootBullet(float seconds)
    {
        canShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, null);
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
        yield return new WaitForSeconds(seconds);
        canShoot = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<FieldOfView>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);

        if (fov.isSeeingPlayer && canShoot)
        {
            StartCoroutine(ShootBullet(2));
        }
    }
}

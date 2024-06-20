using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDmg : MonoBehaviour
{
    [SerializeField] GameObject acid;
    [SerializeField] float damage;
    [SerializeField] bool bossProjectile;

    private void OnCollisionEnter(Collision collision)
    {
        StatsEffects enemyEffects = collision.gameObject.GetComponent<StatsEffects>();

        if (enemyEffects != null)
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;

            enemyEffects.DamageSelf(direction, damage);
        }

        if (bossProjectile)
        {
            GameObject newAcid = Instantiate(acid, transform.position, Quaternion.identity, null);
        }

        Destroy(gameObject);
    }
}

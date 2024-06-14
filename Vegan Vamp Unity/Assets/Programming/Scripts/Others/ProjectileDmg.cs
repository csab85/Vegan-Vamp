using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDmg : MonoBehaviour
{

    [SerializeField] float damage;

    private void OnCollisionEnter(Collision collision)
    {
        StatsEffects enemyEffects = collision.gameObject.GetComponent<StatsEffects>();

        if (enemyEffects != null)
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;

            enemyEffects.DamageSelf(direction, damage);
        }

        Destroy(gameObject);
    }
}

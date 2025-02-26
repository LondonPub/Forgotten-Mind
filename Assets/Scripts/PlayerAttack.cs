using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform  attackTransform;
    [SerializeField] private float  attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damageAmount = 1f;

    private RaycastHit2D[] hits;

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Attack();
        }
    }

    private void Attack()
    {
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

        for (int i = 0 ; i < hits.Length;  i++)
        {
           IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

           if (iDamageable != null)
           {
              //apply damage'
              iDamageable.Damage(damageAmount);
           }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
}

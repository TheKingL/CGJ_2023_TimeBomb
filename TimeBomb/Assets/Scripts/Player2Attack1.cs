using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Attack1 : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;

    [SerializeField] private Player2Life player2Life;

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0 && player2Life.bomb == true)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Collider2D[] playerDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < playerDamage.Length; i++)
                {
                    playerDamage[i].GetComponent<PlayerLife>().TakeDamage();
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}

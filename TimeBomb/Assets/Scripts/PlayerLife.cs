using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public ParticleSystem ps;

    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private GameObject spawnpoint;
    [SerializeField] private Player2Life player2Life;
    [SerializeField] public GameObject bombImageP1;
    [SerializeField] public GameObject bombImageP2;

    public bool bomb = false;
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            Die();
        }
    }

    public void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }*/
    




    public void ResetPlayer()
    {
        transform.position = spawnpoint.GetComponent<Spawnpoint>().GetSpawnPoint().position;
    }

    public void TakeDamage()
    {
        bomb = true;
        player2Life.bomb = false;
        bombImageP1.SetActive(true);
        bombImageP2.SetActive(false);
        ps.Play();
        deathSoundEffect.Play();
        Debug.Log("damagep1");
    }


}

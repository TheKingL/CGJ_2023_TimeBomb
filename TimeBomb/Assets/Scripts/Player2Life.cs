using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2Life : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public ParticleSystem ps;

    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private GameObject spawnpoint;
    [SerializeField] private PlayerLife player1Life;
    [SerializeField] public GameObject bombImageP1;
    [SerializeField] public GameObject bombImageP2;

    public bool bomb = true;
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    

    

    

    public void ResetPlayer()
    {
        transform.position = spawnpoint.GetComponent<Spawnpoint>().GetSpawnPoint().position;
    }

    public void TakeDamage()
    {
        bomb = true;
        player1Life.bomb = false;
        /*bombImageP1.hide();*/
        bombImageP2.SetActive(true);
        bombImageP1.SetActive(false);
        ps.Play();
        deathSoundEffect.Play();
        Debug.Log("damagep2");
    }










    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
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
}

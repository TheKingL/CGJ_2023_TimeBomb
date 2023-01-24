using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int items = 0;
    [SerializeField] private Text itemsText;
    [SerializeField] private AudioSource collectSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            items++;
            itemsText.text = "Items: " + items;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CanhaoInimigo : MonoBehaviour
{
    public int damageAmount = 20;
    public float speed = 30f;
    void Start ()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barco"))
        {
          // Verifica se o objeto que entrou na �rea de colis�o tem o script PlayerHealth
          PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

          // Se o objeto tiver o script PlayerHealth
          if (playerHealth != null)
          {
            // Reduz a sa�de do jogador pelo valor de dano da mina
            playerHealth.TakeDamage(damageAmount);
            AudioManager.instance.audioSource[2].Play();
            Destroy(gameObject);
          }
        }
      
    }
}

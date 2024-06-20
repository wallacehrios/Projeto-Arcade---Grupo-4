using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;

public class Mine : MonoBehaviour
{
    public int damageAmount = 200; // Quantidade de dano que a mina causa
    // M�todo chamado quando um objeto entra na �rea de colis�o da mina
    public GameObject explosionPrefab; // Prefab do efeito de explosão
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.tag);

        if (other.CompareTag("Barco") || other.CompareTag("Bomba"))
        {
            Debug.Log("Mine triggered by: " + other.tag);

            if (other.CompareTag("Barco"))
            {
                // Verifica se o objeto que entrou na área de colisão tem o script PlayerHealth
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

                // Se o objeto tiver o script PlayerHealth
                if (playerHealth != null)
                {
                    // Reduz a saúde do jogador pelo valor de dano da mina
                    playerHealth.TakeDamage(damageAmount);
                    Debug.Log("Player took damage: " + damageAmount);
                }
            }

            // Toca o som de explosão
            AudioManager.instance.audioSource[0].Play();
            // Instancia o efeito de explosão na posição da mina
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Debug.Log("Explosion instantiated");

            // Aguarda antes de desativar o objeto
            Destroy(gameObject);
        }
    }
}

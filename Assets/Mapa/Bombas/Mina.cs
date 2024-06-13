using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Mine : MonoBehaviour
{
    public int damageAmount = 200; // Quantidade de dano que a mina causa
    public AudioClip collectSound; // Som a ser reproduzido ao coletar o coletável de invencibilidade
    public float delayBeforeDisappear = 0.5f; // Tempo de espera antes do objeto desaparecer
    public AudioMixerGroup audioMixer;
    public GameObject explosionPrefab; // Prefab do efeito de explosão

    private AudioSource audioSource; // Componente de áudio para reproduzir o som

    void Start()
    {
        // Adiciona um componente de áudio ao objeto coletável
        audioSource = gameObject.AddComponent<AudioSource>();
        // Atribui o som ao componente de áudio
        audioSource.clip = collectSound;
        audioSource.outputAudioMixerGroup = audioMixer;
    }

    // Método chamado quando um objeto entra na área de colisão da mina
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
            audioSource.Play();
            // Instancia o efeito de explosão na posição da mina
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Debug.Log("Explosion instantiated");

            // Aguarda antes de desativar o objeto
            Invoke("DeactivateCollectible", delayBeforeDisappear);
        }
    }
    void DeactivateCollectible()
    {
        Destroy(gameObject);
    }
}

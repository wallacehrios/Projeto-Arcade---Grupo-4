using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VidaInimigos : MonoBehaviour
{
    public int maxHealth = 100; // O máximo de pontos de vida que o inimigo pode ter
    public int currentHealth; // A quantidade atual de pontos de vida do inimigo
    public GameObject explosionPrefab; // Prefab do efeito de explosão
    public AudioClip damageSound; // Som a ser reproduzido ao tomar dano
    public AudioMixerGroup audioMixer;
    public float volumeAudio = 1f;
    private AudioSource audioSource; // Componente de áudio para reproduzir o som

    void Start()
    {
        // Define a quantidade inicial de pontos de vida para o valor máximo
        currentHealth = maxHealth;

        // Adiciona um componente de áudio ao inimigo
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = damageSound;
        audioSource.outputAudioMixerGroup = audioMixer;
        audioSource.volume = volumeAudio;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            // Se a vida chegar a zero, chama o método para tratar a morte do inimigo
            HandleDeath();
        }
    }

    // Método para reduzir os pontos de vida do inimigo
    public void TakeDamage(int damageAmount)
    {
        // Reduz os pontos de vida do inimigo
        currentHealth -= damageAmount;
        Debug.Log("Inimigo tomou dano, vida atual: " + currentHealth);

        // Reproduz o som de dano
        if (audioSource != null && damageSound != null)
        {
            audioSource.Play();
        }
    }

    // Método para tratar a morte do inimigo
    void HandleDeath()
    {
        // Adiciona pontos ao ScoreManager
        ScoreManager.instance.AddPoint();

        // Instancia o efeito de explosão na posição do inimigo
        GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Debug.Log("Explosão instanciada na posição: " + transform.position);

        // Verifica se o sistema de partículas foi instanciado e está ativo
        ParticleSystem ps = explosionInstance.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            Debug.Log("Sistema de partículas encontrado e ativo.");
            ps.Play();
        }
        else
        {
            Debug.LogWarning("Nenhum sistema de partículas encontrado no prefab de explosão.");
        }

        // Destrói o inimigo
        Destroy(gameObject);
    }
}

using UnityEngine;
using UnityEngine.Audio;

public class InvincibilityCollectible : MonoBehaviour
{

    // M�todo chamado quando um objeto entra na �rea de colis�o do colet�vel de invencibilidade
    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou na �rea de colis�o � o jogador
        if (other.CompareTag("Barco"))
        {
            // Obt�m o componente InvincibilityController do jogador
            InvincibilityController invincibilityController = other.GetComponent<InvincibilityController>();

            // Se o componente InvincibilityController existir no jogador
            if (invincibilityController != null)
            {
                // Coleta o ponto de invencibilidade
                invincibilityController.CollectInvincibilityPoint();
                AudioManager.instance.audioSource[1].Play();
                Destroy(gameObject);
            }
        }
    }
}
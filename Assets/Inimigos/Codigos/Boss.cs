using UnityEngine;

public class Boss : MonoBehaviour
{
    public int danoAoTocar = 50; // Quantidade de dano ao tocar no jogador
    public float forcaEmpurrao = 10f; // Força aplicada para empurrar o jogador
    public GameObject bombaSpawnerPrefab; // Prefab do Bomba Spawner
    public GameObject inimigoSpawnerPrefab; // Prefab do Inimigo Spawner
    private bool bombaSpawnerCriado = false; // Flag para verificar se o Bomba Spawner foi criado
    private bool inimigoSpawnerCriado = false; // Flag para verificar se o Inimigo Spawner foi criado
    private float tempoInicio = 0f; // Tempo de início para controle do tempo

    // Método chamado quando outro collider entra em contato com o collider do Boss
    void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto que colidiu é o jogador
        if (collision.gameObject.CompareTag("Barco"))
        {
            // Obtém o componente PlayerHealth do jogador
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Se o componente PlayerHealth existir no jogador
            if (playerHealth != null)
            {
                // Aplica dano ao jogador
                playerHealth.TakeDamage(danoAoTocar);

                // Empurra o jogador para frente
                Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (playerRigidbody != null)
                {
                    // Calcula a direção do empurrão
                    Vector3 direcaoEmpurrao = collision.transform.forward;

                    // Aplica a força de empurrão
                    playerRigidbody.AddForce(direcaoEmpurrao * forcaEmpurrao, ForceMode.Impulse);
                }
            }
        }
    }

    // Método Update é chamado uma vez por frame
    void Update()
    {
        // Verifica se já passaram 10 segundos e o Bomba Spawner ainda não foi criado
        if (!bombaSpawnerCriado && Time.time - tempoInicio >= 10)
        {
            CriarBombaSpawner();
            bombaSpawnerCriado = true;
        }

        // Verifica se já passaram 20 segundos e o Inimigo Spawner ainda não foi criado
        if (!inimigoSpawnerCriado && Time.time - tempoInicio >= 20)
        {
            CriarInimigoSpawner();
            inimigoSpawnerCriado = true;
        }
    }

    // Método para criar o Bomba Spawner
    void CriarBombaSpawner()
    {
        Instantiate(bombaSpawnerPrefab, transform.position, Quaternion.identity);
    }

    // Método para criar o Inimigo Spawner
    void CriarInimigoSpawner()
    {
        Instantiate(inimigoSpawnerPrefab, transform.position, Quaternion.identity);
    }
}

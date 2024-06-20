using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

// Definição da classe InimigosIA que herda de MonoBehaviour
public class InimigosIA : MonoBehaviour
{
    // Declaração de variáveis públicas configuráveis no editor do Unity
    public float RaioVisao = 20; // Raio de visão do inimigo para detectar o jogador
    public float velocidadeGiro = 1f; // Velocidade com que o inimigo gira para olhar o jogador
    public GameObject balaCanhao; // Prefab da bala de canhão que será instanciada ao atirar
    public Transform[] pontosTiro; // Array de pontos de tiro, onde as balas serão instanciadas
    public float tempoSumir = 3f; // Tempo até que a bala de canhão instanciada seja destruída
    public AudioClip somTiro; // Som que será tocado ao atirar
    private float tempoSpawn = 0f; // Tempo restante para o próximo disparo
    private Transform alvo; // Referência ao Transform do jogador (alvo do inimigo)
    private NavMeshAgent agent; // Referência ao componente NavMeshAgent para movimentação do inimigo
    private AudioSource audioSource; // Componente de áudio para tocar o som de tiro

    // Método Start é chamado uma vez no início do jogo
    void Start()
    {
        // Encontra o jogador na cena e guarda sua Transform em 'alvo'
        alvo = GameObject.FindGameObjectWithTag("Barco").transform;
        // Obtém o componente NavMeshAgent anexado ao objeto inimigo
        agent = GetComponent<NavMeshAgent>();
        // Adiciona e configura o componente AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = somTiro;
    }

    // Método Update é chamado uma vez por frame
    void Update()
    {
        // Calcula a distância entre o inimigo e o jogador
        float distancia = Vector3.Distance(alvo.position, transform.position);

        // Se o jogador estiver dentro do raio de visão
        if (distancia <= RaioVisao)
        {
            // Define o destino do NavMeshAgent para a posição do jogador
            agent.SetDestination(alvo.position);

            // Se o inimigo estiver dentro da distância de parada do NavMeshAgent
            if (distancia <= agent.stoppingDistance)
            {
                // Chama o método para fazer o inimigo olhar para o jogador
                OlharAlvo();

                // Para cada ponto de tiro, chama o método para atirar
                foreach (Transform Pontos in pontosTiro)
                {
                    Atirar(Pontos);
                }
            }
        }
    }

    // Método para fazer o inimigo olhar para o jogador
    void OlharAlvo()
    {
        // Calcula a direção do jogador em relação ao inimigo
        Vector3 direcao = (alvo.position - transform.position).normalized;

        // Cria uma rotação para olhar para o jogador, mantendo a mesma altura
        Quaternion girarAlvo = Quaternion.LookRotation(new Vector3(direcao.x, 0, direcao.z));

        // Suavemente gira o inimigo para olhar para o jogador
        transform.rotation = Quaternion.Slerp(transform.rotation, girarAlvo, Time.deltaTime * velocidadeGiro);
    }

    // Método para atirar do ponto de tiro especificado
    void Atirar(Transform Pontos)
    {
        // Reduz o tempo de espera para o próximo disparo
        tempoSpawn -= Time.deltaTime;

        // Se o tempo de espera tiver passado
        if (tempoSpawn <= 0)
        {
            // Instancia uma bala de canhão no ponto de tiro
            GameObject clone = Instantiate(balaCanhao, Pontos.position, Pontos.rotation);

            // Destrói a bala de canhão após 'tempoSumir' segundos
            Destroy(clone, tempoSumir);

            // Toca o som de tiro
            audioSource.Play();

            // Reseta o tempo de espera para o próximo disparo
            tempoSpawn = 2.5f;
        }
    }

    // Método para desenhar gizmos no editor do Unity quando o objeto está selecionado
    void OnDrawGizmosSelected()
    {
        // Define a cor do gizmo
        Gizmos.color = Color.red;

        // Desenha uma esfera ao redor do inimigo para representar o raio de visão
        Gizmos.DrawWireSphere(transform.position, RaioVisao);
    }
}

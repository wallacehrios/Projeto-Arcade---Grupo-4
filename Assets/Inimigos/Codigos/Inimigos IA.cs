using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

// Defini��o da classe InimigosIA que herda de MonoBehaviour
public class InimigosIA : MonoBehaviour
{
    // Declara��o de vari�veis p�blicas configur�veis no editor do Unity
    public float RaioVisao = 20; // Raio de vis�o do inimigo para detectar o jogador
    public float velocidadeGiro = 1f; // Velocidade com que o inimigo gira para olhar o jogador
    public GameObject balaCanhao; // Prefab da bala de canh�o que ser� instanciada ao atirar
    public Transform[] pontosTiro; // Array de pontos de tiro, onde as balas ser�o instanciadas
    public float tempoSumir = 3f; // Tempo at� que a bala de canh�o instanciada seja destru�da
    public AudioClip somTiro; // Som que ser� tocado ao atirar
    private float tempoSpawn = 0f; // Tempo restante para o pr�ximo disparo
    private Transform alvo; // Refer�ncia ao Transform do jogador (alvo do inimigo)
    private NavMeshAgent agent; // Refer�ncia ao componente NavMeshAgent para movimenta��o do inimigo
    private AudioSource audioSource; // Componente de �udio para tocar o som de tiro
    public AudioMixerGroup audioMixer;
    public float volumeAudio = 1f;

    // M�todo Start � chamado uma vez no in�cio do jogo
    void Start()
    {
        // Encontra o jogador na cena e guarda sua Transform em 'alvo'
        alvo = GameObject.FindGameObjectWithTag("Barco").transform;
        // Obt�m o componente NavMeshAgent anexado ao objeto inimigo
        agent = GetComponent<NavMeshAgent>();
        // Adiciona e configura o componente AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = somTiro;
        audioSource.outputAudioMixerGroup = audioMixer;
        audioSource.volume = volumeAudio;
    }

    // M�todo Update � chamado uma vez por frame
    void Update()
    {
        // Calcula a dist�ncia entre o inimigo e o jogador
        float distancia = Vector3.Distance(alvo.position, transform.position);

        // Se o jogador estiver dentro do raio de vis�o
        if (distancia <= RaioVisao)
        {
            // Define o destino do NavMeshAgent para a posi��o do jogador
            agent.SetDestination(alvo.position);

            // Se o inimigo estiver dentro da dist�ncia de parada do NavMeshAgent
            if (distancia <= agent.stoppingDistance)
            {
                // Chama o m�todo para fazer o inimigo olhar para o jogador
                OlharAlvo();

                // Para cada ponto de tiro, chama o m�todo para atirar
                foreach (Transform Pontos in pontosTiro)
                {
                    Atirar(Pontos);
                }
            }
        }
    }

    // M�todo para fazer o inimigo olhar para o jogador
    void OlharAlvo()
    {
        // Calcula a dire��o do jogador em rela��o ao inimigo
        Vector3 direcao = (alvo.position - transform.position).normalized;

        // Cria uma rota��o para olhar para o jogador, mantendo a mesma altura
        Quaternion girarAlvo = Quaternion.LookRotation(new Vector3(direcao.x, 0, direcao.z));

        // Suavemente gira o inimigo para olhar para o jogador
        transform.rotation = Quaternion.Slerp(transform.rotation, girarAlvo, Time.deltaTime * velocidadeGiro);
    }

    // M�todo para atirar do ponto de tiro especificado
    void Atirar(Transform Pontos)
    {
        // Reduz o tempo de espera para o pr�ximo disparo
        tempoSpawn -= Time.deltaTime;

        // Se o tempo de espera tiver passado
        if (tempoSpawn <= 0)
        {
            // Instancia uma bala de canh�o no ponto de tiro
            GameObject clone = Instantiate(balaCanhao, Pontos.position, Pontos.rotation);

            // Destr�i a bala de canh�o ap�s 'tempoSumir' segundos
            Destroy(clone, tempoSumir);

            // Toca o som de tiro
            audioSource.Play();

            // Reseta o tempo de espera para o pr�ximo disparo
            tempoSpawn = 2.5f;
        }
    }

    // M�todo para desenhar gizmos no editor do Unity quando o objeto est� selecionado
    void OnDrawGizmosSelected()
    {
        // Define a cor do gizmo
        Gizmos.color = Color.red;

        // Desenha uma esfera ao redor do inimigo para representar o raio de vis�o
        Gizmos.DrawWireSphere(transform.position, RaioVisao);
    }
}

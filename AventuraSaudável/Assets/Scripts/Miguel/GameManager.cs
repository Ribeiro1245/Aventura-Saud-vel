using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// GameManager - Jogo de Apanhar Comidas (versão "Whack-a-Mole")
/// 
/// SETUP INSTRUCTIONS:
/// 1. Create a new Unity 2D project
/// 2. Attach this script to an empty GameObject called "GameManager"
/// 3. Create Food Hole prefabs (see FoodHole.cs)
/// 4. Set up UI Canvases (see UI Setup section below)
/// 5. Assign all references in the Inspector
/// 
/// UI SETUP:
/// - Canvas "MenuCanvas"   → Panel com 3 botões: Fácil, Médio, Difícil
/// - Canvas "GameCanvas"   → HUD com Score, Lives, Timer
/// - Canvas "GameOverCanvas" → Texto "Fim de Jogo!", Pontuação final, Botão "Jogar Novamente"
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // ───── Dificuldade ─────
    public enum Difficulty { Facil, Medio, Dificil }
    [HideInInspector] public Difficulty currentDifficulty;

    // ───── Config por dificuldade ─────
    [System.Serializable]
    public class DifficultySettings
    {
        public string name;
        public float gameDuration = 60f;   // segundos de jogo
        public float minShowTime = 1.2f;  // tempo mínimo que a comida fica visível
        public float maxShowTime = 2.5f;  // tempo máximo que a comida fica visível
        public float spawnInterval = 1.0f;  // intervalo entre spawns
        public float badFoodChance = 0.30f; // probabilidade de comida má (0–1)
        public int maxSimultaneous = 2;     // máx comidas visíveis ao mesmo tempo
    }

    [Header("Dificuldades")]
    public DifficultySettings[] difficultySettings = new DifficultySettings[]
    {
        new DifficultySettings { name="Fácil",   gameDuration=60f, minShowTime=1.5f, maxShowTime=3f,   spawnInterval=1.2f, badFoodChance=0.20f, maxSimultaneous=2 },
        new DifficultySettings { name="Médio",   gameDuration=45f, minShowTime=1.0f, maxShowTime=2f,   spawnInterval=0.8f, badFoodChance=0.30f, maxSimultaneous=3 },
        new DifficultySettings { name="Difícil", gameDuration=30f, minShowTime=0.6f, maxShowTime=1.2f, spawnInterval=0.5f, badFoodChance=0.45f, maxSimultaneous=4 }
    };

    // ───── Estado do jogo ─────
    [HideInInspector] public int score;
    [HideInInspector] public int lives;
    [HideInInspector] public float timeLeft;
    [HideInInspector] public bool gameRunning;

    private const int MAX_LIVES = 3;
    private const int GOOD_POINTS = 1;
    private const int BAD_POINTS = 1;   // pontos perdidos ao clicar em comida má

    // ───── Referências ─────
    [Header("Buracos de Comida (FoodHole[])")]
    public FoodHole[] foodHoles;          // arraste os buracos aqui no Inspector

    [Header("Canvases")]
    public GameObject menuCanvas;
    public GameObject gameCanvas;
    public GameObject gameOverCanvas;

    [Header("HUD (GameCanvas)")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timerText;

    [Header("Ecrã de Fim de Jogo (GameOverCanvas)")]
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI difficultyResultText;

    [Header("Efeitos Visuais (opcional)")]
    public GameObject goodHitFX;          // partícula ao acertar comida boa
    public GameObject badHitFX;           // partícula ao acertar comida má

    // ───── Internos ─────
    private DifficultySettings activeSettings;
    private Coroutine spawnCoroutine;
    private int activeCount = 0;          // comidas activas agora
    private float spawnTimer = 0f;

    // ══════════════════════════════════════════════
    //  UNITY LIFECYCLE
    // ══════════════════════════════════════════════

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        ShowMenu();
    }

    void Update()
    {
        if (!gameRunning) return;

        // Contagem decrescente
        timeLeft -= Time.deltaTime;
        UpdateHUD();

        // Spawn periódico
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            spawnTimer = activeSettings.spawnInterval;
            TrySpawnFood();
        }

        if (timeLeft <= 0f)
            EndGame();
    }

    // ══════════════════════════════════════════════
    //  FLUXO PRINCIPAL
    // ══════════════════════════════════════════════

    /// <summary>Mostra o menu de selecção de dificuldade.</summary>
    public void ShowMenu()
    {
        gameRunning = false;
        HideAllCanvases();
        menuCanvas.SetActive(true);
        HideAllFood();
    }

    /// <summary>Chamado pelos botões de dificuldade no menu.</summary>
    public void StartGame(int difficultyIndex)
    {
        currentDifficulty = (Difficulty)difficultyIndex;
        activeSettings = difficultySettings[difficultyIndex];

        score = 0;
        lives = MAX_LIVES;
        timeLeft = activeSettings.gameDuration;
        activeCount = 0;
        spawnTimer = 0f;

        HideAllCanvases();
        gameCanvas.SetActive(true);
        UpdateHUD();

        HideAllFood();
        gameRunning = true;
    }

    /// <summary>Termina o jogo (tempo esgotado ou vidas = 0).</summary>
    public void EndGame()
    {
        gameRunning = false;
        HideAllFood();

        HideAllCanvases();
        gameOverCanvas.SetActive(true);

        finalScoreText.text = "Pontuação: " + score;
        difficultyResultText.text = "Dificuldade: " + activeSettings.name;
    }

    /// <summary>Botão "Jogar Novamente" no ecrã de Game Over.</summary>
    public void RestartGame()
    {
        ShowMenu();
    }

    // ══════════════════════════════════════════════
    //  LÓGICA DE JOGO
    // ══════════════════════════════════════════════

    /// <summary>Tenta activar um buraco aleatório que esteja inactivo.</summary>
    void TrySpawnFood()
    {
        if (activeCount >= activeSettings.maxSimultaneous) return;

        // Recolhe buracos disponíveis
        List<FoodHole> available = new List<FoodHole>();
        foreach (var hole in foodHoles)
            if (!hole.IsActive) available.Add(hole);

        if (available.Count == 0) return;

        FoodHole chosen = available[Random.Range(0, available.Count)];
        bool isBad = Random.value < activeSettings.badFoodChance;
        float showTime = Random.Range(activeSettings.minShowTime, activeSettings.maxShowTime);

        chosen.Show(isBad, showTime);
        activeCount++;
    }

    /// <summary>Chamado por FoodHole quando o jogador clica numa comida boa.</summary>
    public void OnGoodFoodClicked(Vector3 worldPos)
    {
        score += GOOD_POINTS;
        activeCount = Mathf.Max(0, activeCount - 1);
        SpawnFX(goodHitFX, worldPos);
        UpdateHUD();
    }

    /// <summary>Chamado por FoodHole quando o jogador clica numa comida má.</summary>
    public void OnBadFoodClicked(Vector3 worldPos)
    {
        lives--;
        score = Mathf.Max(0, score - BAD_POINTS);
        activeCount = Mathf.Max(0, activeCount - 1);
        SpawnFX(badHitFX, worldPos);
        UpdateHUD();

        if (lives <= 0)
            EndGame();
    }

    /// <summary>Chamado por FoodHole quando a comida sai sozinha (sem ser clicada).</summary>
    public void OnFoodExpired()
    {
        activeCount = Mathf.Max(0, activeCount - 1);
    }

    // ══════════════════════════════════════════════
    //  UTILIDADES
    // ══════════════════════════════════════════════

    void UpdateHUD()
    {
        if (scoreText) scoreText.text = "Pontos: " + score;
        if (livesText) livesText.text = "Vidas: " + GetLivesString();
        if (timerText) timerText.text = "Tempo: " + Mathf.CeilToInt(Mathf.Max(0, timeLeft)) + "s";
    }

    string GetLivesString()
    {
        string s = "";
        for (int i = 0; i < MAX_LIVES; i++)
            s += (i < lives) ? "[X] " : "[ ] ";
        return s.Trim();
    }

    void HideAllFood()
    {
        if (foodHoles == null) return;
        foreach (var h in foodHoles) h.ForceHide();
    }

    void HideAllCanvases()
    {
        if (menuCanvas) menuCanvas.SetActive(false);
        if (gameCanvas) gameCanvas.SetActive(false);
        if (gameOverCanvas) gameOverCanvas.SetActive(false);
    }

    void SpawnFX(GameObject prefab, Vector3 pos)
    {
        if (prefab == null) return;
        GameObject fx = Instantiate(prefab, pos, Quaternion.identity);
        Destroy(fx, 2f);
    }
}
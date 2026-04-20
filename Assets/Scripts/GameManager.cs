using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string nextSpawnPoint;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) return;

        // Spawn
        if (!string.IsNullOrEmpty(nextSpawnPoint))
        {
            GameObject spawn = GameObject.Find(nextSpawnPoint);

            if (spawn != null)
            {
                player.transform.position = spawn.transform.position;
            }
        }

        // Camera
        CinemachineVirtualCamera vcam = FindFirstObjectByType<CinemachineVirtualCamera>();

        if (vcam != null)
        {
            vcam.Follow = player.transform;
        }
    }
}

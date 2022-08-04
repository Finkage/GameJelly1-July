// https://learn.unity.com/tutorial/implement-data-persistence-between-scenes

using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [Header("Persisting Variables")]
    public float mouseSensitivity = 0f;

    private void Awake()
    {
        // Prevent multiple instances of SaveManager (singleton pattern)
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Prevent this game object from being destroyed upon restart
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

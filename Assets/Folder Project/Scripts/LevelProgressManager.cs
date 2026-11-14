
using UnityEngine;

public class LevelProgressManager : MonoBehaviour
{
    public static LevelProgressManager Instance { get; private set; }

    public bool HasSunKey { get; private set; }
    public bool HasMoonKey { get; private set; }

    public bool HasBothKeys => HasSunKey && HasMoonKey;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void CollectSunKey()
    {
        HasSunKey = true;
        Debug.Log("Sun Key collected!");
    }

    public void CollectMoonKey()
    {
        HasMoonKey = true;
        Debug.Log("Moon Key collected!");
    }

    public bool UseSunKey()
    {
        if (!HasSunKey) return false;

        HasSunKey = false;
        return true;
    }

    public bool UseMoonKey()
    {
        if (!HasMoonKey) return false;

        HasMoonKey = false;
        return true;
    }
}

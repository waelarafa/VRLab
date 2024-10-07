using UnityEngine;

public class IGlobalSingleton<T> : MonoBehaviour
{
    private static T _instance;
    public static T instance
    {
        get => _instance;
        set
        {
            _instance = value;
        }
    }
    private void Awake()
    {
        instance = GetComponent<T>();
    }
    private void OnDisable()
    {
        instance = default(T);
    }
}

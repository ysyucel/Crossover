using UnityEngine;

public class SingletonMaster : MonoBehaviour
{
    private static SingletonMaster instance;

    public static SingletonMaster Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<SingletonMaster>();

                if (instance == null) {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<SingletonMaster>();
                    singletonObject.name = "SingletonMaster";
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return instance;
        }
    }

    private void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Add your Singleton functionality here...
}
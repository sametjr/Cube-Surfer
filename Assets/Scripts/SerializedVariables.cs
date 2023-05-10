using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializedVariables : MonoBehaviour
{
    #region Singleton
    private static SerializedVariables _instance;
    public static SerializedVariables Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SerializedVariables");
                go.AddComponent<SerializedVariables>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    [Header("Platform")]
    [SerializeField] private GameObject platform;
    private float platformSize;
    public float[] positions = new float[5];
    private void Start() {
        MeshRenderer mr = platform.GetComponent<MeshRenderer>();
        platformSize = mr.bounds.size.x;

        for(int i = 0; i < 5; i++)
        {
            positions[i] = mr.bounds.min.x + platformSize * i / 5;
        }
    }

    [Header("Movement")]
    public float speed = 3f;
    public float increment = 5f;
    public float jumpAddition = 0.2f;

    [Header("Camera")]
    public float followOffsetIncrement = .2f;
    public float lerpValue = .7f;
    public float fovIncrement = 6f;

    [Header("Environment")]
    public float timeToDestroyCubeOnLava = 1f;
    public int maxParticlePoolSize = 30;

    [Header("Gameplay")]
    public int scorePerDiamond = 10;

}

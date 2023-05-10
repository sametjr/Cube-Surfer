using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CubeController : MonoBehaviour
{
    #region Singleton
    private static CubeController _instance;
    public static CubeController Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("CubeController");
                go.AddComponent<CubeController>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    private List<GameObject> cubes;
    [HideInInspector] public float cubeSize;


    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CameraControls cameraControls;
    [SerializeField] private ParticlesHandler particlesHandler;
    [SerializeField] private UIHandler uiHandler;

    [SerializeField] private GameObject initialCube;
    [SerializeField] private GameObject bottomOfCubes;
    [SerializeField] private GameObject _trails;


    [System.Serializable] public class CubeCountChanged : UnityEvent { }
    [System.Serializable] public class EntityCollected : UnityEvent { }
    public CubeCountChanged OnCubeAdded;
    public CubeCountChanged OnCubeRemoved;
    public EntityCollected OnDiamondCollected;

    private void Start()
    {
        MeshRenderer mr = initialCube.GetComponent<MeshRenderer>();
        cubeSize = mr.bounds.size.y;
        cubes = new();
        cubes.Add(initialCube);
    }

    #region Subscriptions/Listeners
    private void OnEnable() {
        OnCubeAdded.AddListener(characterMovement.CubeAdded);
        OnCubeAdded.AddListener(cameraControls.CubeAdded);
        OnCubeRemoved.AddListener(cameraControls.CubeRemoved);
        OnDiamondCollected.AddListener(delegate { particlesHandler.PlayDiamondCollectParticles(bottomOfCubes.transform.position); });
        OnDiamondCollected.AddListener(GameManager.Instance.AddDiamondToScore);
        OnDiamondCollected.AddListener(uiHandler.UpdateScore);
    }

    private void OnDisable() {
        OnCubeAdded.RemoveListener(characterMovement.CubeAdded);
        OnCubeAdded.RemoveListener(cameraControls.CubeAdded);
        OnCubeRemoved.RemoveListener(cameraControls.CubeRemoved);
        OnDiamondCollected.RemoveListener(uiHandler.UpdateScore);
    }
    #endregion


    public void AddCube(GameObject _cube)
    {
        if (cubes.Contains(_cube)) return;

        _cube.transform.parent = transform;
        // characterMovement.CubeAdded(cubeSize);
        Transform lastCubeTransform = cubes[cubes.Count - 1].transform;
        _cube.transform.position = new Vector3(lastCubeTransform.position.x, lastCubeTransform.position.y - cubeSize, lastCubeTransform.position.z);
        cubes.Add(_cube);


        SoundManager.Instance.PlaySound(SoundManager.SoundType.CubeAdded);
        OnCubeAdded.Invoke();
    }

    public void RemoveCube(GameObject _cube)
    {

        cubes.Remove(_cube);
        _cube.transform.SetParent(null);
        if(cubes.Count <= 0 && GameManager.Instance.isPlaying) GameManager.Instance.GameOver();
        SoundManager.Instance.PlaySound(SoundManager.SoundType.CubeRemoved);
        OnCubeRemoved.Invoke();
    }

    private void LateUpdate()
    { 
        if(!GameManager.Instance.isPlaying) return;
        Vector3 lastCubePosition = cubes[cubes.Count - 1].transform.position;
        bottomOfCubes.transform.position = lastCubePosition;
        _trails.transform.position = new Vector3(transform.position.x, _trails.transform.position.y, transform.position.z);
    }

    public void DestroyLastCube()
    {
        Destroy(cubes[cubes.Count - 1]);
        cubes.RemoveAt(cubes.Count - 1);
        SoundManager.Instance.PlaySound(SoundManager.SoundType.CubeRemoved);
        if(cubes.Count <= 0 && GameManager.Instance.isPlaying) GameManager.Instance.GameOver();
    }

    public void DiamondCollected()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.DiamondCollected);
        OnDiamondCollected.Invoke();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SoundManager");
                go.AddComponent<SoundManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _cubeAdded,
                                       _diamondCollected
                                       ,_cubeRemoved;

    public void PlaySound(SoundType _soundType)
    {
        switch(_soundType)
        {
            case SoundType.CubeAdded:
                _audioSource.PlayOneShot(_cubeAdded);
                break;
            case SoundType.DiamondCollected:
                _audioSource.PlayOneShot(_diamondCollected);
                break;
            case SoundType.CubeRemoved:
                _audioSource.PlayOneShot(_cubeRemoved);
                break;
        }
    }


    public enum SoundType
    {
        CubeAdded,
        DiamondCollected,
        CubeRemoved,
    }
}

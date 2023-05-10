using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesHandler : MonoBehaviour
{
    [SerializeField] private GameObject _particlesPrefab;
    private Queue<GameObject> particlesPool = new Queue<GameObject>();
    
    private void Start() {
        for(int i = 0; i < SerializedVariables.Instance.maxParticlePoolSize; i++)
        {
            GameObject go = Instantiate(_particlesPrefab, transform);
            go.SetActive(false);
            particlesPool.Enqueue(go);
        }    
    }
    public void PlayDiamondCollectParticles(Vector3 _pos)
    {
        if(particlesPool.Count > 0)
        {
            GameObject go = particlesPool.Dequeue();
            go.transform.position = _pos;
            go.SetActive(true);
            if(!go.GetComponent<ParticleSystem>().isPlaying)
                go.GetComponent<ParticleSystem>().Play();
            
            StartCoroutine(DeactivateParticles(go));
        }
    }

    private IEnumerator DeactivateParticles(GameObject _go)
    {
        yield return new WaitForSeconds(1f);
        _go.SetActive(false);
        particlesPool.Enqueue(_go);
    }
}

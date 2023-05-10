using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    private Vector2 bounds;

    private void Start() {
        MeshRenderer mr = GameObject.FindGameObjectWithTag("Ground").GetComponent<MeshRenderer>();
        bounds = new Vector2(mr.bounds.min.x, mr.bounds.max.x);
        
    }

    

    private void Update() {
        
        _transform.position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.z + SerializedVariables.Instance.speed * Time.deltaTime);
    }
    public void MoveLeft()
    {
        if(_transform.position.x - SerializedVariables.Instance.increment * Time.deltaTime < bounds.x) return; // If the character is at the leftmost position, don't move left
        _transform.position = new Vector3(_transform.position.x - SerializedVariables.Instance.increment * Time.deltaTime, _transform.position.y, _transform.position.z);
    }

    public void MoveRight()
    {
        if(_transform.position.x + SerializedVariables.Instance.increment * Time.deltaTime > bounds.y) return; // If the character is at the rightmost position, don't move right
        _transform.position = new Vector3(_transform.position.x + SerializedVariables.Instance.increment * Time.deltaTime, _transform.position.y, _transform.position.z);
    }

    public void CubeAdded()
    {
        _transform.position = new Vector3(_transform.position.x, _transform.position.y + CubeController.Instance.cubeSize + SerializedVariables.Instance.jumpAddition, _transform.position.z);
    }
}

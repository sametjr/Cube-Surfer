using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _finalScoreText;
    [SerializeField] private GameObject _multiplierText;
    [SerializeField] private GameObject _player;

    [SerializeField] BoxCollider _stairsCollider;

    public void UpdateScore()
    {
        _scoreText.text = GameManager.Instance.score.ToString();
        LeanTween.scale(_scoreText.gameObject, Vector3.one * 1.5f, .15f).setEasePunch().setOnComplete(() => {
            LeanTween.scale(_scoreText.gameObject, Vector3.one, .15f).setEasePunch();
        });
    }

    public void ShowGameOverPanel()
    {
        Debug.Log("Show Game Over Panel");
        int multiplier = GetMultiplier() + 1; //if the player is on the first stair, the multiplier will be 2
        if (multiplier == 0) multiplier = 1; //  if the player is on the ground, the multiplier will be 1
        if (multiplier == 11) multiplier = 10; // if the player is on the last stair, the multiplier will be 10

        _finalScoreText.GetComponent<TMP_Text>().text = GameManager.Instance.score.ToString();
        GameManager.Instance.score *= multiplier; // TODO: Change this to the actual multiplier

        LeanTween.moveLocal(_gameOverPanel, Vector3.zero, 1f).setEaseOutBack().setOnComplete(() => {
            _multiplierText.GetComponent<TMP_Text>().text = "x" + multiplier.ToString();
            LeanTween.moveLocal(_multiplierText, _finalScoreText.GetComponent<RectTransform>().rect.position, .5f).setEaseOutBack();

            LeanTween.scale(_multiplierText, Vector3.one * 1.5f, .5f).setEaseOutBack().setOnComplete(() => {

                LeanTween.scale(_multiplierText, Vector3.one, .5f).setEaseOutBack().setOnComplete(() => {
                    _multiplierText.SetActive(false);

                    LeanTween.scale(_finalScoreText, Vector3.one * 1.5f, .5f).setEaseOutBack().setOnComplete(() => {
                        _finalScoreText.GetComponent<TMP_Text>().text = GameManager.Instance.score.ToString();
                        LeanTween.scale(_finalScoreText, Vector3.one, .5f).setEaseOutBack();
                    }); 

                });

            });

        });
    }

    public int GetMultiplier()
    {
        // There are 10 stairs in total, when the game over, find out which stair the player is on and return the multiplier
        
        // if the player couldnt reach the stairs, return 1, if the player has passed the stairs, return 10
        if(_player.transform.position.z < _stairsCollider.bounds.min.z) return 0;
        if(_player.transform.position.z > _stairsCollider.bounds.max.z) return 10;

        int multiplier = 1;
        
        float distance = _stairsCollider.bounds.max.z - _stairsCollider.bounds.min.z;
        Debug.Log("Distance: " + distance);
        float playerDistance = _player.transform.position.z - _stairsCollider.bounds.min.z;
        Debug.Log("Player Distance: " + playerDistance);
        float percentage = playerDistance / distance;
        multiplier = Mathf.RoundToInt(percentage * 10);
        Debug.Log("Multiplier: " + multiplier);
        return multiplier;
    }
}

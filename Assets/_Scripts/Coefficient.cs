using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coefficient : MonoBehaviour
{
    [SerializeField] private TMP_Text _coefficientText;
    private float _coefficient = 0f;

    private void Start()
    {
        StartCoroutine(IncreaseScore());
    }

    private IEnumerator IncreaseScore()
    {
        while(true)
        {
            _coefficient += 0.05f;
            _coefficientText.text = _coefficient.ToString("f2") + "x";
            yield return new WaitForSeconds(0.1f);
        }
    }
}

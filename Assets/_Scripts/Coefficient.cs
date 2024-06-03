using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coefficient : MonoBehaviour
{
    [SerializeField] private TMP_Text _coefficientText;
    [SerializeField] private GameObject _lineWithArea;
    public static float CurrentCoefficient;

    private void Start()
    {
        CurrentCoefficient = 0.00f;
    }

    public void StartCounting()
    {
        StartCoroutine(IncreaseScore());
    }

    private IEnumerator IncreaseScore()
    {
        while(_lineWithArea != null)
        {
            CurrentCoefficient += 0.05f;
            _coefficientText.text = CurrentCoefficient.ToString("f2") + "x";
            yield return new WaitForSeconds(0.1f);
        }
    }
}

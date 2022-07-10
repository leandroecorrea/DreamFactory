using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetailView : MonoBehaviour
{
    [SerializeField] private TMP_Text detailName;
    [SerializeField] private TMP_Text amount;

    public void OnEnable()
    {
        amount.gameObject.SetActive(false);
    }
    public void SetName(string name)
        => detailName.text = name;
    public void SetAmount(string amountText)
    {
        amount.gameObject.SetActive(true);
        amount.text = amountText;
    }
}

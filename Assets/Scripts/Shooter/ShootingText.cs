using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void SetText(int count)
    {
        _text.text = count.ToString();
    }
}

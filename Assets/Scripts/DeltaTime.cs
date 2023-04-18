using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeltaTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI deltaTime;

    private void Update()
    {
        deltaTime.text += "\n" + Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesCounter : MonoBehaviour
{
    TextMeshProUGUI liveCounts;

    private void Start()
    {
        liveCounts = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        liveCounts.text = "x" + GameManager.Instance.playerLives;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Juego gameManager;
    public TextMeshProUGUI countGold;
    public TextMeshProUGUI countLevel;
    public TextMeshProUGUI countWave;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This sets our HUD's texts to our GameManager's attributes current values every frame.
        countGold.text = gameManager.Gold.ToString();
        countLevel.text = gameManager.Level.ToString();
        countWave.text = gameManager.Wave.ToString();
    }
}

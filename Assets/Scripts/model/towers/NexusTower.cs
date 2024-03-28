using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NexusTower : MonoBehaviour
{
    public TextMeshProUGUI countHealth;
    public int Health { get { return health; } set { health = value; } }
    private int health;

    public Juego gameManager;

    public AudioSource explo;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        explo = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        countHealth.text = Health.ToString();
        if(health < 10)
        {
            countHealth.color = Color.red;
        }
    }

    public void takeDamage(int amount)
    {
        health -= amount;
        if (Health <= 0)
        {
            gameManager.PlayAudioSource(explo);
            StartCoroutine(DelayedDestroy(1f));
            
        }
    }

    IEnumerator DelayedDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        gameManager.endGame();
    }

}

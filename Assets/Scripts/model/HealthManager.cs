using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float health = 10f;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //This method substracts health. This class just handles the visuals so it does not affect our actual nexus. Just their healthbar
    public void takeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / 10f;
    }
}

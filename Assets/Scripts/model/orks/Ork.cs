using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Ork : MonoBehaviour
{
    public const int LIMIT1 = 20;
    public const int LIMIT2 = 40;
    public const int LIMIT3 = 60;
    public int Health { get { return health; } set { health = value; } }
    private int health;

    public int Armor { get { return armor; } set { armor = value; } }
    private int armor;

    private int[] armorValues = new int[3] { 10, 20, 30 };
    private int[] healthValues = new int[3] { 150, 200, 250 };
    private int[] goldValues = new int[3] { 1, 1, 2 };

    public float speed;
    public Vector2 currentDirection;
    public int ork_type;

    private Animator animator;
    private Juego gameManager;
    private int countRight = 0;

    private SpriteRenderer orkRenderer;

    public bool isSlowed;

    BoxCollider2D bc2d;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        orkRenderer = GetComponent<SpriteRenderer>();
        speed = 1f;
        currentDirection = Vector2.left;
        gameManager = GameObject.Find("GameManager").GetComponent<Juego>();
        bc2d = GetComponent<BoxCollider2D>();
        health = healthValues[ork_type];
        armor = armorValues[ork_type];
    }

    // Update is called once per frame
    void Update()
    {
        //While the ork is alive, they play their walk audioSource
        gameManager.PlayAudioSource(gameManager.walk);

        //This is the ork's movement. It moves automatically in our current direction, for our current speed at every frame
        transform.Translate(currentDirection * speed * Time.deltaTime);

        //Manages sprite's color according to whether they're slowed or not
        if(isSlowed == true){
            orkRenderer.color = new Color(0.3f, 0.3f, 1.0f, 1.0f);
        }
        else
        {
            orkRenderer.color = Color.white;
        }
    }
    /*
     * This method checks for collisions. If it collides with any collider we've set in the 'Tilemap', it changes the ork's
     * direction. This method manages both enemy's movement and enemy's attacks on the player.
     */
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Tilemap"))
        {
            
            if (currentDirection == Vector2.left)
            {
                MoveDown();
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (currentDirection == Vector2.down)
            {
                MoveRight();
            }
            else if (currentDirection == Vector2.right)
            {
                if (countRight == 1)
                {
                    MoveDown();
                }
                else
                {
                    MoveUp();
                    countRight++;
                }
            }
            else if (currentDirection == Vector2.up)
            {
                MoveRight();
            }
        }

        if (col.gameObject.CompareTag("Player"))
        {
            attack(col.gameObject.transform);
        }
    }

    // This method changes current direction to left
    public void MoveLeft()
    {
        currentDirection = Vector2.left;
    }

    // This method changes current direction to right
    public void MoveRight()
    {
        currentDirection = Vector2.right;
    }

    // This method changes current direction to up
    public void MoveUp()
    {
        currentDirection = Vector2.up;
    }

    // This method changes current direction to down
    public void MoveDown()
    {
        currentDirection = Vector2.down;
    }

    /*
     //We would use this in endless mode  
     
    public void valuesUpdatedByLevel(int tipo_orco)
    {
        armorValues[tipo_orco] += (1 * gameManager.Level);
        healthValues[tipo_orco] += (2 * gameManager.Level);
    }
    */

    /*
     * This method handles the armor logic. From 0 to 20 -> value = armor
     *                                      From 20 to 40 -> value = 20 + armor-20/2
     *                                      From 40 to 60 -> value = 20 + armor-20/2 + armor-40/4
     *                                      From 60 -> value = 20 + armor-20/2 + armor-40/4 + armor-60/10
     */
    public double getArmorReduction()
    {
        double value = 0;
        if (this.Armor <= LIMIT1)
        {
            value = this.Armor;
        }
        else if (this.Armor <= LIMIT2)
        {
            value = (LIMIT1 + ((this.Armor - LIMIT1) * 0.5));
        }
        else if (this.Armor <= LIMIT3)
        {
            value = (LIMIT1 + ((LIMIT2 - LIMIT1) * 0.5) + ((this.Armor - LIMIT2) * 0.25));
        }
        else
        {
            value = (LIMIT1 + ((LIMIT2 - LIMIT1) * 0.5) + ((LIMIT3 - LIMIT2) * 0.25) + ((this.Armor - LIMIT3) * 0.1));
        }
        return value;
    }

    /*
     * This method handles our enemies attacks. When our enemy collides with our nexus, this method is called by the nexus
     * transform. We get the nexus and healthmanager scripts to substract 1 point of health from both. Then we destroy this
     * gameObject
     */
    public void attack(Transform target)
    {
        NexusTower nexus = target.gameObject.GetComponent<NexusTower>();
        nexus.takeDamage(1);

        HealthManager hManager = target.gameObject.GetComponent<HealthManager>();
        hManager.takeDamage(1);
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        gameManager.PlayAudioSource(gameManager.die);
        gameManager.Gold += this.goldValues[this.ork_type];
        gameManager.orks.Remove(this.gameObject);
    }
    //This method is called when projectiles1 hit our enemies to apply slow on them.
    public void SlowDown()
    {
        StartCoroutine(SlowDownForDuration());
    }
    /*
     * This handles the slow logic. If this object is not slowed, we set a local variable with their current speed(normal speed)
     * then reduce their speed by 0.8f and set isSlowed = True for they are not able to be slowed again until debuff is removed.
     * Then we wait for 2 seconds and return to normal speed, and set isSlowed =False for they are able to be slowed again.
     */
    IEnumerator SlowDownForDuration()
    {
        if (!isSlowed)
        {
            float originalEnemySpeed = speed;
            speed *= 0.8f;

            isSlowed = true;

            yield return new WaitForSeconds(2f);

            speed = originalEnemySpeed;

            isSlowed = false;
        }
    }
}
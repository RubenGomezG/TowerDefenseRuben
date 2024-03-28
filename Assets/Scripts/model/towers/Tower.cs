using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private float[] rangeValues = new float[2] { 3f, 4f};
    
    public float Range { get { return range; } set { range = value; } }
    private float range;

    public int tower_type;
    public int[] towerCost_Values = new int[2] { 3, 4 };

    private float[] attackSpeedValues = new float[2] { 1.25f, 0.9f };
    private float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    private float attackSpeed;

    public GameObject projectile;

    private float cooldown = 0f;


    // Start is called before the first frame update
    void Start()
    {
        this.attackSpeed = attackSpeedValues[tower_type];
        this.range = rangeValues[tower_type];
    }

    // Update is called once per frame
    void Update()
    {
        //If the instantiated tower cooldown is ready, it attacks, else, we substract deltaTime until its ready.
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            attack();
        }
    }

    /*
     * This method calls findtarget() to get a valid transform, we check if it is, and then Instantiate a Projectile on 
     * this object's position, then set their class variable transform to our target's transform and set a cooldown for
     * next attack equal to 1 divided by the amount of times we will attack in a second. If our target is null, this method
     * does nothing.
     */
    public void attack() 
    {
        Transform target = findTarget();
        if (target != null)
        {
            GameObject projectileInstance = Instantiate(projectile, this.transform.position, Quaternion.identity);

            // Asigna el objetivo al proyectil (puedes implementar lógica más avanzada aquí)
            Projectile scriptProjectile = projectileInstance.GetComponent<Projectile>();
            scriptProjectile.target = target;

            cooldown = 1f/this.attackSpeed;
        }

    }

    /*
     * This method stores in a variable every collider2D found that belongs to LayerMask(GroundEnemies)
     * from our object's position, in our object's range. And then for each one, we save those who have a tag Respawn
     * to a list. If there is any, this method returns the Transform component of the first one in the list.
     */
    public Transform findTarget()
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("GroundEnemies"));
        List <Collider2D> enemiesList = new List<Collider2D>();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.CompareTag("Respawn"))
            {
                enemiesList.Add(enemies[i]);
            }
        }
        if (enemiesList.Count > 0)
        {
            return enemiesList[0].transform;
        }

        return null;
    }
    
}

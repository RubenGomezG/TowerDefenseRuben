using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;

    [SerializeField] int amountToPool;
    [SerializeField] GameObject[] towerToPool;

    List<GameObject> towersPooled;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        towersPooled = new List<GameObject>();

        /* For each kind of prefab we have, we instantiate a tower of the kind and add it to our class variable list and
         set them to unactive until we place them on the map
         */
        for (int i = 0; i < towerToPool.Length; i++)
        {
            for (int j = 0; j < amountToPool; j++)
            {
                GameObject tmp = Instantiate(towerToPool[i]);
                tmp.SetActive(false);
                towersPooled.Add(tmp);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // We access this on our spawn class to get the amount of towers1 that we will be able to place
    public GameObject[] getTower1Pooled()
    {
        GameObject[] towers1 = new GameObject[amountToPool];
        for (int i = 0; i < amountToPool; i++)
        {
            towers1[i] = towersPooled[i];
        }
        return towers1;
    }

    // We access this on our spawn class to get the amount of towers2 that we will be able to place
    public GameObject[] getTower2Pooled()
    {
        GameObject[] towers2 = new GameObject[amountToPool];
        for (int i = 0; i < amountToPool; i++)
        {
            towers2[i] = towersPooled[i + amountToPool];
        }
        return towers2;
    }

    /*public GameObject[] getTower3Pooled()
    {
        GameObject[] towers3 = new GameObject[amountToPool];
        for (int i = 0; i < amountToPool; i++)
        {
            towers3[i] = towersPooled[i + amountToPool * 2];
        }
        return towers3;
    }*/
}

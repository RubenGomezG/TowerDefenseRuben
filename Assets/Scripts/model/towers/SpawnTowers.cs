using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTowers : MonoBehaviour
{

    [SerializeField] Sprite[] towerSprites;
    [SerializeField] SpriteRenderer towerSilhouette;

    public Juego gameManager;

    private List<GameObject> placedTowers = new List<GameObject>();

    GameObject[] towerToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        towerSilhouette.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        towerSilhouettePosition();
        addTower();
    }

    /*
     Instantiates the tower that we have previously picked. If we havent picked any, it returns. Then checks that we have
    used mouse left click. Then checks if there is any tower in 1.2f from your cursor current placement. And if we pass all
    the checks, we do a for loop to check if there's any tower of the kind in the pool, ready to be placed.

    After that, we check again if the tower is not active in the game and if our player's gold is enough to pay for the tower.
    If so, we set the tower to active in the position of the silhouette sprite.(it was already instantiated, but its easier to say
    it this way). We set the silhouette sprite back to null so we can click our buttons again and add this tower to a list of 
     placed towers, for the previous overlap check.

    Finally, we substract the gold cost of the tower from the player.
     */
    void addTower()
    {
        if (towerSilhouette.sprite != null) {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (!IsOverlappingExistingTower(cursorWorldPosition))
                {
                    for (int i = 0; i < towerToSpawn.Length; i++)
                    {
                        if (!towerToSpawn[i].activeInHierarchy && gameManager.Gold >= towerToSpawn[i].gameObject.GetComponent<Tower>().towerCost_Values[towerToSpawn[i].gameObject.GetComponent<Tower>().tower_type]) {
                            towerToSpawn[i].SetActive(true);
                            towerToSpawn[i].transform.position = (Vector2)towerSilhouette.transform.position;
                            towerSilhouette.sprite = null;
                            //DontDestroyOnLoad(towerToSpawn[i]); use this in case we play endless mode
                            placedTowers.Add(towerToSpawn[i]);
                            gameManager.Gold -= towerToSpawn[i].gameObject.GetComponent<Tower>().towerCost_Values[towerToSpawn[i].gameObject.GetComponent<Tower>().tower_type];
                            break;
                        }
                    }
                }
            }        
        }
    }

    /*
     Sets the TowerSilhouette sprite to the cursor position until we place a tower.(after button click and updating
    until we set it back to null after we place the tower
     */
    void towerSilhouettePosition()
    {
        Vector2 cursorLocation = Input.mousePosition;
        Vector2 cursorWorldPosition = Camera.main.ScreenToWorldPoint(cursorLocation);
        towerSilhouette.transform.position = cursorWorldPosition;
    }

    /*
     We have created an array of GameObjects based on their sprites on the auxiliar class ObjectPooling. Those are instantiated 
    and added to a list of towers to pool. So you will be able to access this method the amount of those instances of that
    particular tower pooled. This is only used in the buttons to select the tower that we are going to create.
    
     */
    public void pickTower(int tower_type)
    {
        towerSilhouette.sprite = towerSprites[tower_type];

        if(tower_type == 0)
        {
            towerToSpawn = ObjectPooling.Instance.getTower1Pooled();
        }
        else if (tower_type == 1)
        {
            towerToSpawn = ObjectPooling.Instance.getTower2Pooled();
        }
        /*else if (tower_type == 2)
        {
            towerToSpawn = ObjectPooling.Instance.getTower3Pooled();
        }
        */
    }
    /*
     For each tower already placed, checks the distance between a position parameter(will always be our mouse), if the distance
    is less than 1.2f, returns true. Else, returns false. If its true it will not let us place another tower in the same place
    when we call this method for check.
     */
    private bool IsOverlappingExistingTower(Vector3 position)
    {
        foreach (var tower in placedTowers)
        {
            float distance = Vector2.Distance(position, tower.transform.position);
            if (distance < 1.2f)
            {
                return true; // Overlaps with an existing tower
            }
        }
        return false; // Position is valid
    }
}

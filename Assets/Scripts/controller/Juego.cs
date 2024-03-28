using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Juego : MonoBehaviour
{
    public List<GameObject> orks;

    public List<GameObject> orkPrefabs;
    public int Level { get { return level; } set { level = value; } }
    private int level = 1;

    public int Wave { get { return wave; } set { wave = value; } }
    private int wave = 1;

    public int Gold { get { return gold; } set { gold = value; } }
    private int gold = 10;

    public GameObject nexo;

    public AudioSource die;
    public AudioSource walk;
    

    // Start is called before the first frame update
    void Start()
    {
        
        //First wave
        StartCoroutine(SpawnEnemiesAfterDelay(3f,0,1));
        StartCoroutine(SpawnEnemiesAfterDelay(6f,0,1));
        StartCoroutine(SpawnEnemiesAfterDelay(9f,0,1));
        StartCoroutine(SpawnEnemiesAfterDelay(14f,1,1));
        StartCoroutine(SpawnEnemiesAfterDelay(18f,1,1));
        StartCoroutine(SpawnEnemiesAfterDelay(23f,1,1));
        
        //Second wave
        StartCoroutine(SpawnEnemiesAfterDelay(33f,0,1));
        StartCoroutine(SpawnEnemiesAfterDelay(36f,0,1));
        StartCoroutine(SpawnEnemiesAfterDelay(41f,1,1));
        StartCoroutine(SpawnEnemiesAfterDelay(46f,1,1));
        StartCoroutine(SpawnEnemiesAfterDelay(53f,2,1));
        
        //Third wave
        StartCoroutine(SpawnEnemiesAfterDelay(63f, 0, 3));
        StartCoroutine(SpawnEnemiesAfterDelay(68f, 1, 2));
        StartCoroutine(SpawnEnemiesAfterDelay(73f, 1, 1));
        StartCoroutine(SpawnEnemiesAfterDelay(78f, 1, 1));
        StartCoroutine(SpawnEnemiesAfterDelay(79f, 1, 1));
        StartCoroutine(SpawnEnemiesAfterDelay(83f, 2, 1));
        
        //Fourth wave
        StartCoroutine(SpawnEnemiesAfterDelay(93f, 0, 1));
        StartCoroutine(SpawnEnemiesAfterDelay(98f, 1, 1));
        StartCoroutine(SpawnEnemiesAfterDelay(103f, 1, 1));
        StartCoroutine(SpawnEnemiesAfterDelay(108f, 2, 1));
        StartCoroutine(SpawnEnemiesAfterDelay(113f, 2, 1));
        
        //Fifth wave
        StartCoroutine(SpawnEnemiesAfterDelay(123f, 1, 1));
        StartCoroutine(SpawnEnemiesAfterDelay(128f, 1, 1));
        StartCoroutine(SpawnEnemiesAfterDelay(133f, 1, 1));
        StartCoroutine(SpawnEnemiesAfterDelay(138f, 2, 1));
        StartCoroutine(SpawnEnemiesAfterDelay(143f, 2, 1));

        //Finish
        StartCoroutine(SpawnEnemiesAfterDelay(163f, 0, 0));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*This method is called anywhere if we need to change our Scene. Recieves a String parameter and calls the SceneManager
     * to search and load the Scene corresponding to that String name.
    */
    public static void changeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    // This method is called when our nexus reaches 0 health. Swaps our scene to our endGame scene 'Lose'
    public void endGame()
    {
        changeScene("Lose");
    }

    public void winGame()
    {
        changeScene("Win");
    }

    /*
        public void levelUp()
        {
            //To be used in endless mode
        }
    */

    //This method handles the audio. Recieves an AudioSource parameter to choose which clip is going to be played.
    //Checks whether is null or is playing to be sure, then it plays the AudioClip
    public void PlayAudioSource(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    /*
     This method handles our spawnTimers. Recieves 3 parameters:
    - Delay will be the time away from the start
    - Type is the type of enemy this method will create
    - Amount will be the amount of those enemies we are going to create
    We consider every group of enemies a wave, and before the first enemy of each group is spawned, we increase our wave counter,
    then calls 'SpawnEnemy' to instantiate our enemies after the delay.
     */
    IEnumerator SpawnEnemiesAfterDelay(float delay,int type,int amount)
    {
        yield return new WaitForSeconds(delay);

        if (delay == 33f || delay == 63f || delay == 93f || delay == 123f)
        {
            wave++;
        }
        // Example: Spawn the first enemy type 3 times
        SpawnEnemy(orkPrefabs[type], amount);
        if(delay == 163f && orks.Count <= 0)
        {
            winGame();
        }
        
    }
    /*
     * This handles the enemies spawn logic. Recieves 2 parameters:
     * - enemyPrefab will be the type of GameObject we are going to instantiate
     * - count will be the amount we are going to instantiate
     * This method will instantiate a prefab of the type we have chose as many times as the count parameter value.
     * The transform and quaternion will be our GameManager's position and rotation.
    */
    void SpawnEnemy(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            orks.Add(Instantiate(enemyPrefab, transform.position, Quaternion.identity));
        }
    }
}

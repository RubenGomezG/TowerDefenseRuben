using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Button continueGame;
    public Button startGame;

    // Start is called before the first frame update
    void Start()
    {
        disableButton(continueGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void disableButton(Button button)
    {
        button.gameObject.SetActive(false);
    }

    void enableButton(Button button)
    {
        button.gameObject.SetActive(true);
    }
}

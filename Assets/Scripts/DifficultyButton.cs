using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Button

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficulty()
    {
        Debug.Log(gameObject.name + " was clicked");
        if (gameObject.name == "Hard")
        {
            difficulty = 3;
        }
        else if (gameObject.name == "Medium")
        {
            difficulty = 2;
        }
        else
        {
            difficulty = 1;
        }
        gameManager.StartGame(difficulty);
    }
}

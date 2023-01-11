using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DificultButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficulty()
    {
        Debug.Log(button.gameObject.name + "was clicked");
        gameManager.StartGame(difficulty);
    }
}

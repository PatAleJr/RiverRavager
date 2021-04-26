using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gc;

    private void Awake()
    {
        if (gc != null && gc != this)
        {
            Destroy(gc);
        }

        gc = this;
    }

    public Animator canvasAnim;

    public enum State {Title, Playing, GameOver};
    public State currentState;

    public EnemySpawner[] spawners;

    public GameObject player;
    private PlayerHealth playerHealth;
    public Transform playerResetPos;

    void Start()
    {
        currentState = State.Title;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (currentState == State.Title)
            {
                startPlaying();
            }
            else if (currentState == State.GameOver) {
                reset();
            }
        }

    }

    void startPlaying()
    {
        currentState = State.Playing;
        Score.scoreScript.startPlaying();
        canvasAnim.SetTrigger("Playing");

        foreach (EnemySpawner spawner in spawners)
            spawner.startPlaying();
    }

    public void gameOver()
    {
        currentState = State.GameOver;
        Score.scoreScript.gameOver();
        canvasAnim.SetTrigger("GameOver");

        foreach (EnemySpawner spawner in spawners)
            spawner.gameOver();
    }

    void reset()
    {
        currentState = State.Title;
        Score.scoreScript.reset();
        canvasAnim.SetTrigger("Reset");
        playerHealth.reset();
    }
}

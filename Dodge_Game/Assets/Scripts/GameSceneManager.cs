using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    private Player player;
    private EnemySpawner spawner;
    private UI_Time timer;
    public GameObject gameOverUI;
    public GameObject background;
    private SpriteRenderer backgroundRenderer;
    
    // Start is called before the first frame update
    void Awake()
    {
        timer = FindObjectOfType<UI_Time>();
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<EnemySpawner>();
        backgroundRenderer = background.GetComponent<SpriteRenderer>();
        BackgroundChange();
    }

    void BackgroundChange()
    {
        backgroundRenderer.color = new Color(
            1 - Random.value * 0.3f, 
            1 - Random.value * 0.3f, 
            1 - Random.value * 0.3f);
        if(player.GetHP() > 0)
            Invoke("BackgroundChange", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetHP() <= 0)
        {
            gameOverUI.SetActive(true);
            spawner.OffSpawner();
            timer.OffTimer();
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

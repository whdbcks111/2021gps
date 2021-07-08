using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public Text escHelp;
    public Text soundSourceText;
    private float stayEsc = 0f;
    private float staySourceColor = 0f;
    private int lastColor = 0;
    private Color[] sourceColors = { Color.magenta, Color.green, Color.yellow, Color.cyan };
    private bool isEscBlocked = false;
    private int escCount = 0;
    
    private void Update()
    {
        if (!isEscBlocked)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                escHelp.text = "계속 누르고 있으면 게임이 종료됩니다...";
                stayEsc += Time.deltaTime;
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
            {
                stayEsc = 0;
                escHelp.text = "";
                escCount++;
            }

            if (stayEsc >= 1)
            {
                ExitGame();
            }
            
            if (escCount >= 30)
            {
                isEscBlocked = true;
                escHelp.text = "화려한 손놀림으로 인해 ESC 버튼이 금지되었습니다.";
                escHelp.color = Color.red;
            }
        }

        staySourceColor += Time.deltaTime;
        if (staySourceColor >= 0.5f)
        {
            staySourceColor -= 0.5f;
            lastColor++;
            if (lastColor >= sourceColors.Length) lastColor = 0;
            soundSourceText.color = sourceColors[lastColor];
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

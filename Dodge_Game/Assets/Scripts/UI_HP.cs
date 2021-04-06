using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    private Text hpText;
    private Player player;

    private int playerHp;
    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<Player>();
        hpText = GetComponent<Text>();
        playerHp = player.GetHP();
        updateUIText();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetHP() != playerHp)
        {
            playerHp = player.GetHP();
            updateUIText();
        }
    }

    void updateUIText()
    {
        hpText.text = "HP : " + player.GetHP();
    }
}

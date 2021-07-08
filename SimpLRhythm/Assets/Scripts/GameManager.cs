using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Image screenBlur;
    public Text rankText;
    public Text endScoreText;
    public Text escHelpText;
    public Text scoreText;
    public static GameManager instance;
    public Text hitGrade;
    public Text spaceHelpText;
    public Text songName;
    private AudioSource audioSource;
    public string songNameStr;
    public GameObject noteGroup;
    public SpriteRenderer hitter;
    public NoteMove notePrefab;
    
    public AudioClip song;
    public AudioClip hitSound;

    private bool isEnded = false;
    private float latestShowed = 0;
    private float playTime = 0;
    public string noteStr = "";
    private float[] notes;
    private static float offset = 0.05f;
    public static float noteMoveTime = 1.5f;
    public static float noteDistance;
    private int curNote = 0;

    public int score = 0;
    private bool isStarted = false;
    private float changeColor = 0f;

    public static float screenPixelWidth;
    public static float screenPixelHeight;

    void Start()
    {
        instance = this;
        screenBlur.gameObject.SetActive(false);
        HideGrade();
        string[] noteStrList = noteStr.Split(new char[]{' '});
        notes = new float[noteStrList.Length];
        int i = 0;
        foreach (string s in noteStrList)
        {
            float result;
            if (float.TryParse(s, out result))
            {
                notes[i++] = result;
            }
        }
        
        audioSource = GetComponent<AudioSource>();
        screenPixelWidth = Camera.main.pixelWidth;
        screenPixelHeight = Camera.main.pixelHeight;
        noteDistance = Camera.main.ScreenToWorldPoint(new Vector3(screenPixelWidth * 0.8f, 0, 0)).x 
                       - Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        Vector3 hpos = hitter.gameObject.transform.position;
        hpos.x = Camera.main.ScreenToWorldPoint(new Vector3(screenPixelWidth * 0.8f, screenPixelHeight * 0.5f, 0)).x;
        hitter.gameObject.transform.position = hpos;
        hitGrade.transform.position = new Vector3(screenPixelWidth * 0.8f, screenPixelHeight * 0.7f);
        
        Vector3 snpos = songName.transform.position;
        Vector3 shpos = spaceHelpText.transform.position;
        snpos.y = screenPixelHeight * 0.75f;
        shpos.y = screenPixelHeight * 0.68f;
        songName.transform.position = snpos;
        spaceHelpText.transform.position = shpos;
        songName.text = songNameStr;

        hitter.color = new Color(1, 1, 1, 0.1f);
    }

    private void Update()
    {
        if (isEnded)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("MainScene");
            }
            return;
        }
        scoreText.text = "SCORE  " + score;
        if (!isStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isStarted = true;
                Invoke("PlaySong", offset + 1);
                SoundHit();
                spaceHelpText.color = new Color(1, 1, 1, 0);
            }
            return;
        }

        if (playTime > notes[notes.Length - 1] + 1f)
        {
            End();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            End();
            return;
        }


        if (playTime <= 1)
        {
            songName.color = new Color(1,1,1,1-playTime);
        }
        else if (songName.color.a > 0) songName.color = new Color(1,1,1,0);

        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
        {
            noteStr += playTime + " ";
            SoundHit();
            ShowGrade();
        }

        if (curNote < notes.Length && playTime + noteMoveTime >= notes[curNote])
        {
            RunNextNote();
        }

        if (changeColor >= 0)
        {
            changeColor -= Time.deltaTime / 0.3f;
            changeColor = Mathf.Clamp(changeColor, 0, 1);
            hitter.color = new Color(0 * changeColor + 1 * (1-changeColor),
                1 * changeColor + 1 * (1-changeColor), 
                1 * changeColor + 1 * (1-changeColor), 
                0.5f * changeColor + 0.1f * (1-changeColor));

        }

        if (playTime - latestShowed > 0.3f && hitGrade.color.a > 0)
        {
            HideGrade();
        }
        
        playTime += Time.deltaTime;
    }

    void End()
    {
        audioSource.Stop();
        char rank;
        float scoreRatio = score / (500.0f * notes.Length);
        if (scoreRatio > 0.9)
        {
            rankText.color = Color.yellow;
            rank = 'S';
        }
        else if (scoreRatio > 0.8)
        {
            rankText.color = Color.red;
            rank = 'A';
        }
        else if (scoreRatio > 0.6f)
        {
            rankText.color = new Color(1, 0.4f, 0);
            rank = 'B';
        }
        else if (scoreRatio > 0.5f)
        {
            rankText.color = new Color(0.3f, 0.3f, 0.8f);
            rank = 'C';
        }
        else if (scoreRatio > 0.4f)
        {
            rankText.color = new Color(0.7f, 0, 0.7f);
            rank = 'D';
        }
        else if (scoreRatio > 0.2f)
        {
            rankText.color = new Color(0.9f, 0.9f, 0.9f);
            rank = 'E';
        }
        else
        {
            rankText.color = new Color(0.6f, 0.6f, 0.6f);
            rank = 'F';
        }
        endScoreText.text = "SCORE  " + score;
        rankText.text = "RANK  " + rank;
        screenBlur.gameObject.SetActive(true);
        isEnded = true;
    }

    void HideGrade()
    {
        hitGrade.color = new Color(0, 0, 0, 0);
    }


    public void ShowMiss()
    {
        hitGrade.color = new Color(0.7f, 0.7f, 0.7f, 1);
        hitGrade.text = "놓침!";
        score = Mathf.Max(score - 500, 0);
        latestShowed = playTime;
    }

    void ShowGrade()
    {
        NoteMove note = null;
        for (int i = 0; i < noteGroup.transform.childCount; i++)
        {
            NoteMove n =
                noteGroup.transform.GetChild(i).GetComponent<NoteMove>();
            print(n.check + " " + n.isInHitter);
            if (!n.check && n.isInHitter)
            {
                note = n;
                break;
            }
        }
        if(note == null) return;
        note.Remove();
        float diff = note.time - playTime;
        if (diff < -noteMoveTime * 0.05f)
        {
            hitGrade.color = new Color(1, 0.3f, 0.3f, 1);
            hitGrade.text = "느림!";
        }
        else if (diff > noteMoveTime * 0.05f)
        {
            hitGrade.color = new Color(1, 0.3f, 0.3f, 1);
            hitGrade.text = "빠름!";
        }
        else
        {
            hitGrade.color = Color.green;
            hitGrade.text = "정확!";
        }
        latestShowed = playTime;
        score += Math.Max(500 - Mathf.Abs((int)(diff * 2500)), 100);
    }

    void SoundHit()
    {
        audioSource.PlayOneShot(hitSound, 10f);
        hitter.color = new Color(0, 1, 1, 0.5f);
        changeColor = 1f;
    }

    void PlaySong()
    {
        audioSource.clip = song;
        audioSource.Play();
    }

    void RunNextNote()
    {
        if(curNote >= notes.Length) return;
        curNote++;
        NoteMove note = Instantiate(notePrefab);
        float r, g, b;
        do
        {
            r = Random.value;
            g = Random.value;
            b = Random.value;
        } while (r + g + b < 1f);
        note.GetComponent<SpriteRenderer>().color = new Color(r, g, b);
        note.transform.parent = noteGroup.transform;
        note.time = notes[curNote - 1];
    }
}

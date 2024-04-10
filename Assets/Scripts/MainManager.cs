using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text RecordText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    public int m_Points;
    public int record;
    public string recordHolder;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        if (GameManager.Instance.record == 0)
        {
            record = 0;
        }
        else
        {
            record = GameManager.Instance.record;
            ReloadRecord();
        }
 
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Load menu screen instead of reloading active scene
                SceneManager.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        SetRecord();
    }

    public void SetRecord()
    {
        if (m_Points > record)
        {
            record = m_Points;

            recordHolder = GameManager.Instance.username;
            GameManager.Instance.recordHolder = recordHolder;
            GameManager.Instance.record = record;

            RecordText.text = $"Best Score : {recordHolder} : {record}";
        }
    }
    
    public void ReloadRecord()
    {
        recordHolder = GameManager.Instance.recordHolder;
        record = GameManager.Instance.record;

        RecordText.text = $"Best Score : {recordHolder} : {record}";
    }
}

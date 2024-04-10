using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIOptions : MonoBehaviour
{
    public TextMeshProUGUI BestScore;
    public TMP_InputField usernameField;
 
    // Start is called before the first frame update
    public void Start()
    {
        if (GameManager.Instance.record != 0)
        {
            BestScore.text = $"Best Score: {GameManager.Instance.recordHolder} : {GameManager.Instance.record}";
        }
        if (GameManager.Instance.username != null)
        {
            usernameField.text = GameManager.Instance.username;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        GameManager.Instance.SaveData();
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

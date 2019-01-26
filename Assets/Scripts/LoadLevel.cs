using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

    public Button LoadButton;

    public UnityAction TaskOnClick { get; private set; }

    void Start () {
		//Button btn = LoadButton.GetComponent<Button>();
        LoadButton.onClick.AddListener(() => QuitOnClick());
    }

    public void QuitOnClick()
    {
        Application.Quit(); //здесь пытаюсь закрыть сцену   
        LoadGame();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Scenes/meny");
    }
}

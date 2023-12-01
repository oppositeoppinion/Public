using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Button _button;
    void Start()
    {
        _button=GetComponent<Button>();
        _button.onClick.AddListener(Click);

    }

   
    public void Click()
    {
        Debug.Log($"asdsad");
        SceneManager.LoadScene("02_GameScene");
    }
    



}

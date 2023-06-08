using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Play : MonoBehaviour
{
    // Start is called before the first frame update
    public BoardSetter setter;
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Easy()
    {
        setter.Easy();
    }
    public void Medium()
    {
        setter.Medium();
    }
    public void Hard()
    {
        setter.Hard();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BoardSetter : MonoBehaviour
{
    public BoardManager board;
    public int width;
    public int height;
    public int mines;
    void Start()
    {
        //DontDestroyOnLoad (transform.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            board = FindObjectOfType<BoardManager>();
            board.boardWidth = width;
            board.boardHeight = height;
            board.totalMines = mines;
        }
        if (scene.name == "Main Menu")
        {
            board = null;
            width = 8;
            height = 8;
            mines = 10;
        }
    }
    public void Easy()
    {
        width = 8;
        height = 8;
        mines = 10;
    }
    public void Medium()
    {
        width = 16;
        height = 16;
        mines = 40;
    }
    public void Hard()
    {
        width = 30;
        height = 16;
        mines = 99;
    }
    public void SetWidth(string w)
    {
        int.TryParse(w, out width);
    }
    public void SetHeight(string h)
    {
        int.TryParse(h, out height);
    }
    public void SetMines(string m)
    {
        int.TryParse(m, out mines);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class BoardManager : MonoBehaviour
{
    public Transform canvas;
    public GameObject tilePrefab;
	public Text minesText;
	public Text timeText;
	float time;
    public int boardWidth;
    public int boardHeight;
    int currentMines;
	int count;
    public int totalMines;
    public int foundMines;
    public TileBehavior[,] board;
    public List<TileBehavior> spawnList;
	// Start is called before the first frame update
    void Start()
    {
        if(boardWidth < 1){
            boardWidth = 1;
        }
        if(boardHeight < 1){
            boardHeight = 1;
        }
        if(totalMines < 1){
            totalMines = 1;
        }
        if(totalMines > boardHeight*boardWidth){
            totalMines = boardHeight*boardWidth;
        }
        board = new TileBehavior[boardWidth, boardHeight];
		SetBoard(boardWidth, boardHeight);
    }
	public void SetBoard(int width, int height)
    {
		for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            { 
                Vector3 spawnpos = new Vector3((j-width/2f)+0.5f, (i-height/2f)-height/15.9f+0.5f, 0);
                board[j, i] = Instantiate(tilePrefab, spawnpos, Quaternion.Euler(Vector3.up * 0), canvas).GetComponent<TileBehavior>();
                spawnList.Add(board[j, i]);
                board[j, i].xpos = j;
                board[j, i].ypos = i;
            }
        }
        for (int i = 0; i < totalMines; i++)
        {
            if(spawnList.Count == 0)
            {
                break;
            }
            int x = UnityEngine.Random.Range(0, spawnList.Count - 1);
            spawnList[x].isMine = true;
            spawnList.RemoveAt(x);
        }
	}
    IEnumerator Win()
    {
		yield return new WaitForSeconds(1f);
        foreach (TileBehavior tile in board)
        {
            tile.sprite.color = Color.white;
			tile.sprite.sprite = tile.flagSprite;
			tile.text.text = "";
			yield return null;
        }
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public IEnumerator Lose()
    {
        foreach (TileBehavior tile in board)
        {
            if (tile.isMine)
            {
                tile.marked = false;
                tile.Reveal();
				yield return null;
            }
        }
		yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
    void Update()
	{
		time += Time.deltaTime;
		minesText.text = "" + (totalMines-foundMines);
		timeText.text = "" + Mathf.FloorToInt(time);
		if(foundMines == totalMines){
			currentMines = 0;
			count = 0;
            foreach (TileBehavior tile in board)
			{
				if(tile.isMine && tile.marked){
					currentMines++;
				}
				if(!tile.isMine && tile.revealed){
					count++;
				}
			}
			if(currentMines + count == boardHeight*boardWidth){
				StartCoroutine(Win());
			}
		}
    }
}
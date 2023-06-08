using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TileBehavior : MonoBehaviour, IPointerClickHandler
{
	public Sprite mineSprite;
	public Sprite flagSprite;
	public Sprite normalSprite;
	public Image sprite;
	public Text text;
	public BoardManager boardManager;
	public bool isMine;
	int adjacent;
	public int xpos;
	public int ypos;
	public bool marked;
	public bool revealed;
	// Start is called before the first frame update
	void Start()
    {
        boardManager = FindObjectOfType<BoardManager>();
		FindAdjacent();
		sprite = GetComponent<Image>();
	}
	public void FindAdjacent()
    {
		if (isMine)
        {
			adjacent = -1;
        }
		else
		{
			for(int i = ypos - 1; i <= ypos+1; i++)
			{
				for(int j = xpos - 1; j <= xpos+1; j++)
				{ 
					if(i >= 0 && j >= 0 && i < boardManager.boardHeight && j < boardManager.boardWidth){
						if(boardManager.board[j, i].isMine){
							adjacent++;
						}
					}
				}
			}
		}
	}
	public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left){
            Reveal();
		}
        else if (eventData.button == PointerEventData.InputButton.Right){
            Mark();
		}
		else if (eventData.button == PointerEventData.InputButton.Middle){
			GroupReveal();
		}
    }
	public void GroupReveal(){
		int count = 0;
		for(int i = ypos - 1; i <= ypos+1; i++)
			{
				for(int j = xpos - 1; j <= xpos+1; j++)
				{ 
					if(i >= 0 && j >= 0 && i < boardManager.boardHeight && j < boardManager.boardWidth){
						if(boardManager.board[j, i].marked){
							count++;
						}
					}
				}
			}
		if(revealed && count == adjacent){
			for(int i = ypos - 1; i <= ypos+1; i++)
			{
				for(int j = xpos - 1; j <= xpos+1; j++)
				{ 
					if(i >= 0 && j >= 0 && i < boardManager.boardHeight && j < boardManager.boardWidth){
						boardManager.board[j, i].Reveal();
					}
				}
			}
		}
	}
	public void Reveal(){
		if(!marked && !revealed){
			revealed = true;
			sprite.color = Color.white;
			if (isMine){
				sprite.sprite = mineSprite;
				StartCoroutine(boardManager.Lose());
			}
			else{
				text.text = "" + adjacent;
				if(adjacent == 0)
				{
					text.text = "";
					for(int i = ypos - 1; i <= ypos+1; i++)
					{
							for(int j = xpos - 1; j <= xpos+1; j++)
						{ 
							if(i >= 0 && j >= 0 && i < boardManager.boardHeight && j < boardManager.boardWidth){
								boardManager.board[j, i].Reveal();
							}
						}
					}
				}
			
			}
		}
	}
	public void Mark(){
		if (!revealed)
		{
			if (marked)
			{
				boardManager.foundMines--;
				sprite.sprite = normalSprite;
			}
			else
			{
				boardManager.foundMines++;
				sprite.sprite = flagSprite;
			}
			marked = !marked;
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TileBehaviorOld : MonoBehaviour, IPointerClickHandler
{
	public Sprite mineSprite;
	public Sprite flagSprite;
	public Sprite normalSprite;
	public Image sprite;
	public Text text;
	public BoardManager boardManager;
	public bool isMine;
	int adjacent;
	public bool marked;
	public bool revealed;
	TileBehavior[] board;
	// Start is called before the first frame update
	void Start()
    {
        boardManager = FindObjectOfType<BoardManager>();
		FindAdjacent();
		sprite = GetComponent<Image>();
	}
	public void FindAdjacent()
    {
		foreach (TileBehavior tile in boardManager.board)
        {
			if(Vector3.Distance(tile.transform.position, transform.position) < 1.5f && tile.isMine)
            {
				adjacent++;
            }
        }
        if (isMine)
        {
			adjacent = -1;
        }
	}
	//OnPointerClick isn't called from inside my code, it's called through Unity's event system.
	//OnPointerClick triggers when the player clicks one of the tiles, calling Reveal on a left click and Mark on a right click.
	public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            Reveal();
        else if (eventData.button == PointerEventData.InputButton.Right)
            Mark();
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
					foreach (TileBehavior tile in boardManager.board)
					{
						if (Vector3.Distance(tile.transform.position, transform.position) < 1.5f)
						{
							tile.Reveal();
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
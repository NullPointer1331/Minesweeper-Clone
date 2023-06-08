using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    public BoardManager boardManager;
    // Start is called before the first frame update
    void Start()
    {
        camera.orthographicSize = Mathf.Max((boardManager.boardHeight) / 1.75f, boardManager.boardWidth / 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

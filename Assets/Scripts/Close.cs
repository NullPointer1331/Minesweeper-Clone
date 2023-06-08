using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    public GameObject thing;
    public void exit()
    {
        thing.SetActive(false);
    }
}

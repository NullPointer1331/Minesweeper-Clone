using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    public GameObject thing;
    public void enter()
    {
        thing.SetActive(true);
    }
}

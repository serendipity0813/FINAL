using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public GameObject[] MiniGames;
    // Start is called before the first frame update
    private void Start()
    {
        Instantiate(MiniGames[0]);
    }

}

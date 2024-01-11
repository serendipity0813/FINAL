using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ThiefCaveManager : MonoBehaviour
{
    public static ThiefCaveManager Instance;
    private int stage = 3;
    public Vector3[] m_hidePosition { get; private set; }
    public GameObject Thief;
    public GameObject Cave;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    private void Start()
    {
        m_hidePosition = new Vector3[stage * 2 + 1];
        //랜덤한 위치에 도둑이 숨을 공간 생성
        for (int i = 0; i < stage * 2 + 1; i++)
        {
            m_hidePosition[i] = new Vector3((int)Random.Range(-5, 5) * 2, 0, (int)Random.Range(-2, 5) * 2);
            Cave.transform.position = m_hidePosition[i];
            Thief.transform.position = m_hidePosition[i];
            Instantiate(Cave);
            Instantiate(Thief);
        }


    }

}

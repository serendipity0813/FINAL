using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ThiefCaveManager : MonoBehaviour
{
    public static ThiefCaveManager Instance;
    private int stage = 3;
    private float m_timer;
    public Vector3[] m_hidePosition { get; private set; }
    public bool IsGaiming;
    public GameObject Target;
    public GameObject Thief;
    public GameObject Cave;
    public GameObject Mission;
    public GameObject Clear;
    public GameObject Fail;

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
        IsGaiming = false;
        m_hidePosition = new Vector3[stage * 2 + 1];
        //·£´ýÇÑ À§Ä¡¿¡ µµµÏÀÌ ¼ûÀ» °ø°£ »ý¼º
        for (int i = 0; i < stage * 2 ; i++)
        {
            m_hidePosition[i] = new Vector3((int)Random.Range(-3, 3) * 2, 0, (int)Random.Range(-1, 4) * 2);
            if(i == 0)
            {
                Target.transform.position = m_hidePosition[i];
                Instantiate(Target);
            }
            Cave.transform.position = m_hidePosition[i];
            Thief.transform.position = m_hidePosition[i];
            Instantiate(Cave);
            Instantiate(Thief);
        }

        Mission.SetActive(true);
    }

    private void Update()
    {
        m_timer += Time.deltaTime;
        if(m_timer > 1)
        {
            Mission.SetActive(false);
        }
        if(m_timer > 2)
        {
            IsGaiming = true;
        }

        if(m_timer > 9)
        {
            IsGaiming = false;
        }
    }



}

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
    public bool IsGaiming { get; private set; }
    public bool IsChanging { get; private set; }
    private bool m_clear;
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
        m_clear = false;
        IsGaiming = false;
        IsChanging = true;
        m_hidePosition = new Vector3[stage * 2 + 1];
        //·£´ýÇÑ À§Ä¡¿¡ µµµÏÀÌ ¼ûÀ» °ø°£ »ý¼º
        for (int i = 0; i <= stage * 2 ; i++)
        {
            m_hidePosition[i] = new Vector3((int)Random.Range(-2, 3) * 2, 0, (int)Random.Range(-2, 5) * 2);
            if(i == 0)
            {
                Target.transform.position = m_hidePosition[i];
                Instantiate(Target);
            }
            Cave.transform.position = m_hidePosition[i];
            Instantiate(Cave);

            if(i%2 == 1)
            {
                Thief.transform.position = m_hidePosition[i];
                Instantiate(Thief);
            }
        }

    }

    private void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > 0.5 && Mission.activeSelf == false)
        {
            Mission.SetActive(true);
        }
        if (m_timer > 1.5 && Mission.activeSelf == true)
        {
            Mission.SetActive(false);
        }
        if(m_timer > 2)
        {
            IsGaiming = true;
        }
        if (m_timer > 7)
        {
            IsChanging = false;
        }
        if (m_timer > 9)
        {
            IsGaiming = false;

            if(Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Target")
                    {
                        Clear.SetActive(true);
                    }
                    else
                    {
                        Fail.SetActive(true);
                    }
                }
            }

        }

        if (m_timer > 15 && m_clear == false)
        {
            Fail.SetActive(true);
        }

    }


}

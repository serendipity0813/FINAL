using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEat : MonoBehaviour
{
    [SerializeField] private int m_level; // 현재 미니게임 난이도
    public int m_winCount; // 승리 카운트 선언
    public int m_repetition; // 과일을 몇번 뿌릴지
    private float m_timer;

    private void Awake()
    {
        m_repetition = 10;
        m_winCount = m_repetition;
    }
}

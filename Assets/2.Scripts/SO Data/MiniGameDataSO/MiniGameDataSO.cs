using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MiniGameDataSO", menuName = "MiniGame/MiniGameDataSO", order = 0)]
public class MiniGameDataSO : ScriptableObject
{
    [Serializable] // 직렬화 (byte)
    public struct Games
    {
        [Header("GameData")]
        public string gameName; // 게임 이름
        public int gameIndex; // 게임 인덱스
        public GameObject gamePrefab; // 게임 프리팹
        public GameObject gameIcon; // 게임 대표 아이콘

        [TextArea]
        public string gameDescription; // 게임 설명
    }
    public List<Games> games = new List<Games>();
}

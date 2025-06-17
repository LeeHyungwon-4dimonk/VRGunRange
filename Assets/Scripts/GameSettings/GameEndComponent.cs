using UnityEngine;

/// <summary>
/// 게임 종료용 스크립트
/// </summary>
public class GameEndComponent : MonoBehaviour
{
    public void GameEnd()
    {
        Application.Quit();
    }
}
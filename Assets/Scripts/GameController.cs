using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : ASingleton<GameController>
{
    public UIMainInterfaceView MainInterfaceView;

    private const string GAMEPLAY_SCENE_NAME = "Gameplay";

    private SceneLoader m_SceneLoader;

    private ulong m_ShieldCost = 5;

    private ulong m_Points = 0;

    private bool m_ShieldActive = false;

    private float m_ShieldDuration = 5.0f;

    private float m_StartTime = 0.0f;

    private float m_ShieldStartTime = -10.0f;

    public void ResetPoints()
    {
        m_Points = 0;
    }

    public void ResetStartTime()
    {
        m_StartTime = Time.time;
    }

    public void ResetShield()
    {
        m_ShieldActive = false;
        m_ShieldStartTime = -10.0f;
    }

    public void HandleCoinPickedUp(Coin coin)
    {
        if (coin == null)
        {
            return;
        }

        m_Points += coin.Points;
    }

    public void HandleShieldUsed()
    {
        if (m_Points >= m_ShieldCost)
        {
            m_Points -= m_ShieldCost;
            m_ShieldStartTime = Time.time;
            m_ShieldActive = true;
        }
    }

    public bool IsShieldActive()
    {
        return m_ShieldActive;
    }

    protected override void Initialize()
    {
        m_SceneLoader = new SceneLoader();
        m_SceneLoader.LoadScene(GAMEPLAY_SCENE_NAME, HandleSceneLoaded);

        MainInterfaceView.Initialize(Pause);
    }

    private void HandleSceneLoaded(Scene loadedScene)
    {
        Debug.LogError($"Loaded scene {loadedScene.name}");
    }

    private bool m_Paused = false;

    private void Update()
    {
        m_ShieldActive = Time.time - m_ShieldStartTime <= m_ShieldDuration;
        MainInterfaceView.Configure(m_Points, Time.time - m_StartTime, m_ShieldActive ? m_ShieldDuration - (Time.time - m_ShieldStartTime) : 0.0f);
    }

    private void Pause()
    {
        m_Paused = !m_Paused;
        Time.timeScale = m_Paused ? 0f : 1f;
    }
}

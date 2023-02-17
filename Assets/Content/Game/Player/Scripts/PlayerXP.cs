using Content.Game.UI.Scripts;
using UnityEngine;
using Zenject;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private float _xpPointsToLevelUp = 10;
    [SerializeField] private int _xpPointsToLevelUpIncreasePerLevel = 5;

    private int _level = 0;
    private float _currentXp;
    private UIManager _uiManager;
    private SignalBus _signalBus;
    
    [Inject]
    public void Construct(UIManager uiManager, SignalBus signalBus)
    {
        _uiManager = uiManager;
        _signalBus = signalBus;
    }

    public void TakeXp(float xpCollected)
    {
        _currentXp += xpCollected;
        _uiManager.XPManager.IncreaseXPView(xpCollected/_xpPointsToLevelUp);

        if (!(_currentXp >= _xpPointsToLevelUp)) return;
        _signalBus.Fire<LevelUpSignal>();
        _level += 1;
        _currentXp -= _xpPointsToLevelUp;
        _xpPointsToLevelUp += _xpPointsToLevelUpIncreasePerLevel;
        _uiManager.XPManager.SetXPPercentage(_currentXp/_xpPointsToLevelUp);
    }
}
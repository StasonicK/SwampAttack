using TMPro;
using UnityEngine;

public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _money.text = _player.Money.ToString();
        _player.MoneyChanged += MoneyChanged;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= MoneyChanged;
    }

    private void MoneyChanged(int money)
    {
        _money.text = money.ToString();
    }
}
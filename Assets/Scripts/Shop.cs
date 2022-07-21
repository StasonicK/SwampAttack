using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _template;
    [SerializeField] private GameObject _itemContainer;

    private void Start()
    {
        for (int i = 0; i < _player.Weapons.Count; i++)
        {
            AddItem(_player.Weapons[i]);
        }
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClicked += OnSellButtonClick;
        view.Render(weapon);
    }

    private void OnSellButtonClick(Weapon weapon, WeaponView weaponView)
    {
        TrySellWeapon(weapon, weaponView);
    }

    private void TrySellWeapon(Weapon weapon, WeaponView view)
    {
        if (weapon.Price <= _player.Money)
        {
            _player.BuyWeapon(weapon);
            weapon.Buy();
            view.SellButtonClicked -= OnSellButtonClick;
        }
    }
}
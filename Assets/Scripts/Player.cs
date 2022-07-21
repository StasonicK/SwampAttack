using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Transform _shootPoint;

    public readonly List<Weapon> Weapons = new();
    private List<Weapon> _availableWeapons = new();
    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private int _currentHealth;
    private Animator _animator;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
        GetWeapons();
        _availableWeapons.Add(Weapons[0]);
        ChangeWeapon(_availableWeapons[_currentWeaponNumber]);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot(_shootPoint);
        }
    }

    private void GetWeapons()
    {
        Component[] components = GetComponentsInChildren(typeof(Component));

        foreach (Component component in components)
        {
            if (component is Weapon)
            {
                Weapons.Add(component as Weapon);
            }
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
            Destroy(gameObject);
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        _availableWeapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _availableWeapons.Count - 1) _currentWeaponNumber = 0;
        else _currentWeaponNumber++;

        ChangeWeapon(_availableWeapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0) _currentWeaponNumber = _availableWeapons.Count - 1;
        else _currentWeaponNumber--;

        ChangeWeapon(_availableWeapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
}
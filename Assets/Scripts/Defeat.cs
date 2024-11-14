using UnityEngine;

public class Defeat : MonoBehaviour
{
    [SerializeField] private Ship _ship;
    [SerializeField] private Menu _menu;

    private void OnEnable() => _ship.Touched += Lose;

    private void OnDisable() => _ship.Touched -= Lose;

    private void Lose() => _menu.Lose();
}

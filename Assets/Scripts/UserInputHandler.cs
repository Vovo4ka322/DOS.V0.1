using UnityEngine;

public class UserInputHandler : MonoBehaviour
{
    [SerializeField] private Selector _selector;
    [SerializeField] private Clip _clip;
    [SerializeField] private CoreFactory _factory;
    [SerializeField] private Gun _gun;
    [SerializeField] private AttackButton _attackButton;
    [SerializeField] private Cooldown _cooldown;

    private float _timeToPressButton = 0.2f;
    private float _timeToPressCore = 0.2f;

    public int LineIndex { get; private set; }

    private void OnEnable()
    {
        _selector.CoreSelected += OnRemoved;
        _selector.ButtonSelected += OnShoot;
    }

    private void OnDisable()
    {
        _selector.CoreSelected -= OnRemoved;
        _selector.ButtonSelected -= OnShoot;
    }

    private void OnRemoved(Line line)
    {
        if (_clip.IsFullQuantity == false && _cooldown.CanUse)
        {
            LineIndex = _factory.PlacesForSeat.IndexOf(line);

            Core core = _factory.Cores[LineIndex].Dequeue();

            core.RemoveRigidbody();

            StartCoroutine(_factory.CreateOneCore(LineIndex));

            _clip.Add(core);

            StartCoroutine(_clip.FindMatch(core));

            _cooldown.LaunchTimer(_timeToPressCore);
        }
    }

    private void OnShoot()
    {
        if (_cooldown.CanUse)
        {
            _attackButton.PlayAnimation();
            _gun.Shoot();
            _cooldown.LaunchTimer(_timeToPressButton);
        }
    }
}

using UnityEngine;

/// <summary> When checking the price, to know the position of the figure </summary>
public struct Coordinates
{
    public int x;
    public int y;
}

[RequireComponent(typeof(Animator))]
public class Figure : MonoBehaviour
{
    #region Fields and properties

    // units: units/figure
    internal static readonly float FIGURE_SIZE = 2.2f;

    private static int animationIndex = 0;

    private static readonly int numberOfAnimations = 4;

    [field: SerializeField]
    public FigureType Type { get; private set; }

    private Roller _roller;

    internal Coordinates Coordinates => _coordinates;
    private Coordinates _coordinates;

    private Animator _animator;

    #endregion

    #region Unity callbacks

    void Awake()
    {
        _roller = GetComponentInParent<Roller>();
        _coordinates.x = _roller.Position;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _roller.OnRollerStopped += RollerStopped;
    }

    private void OnDestroy()
    {
        _roller.OnRollerStopped -= RollerStopped;
    }

    #endregion

    #region Methods

    /// <summary> 
    /// When the roller stops, check if I'm a visible figure and add me to check if there are prices.
    /// </summary>
    private void RollerStopped()
    {
        int pos = CheckMyPosition();
        if (pos == -1)
            return;

        _coordinates.y = pos;
        PrizeManager.Instance.AddFigureToCheck(this);
    }

    /// <summary> Check if I'm in a visible position </summary>
    /// <returns> 0/1/2 if visible, -1 if not</returns>
    private int CheckMyPosition()
    {
        int pos = Mathf.RoundToInt(transform.position.y);

        if (pos == 3)
            return 0;
        if (pos == 1)
            return 1;
        if (pos == -1)
            return 2;

        return -1;
    }

    internal void PlayPrizeAnimation(float duration)
    {
        _animator.SetInteger("index", animationIndex);
        _animator.SetFloat("vel", duration);
        _animator.SetTrigger("play");
    }

    internal static void UpdateAnimation()
    {
        animationIndex = animationIndex == numberOfAnimations - 1 ? 0 : animationIndex + 1;
    }

    #endregion
}

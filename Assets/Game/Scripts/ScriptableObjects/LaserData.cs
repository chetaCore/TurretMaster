using UnityEngine;

[CreateAssetMenu(fileName = "Lazer", menuName = "Entity/Projectile/lazer")]
public class LaserData : ProjectileData
{
    [SerializeField] private float _width;

    public float Width
    { get { return _width; } }

    [SerializeField] private float _length;

    public float Length
    { get { return _length; } }

    public LineRenderer _rendererLine;

    public LineRenderer RendererLine
    { get { return _rendererLine; } }
}
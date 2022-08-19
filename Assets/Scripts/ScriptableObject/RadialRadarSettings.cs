using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Radial Settings", menuName = "Game Settings/Radial Settings")]
public class RadialRadarSettings : ScriptableObject
{
    [Range(0, 255)]
    [SerializeField] private int opacity;
    public int Opacity { get { return opacity; } }

    [Range(1.6f, 10)]
    [SerializeField] private float radialRadarScale;
    public float RadialRadarScale { get { return radialRadarScale; } }

    [Header("Color Settings")]
    [SerializeField] private Color defaultColor;
    public Color DefaultColor { get { return defaultColor; } }

    [SerializeField] private Color detectColor;
    public Color DetectColor { get { return detectColor; } }

}

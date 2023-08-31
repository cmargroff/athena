using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
[Serializable]
public class Pan
{
    public Vector2 Coordinates;
    public float Time;

    public Ease Ease=Ease.Linear;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitter<T> where T : IHittable
{
    void DoHit(T hittable);
}

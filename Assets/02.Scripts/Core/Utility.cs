using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utility
{
    public static void FillAmount(this Image self, float current, float max)
        => self.fillAmount = current / max;


    public static Transform GetNearestTarget(this Transform self, float distance, LayerMask targetLayer)
    {
        Collider[] colliders = Physics.OverlapSphere(self.position, distance, targetLayer);
        Transform nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            float currentDistance = Vector3.Distance(self.position, collider.transform.position);
            if (currentDistance < nearestDistance)
            {
                nearestDistance = currentDistance;
                nearestTarget = collider.transform;
            }
        }

        return nearestTarget;
    }

    public static bool TargetInDistance(this Transform self, Transform target, float distance)
        => Vector3.Distance(self.position, target.position) <= distance;
}

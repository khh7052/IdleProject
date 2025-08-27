using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utility
{
    public static void FillAmount(this Image self, float current, float max)
        => self.fillAmount = current / max;
}

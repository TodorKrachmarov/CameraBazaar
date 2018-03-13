namespace CameraBazaar.Data.Models
{
    using System;

    [Flags]
    public enum LightMeteringType
    {
        Spot = 1,
        CenterWeighted = 2,
        Evaluative = 4
    }
}

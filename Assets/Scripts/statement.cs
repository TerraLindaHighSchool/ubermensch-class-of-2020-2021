using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Statement
{
    interface Statement
    {
        string opener { get; }
        string OptionOne { get; }
        float OptionOneModifier { get; }
        Statement OptionOneOutcome { get; }
    }
}


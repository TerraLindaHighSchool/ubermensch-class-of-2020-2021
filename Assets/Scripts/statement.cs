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

        string OptionTwo { get; }
        float OptionTwoModifier { get; }
        Statement OptionTwoOutcome { get; }

        string OptionThree { get; }
        float OptionThreeModifier { get; }
        Statement OptionThreeOutcome { get; }

        string OptionFour { get; }
        float OptionFourModifier { get; }
        Statement OptionFourOutcome { get; }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryItemInterface
{
    string Name { get; }
    int Value { get; }
    Sprite Icon { get; }
    string ToolTip { get; }
    bool QuestItem { get; }
    int StrengthBoost { get; }
    int CharismaBoost { get; }
    int ConstitutionBoost { get; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellContainer
{
    public bool learned = false;
    public int level;
    public BaseSpell spellReference;
}

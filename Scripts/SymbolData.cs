using UnityEngine;

[CreateAssetMenu(fileName = "Symbol", menuName = "Slot/Symbol")]
public class SymbolData : ScriptableObject
{
    public string symbolID;       // "H1", "H2", "WILD", etc.
    public Sprite symbolSprite;   // The image
    public int payoutValue;       // How much it pays (100, 80, 60, etc.)
    public bool isWild;           // Check this for WILD symbol
}
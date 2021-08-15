using UnityEngine;
using System.Collections;

/// <summary>
/// ScriptableObject za cuvanje palete boja. Paleta boja treba da zadrzi odredjeni broj nijansi te boje. Svaka boja ima svoj asset.
/// </summary>
[CreateAssetMenu (fileName = "ColorPalette" , menuName = "Webelinx/ColorPalette")]
public class PaletteAsset : ScriptableObject {

	public Color[] color;
}

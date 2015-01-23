using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Magicolo.GraphicsTools {
	[AddComponentMenu("Magicolo/Render Reorder")]
	public class RenderReorder : MonoBehaviour {

		public enum RenderTypes {
			Background = 1000,
			Geometry = 2000,
			Transparent = 3000,
			Overlay = 4000
		}
		
		[Popup("shaders")]
		public string shader;
		public string[] shaders = {"Diffuse", "Transparent/Diffuse", "Self-Illumin/Diffuse", "Sprites/Default", "Sprites/Diffuse"};
		
		public int offset;
		public RenderTypes renderType = RenderTypes.Geometry;
		public List<Material> materials = new List<Material>();
	}
}
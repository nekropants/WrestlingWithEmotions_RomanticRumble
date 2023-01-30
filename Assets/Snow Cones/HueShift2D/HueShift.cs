using UnityEngine;

namespace HueShift2D
{
    [ExecuteInEditMode]
    public class HueShift : MonoBehaviour
    {
        public float Shift;

        public float Saturation;

        public float LowerLimit;

        public float UpperLimit;

        public bool Inverted;

        private Renderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        public void Update()
        {
            _renderer = GetComponent<Renderer>();
            if (!_renderer.isVisible)
                return;
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetFloat("_Inverted", Inverted ? 1 : -1);
            materialPropertyBlock.SetFloat("_LimitU", UpperLimit);
            materialPropertyBlock.SetFloat("_LimitL", LowerLimit);
            if (_renderer.sharedMaterial.HasProperty("_Saturation"))
                materialPropertyBlock.SetFloat("_Saturation", Saturation);
            materialPropertyBlock.SetFloat("_Shift", Shift);
            _renderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}
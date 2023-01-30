using UnityEngine;

namespace Battlehub.SplineEditor
{
    [ExecuteInEditMode]
    public class SplineControlPoint : MonoBehaviour
    {

        [SerializeField, HideInInspector]
        private int m_index;

#if UNITY_EDITOR
        private Vector3 m_localPosition;
#endif

        public int Index
        {
            get { return m_index; }
            set { m_index = value; }
        }

        private SplineBase m_spline;
        private void OnEnable()
        {
            m_spline = GetComponentInParent<SplineBase>();
            if (m_spline == null)
            {
                return;
            }

            m_spline.ControlPointChanged -= OnControlPointChanged;
            m_spline.ControlPointChanged += OnControlPointChanged;
        }

        private void Start()
        {
            if (m_spline == null)
            {
                m_spline = GetComponentInParent<SplineBase>();
                if (m_spline == null)
                {
                    Debug.LogError("Is not a child of gameobject with Spline or MeshDeformer component");
                    return;
                }

                m_spline.ControlPointChanged -= OnControlPointChanged;
                m_spline.ControlPointChanged += OnControlPointChanged;
            }

#if UNITY_EDITOR
            m_localPosition = transform.localPosition;
#endif
        }

        private void OnDisable()
        {
            if (m_spline == null)
            {
                return;
            }
            m_spline.ControlPointChanged -= OnControlPointChanged;
        }

        protected void OnDestroy()
        {
            if (m_spline != null)
            {
                m_spline.ControlPointChanged -= OnControlPointChanged;
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (transform.localPosition != m_localPosition)
            {
                m_localPosition = transform.localPosition;
                if (m_spline != null)
                {
                    m_spline.SetControlPointLocal(m_index, m_localPosition);
                }
            }
        }
#endif

        private void OnControlPointChanged(int pointIndex)
        {
            if (m_spline == null)
            {
                return;
            }

            if (pointIndex == m_index)
            {
                transform.position = m_spline.transform.TransformPoint(m_spline.GetControlPointLocal(pointIndex));
#if UNITY_EDITOR
                m_localPosition = transform.localPosition;
#endif
            }
        }
    }
}


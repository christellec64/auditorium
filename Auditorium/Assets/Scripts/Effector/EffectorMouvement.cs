using UnityEngine;
using UnityEngine.EventSystems;
public class EffectorMouvement : MonoBehaviour {
    #region Private Members

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _minRadius;
    [SerializeField] private float _maxRadius;
    [SerializeField] private float _forceMultiplier = 1f;
    [SerializeField] private Texture2D _cursorMove;
    [SerializeField] private Texture2D _cursorResize;

    private Camera _camera;
    private Vector2 _offset;
    public Transform _effectorTransform;
    private Transform _effectorResizeTransform;
    private CircleShape _effectorResizeRadiusScript;
    private AreaEffector2D _areaEffector;
    private float _lastDistanceFromEffector;
    private Vector2 _cursorMoveHotSpot;
    private Vector2 _cursorResizeHotSpot;

    private CursorType _currentCursor;
    private enum CursorType {
        Normal,
        Move,
        Resize
    }

    #endregion

    private void Awake( ) {
        _camera = Camera.main;

        _cursorMoveHotSpot = new Vector2(_cursorMove.width / 2f, _cursorMove.height / 2f);
        _cursorResizeHotSpot = new Vector2(_cursorResize.width / 2f, _cursorResize.height / 2f);

        _currentCursor = CursorType.Normal;
    }

    // Update is called once per frame
    void Update( ) {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = new RaycastHit2D( );

        if (!EventSystem.current.IsPointerOverGameObject()) {
            hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _layerMask);
        }
        else if ( _currentCursor != CursorType.Normal && !Input.GetMouseButton(0) ) {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            _currentCursor = CursorType.Normal;
        }

        if ( hit.collider != null ) {
            if ( hit.collider.CompareTag("EffectorCenter") && _currentCursor != CursorType.Move ) {
                Cursor.SetCursor(_cursorMove, _cursorMoveHotSpot, CursorMode.Auto);
                _currentCursor = CursorType.Move;
            }
            else if ( hit.collider.CompareTag("EffectorOut") && _currentCursor != CursorType.Resize ) {
                Cursor.SetCursor(_cursorResize, _cursorResizeHotSpot, CursorMode.Auto);
                _currentCursor = CursorType.Resize;
            }
        }
        else if ( _currentCursor != CursorType.Normal  && !Input.GetMouseButton(0)) {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            _currentCursor = CursorType.Normal;
        }

        if ( Input.GetMouseButtonDown(0) ) {

            if ( hit.collider != null ) {

                if ( hit.collider.CompareTag("EffectorCenter") ) {

                    _effectorTransform = hit.collider.transform.parent;
                    _offset = hit.point - ( Vector2 ) _effectorTransform.position;

                }

                if ( hit.collider.CompareTag("EffectorOut") ) {

                    _effectorResizeTransform = hit.collider.transform;
                    _effectorResizeRadiusScript = _effectorResizeTransform.GetComponent<CircleShape>( );
                    _areaEffector = _effectorResizeTransform.GetComponent<AreaEffector2D>( );
                    _lastDistanceFromEffector = Vector2.Distance(ray.origin, _effectorResizeTransform.position);

                }
            }
        }

        if ( Input.GetMouseButton(0) && _effectorTransform != null ) {

            _effectorTransform.position = new Vector3(ray.origin.x - _offset.x, ray.origin.y - _offset.y, 0f);

        }

        if ( Input.GetMouseButton(0) && _effectorResizeTransform != null ) {

            float _distance = Vector2.Distance(ray.origin, _effectorResizeTransform.position);
            float _difDistance = _lastDistanceFromEffector - _distance;

            float _newRadius = _effectorResizeRadiusScript.Radius - _difDistance;
            _newRadius = Mathf.Clamp(_newRadius, _minRadius, _maxRadius);

            _areaEffector.forceMagnitude = _newRadius * _forceMultiplier;

            _effectorResizeRadiusScript.Radius = _newRadius;
            _lastDistanceFromEffector = _distance;


        }

        if ( Input.GetMouseButtonUp(0) ) {

            _effectorTransform = null;
            _effectorResizeTransform = null;
            _effectorResizeRadiusScript = null;
            _areaEffector = null;

        }
    }
}

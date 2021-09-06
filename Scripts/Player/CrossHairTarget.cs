using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    [SerializeField] private Transform _fpsCameraTransform;
    private Ray _cameraRay;
    private RaycastHit _hit;

    // Update is called once per frame
    void Update()
    {
        this._cameraRay.origin = this._fpsCameraTransform.position;
        this._cameraRay.direction = this._fpsCameraTransform.forward;
        Physics.Raycast(this._cameraRay, out this._hit);
        this.transform.position = this._hit.point;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacarFoto : MonoBehaviour
{
    [SerializeField] private float minFlashTime = 8f;
    [SerializeField] private float maxFlashTime = 12f;
    [SerializeField] private float pictureDelay = 1f;
    [SerializeField] private bool invertDirection = false;
    [SerializeField] private bool enableLeft = true;
    [SerializeField] private bool enableRight = true;
    [SerializeField] private GameObject flashPrefab;

    private float nextPictureTime = 0;
    private Vector3 initialEulerAngle;
    private readonly int[] possibleRotationValues = new int[5] { -75, -45, 0, 45, 75 };
    private bool isTakingPicture;

    private void Awake() {
        initialEulerAngle = transform.eulerAngles;
    }

    private void Update() {
        if (Time.time > nextPictureTime) {
            var flashTime = Random.Range(minFlashTime, maxFlashTime);
            nextPictureTime = Time.time + flashTime;
        } else {
            if (isTakingPicture) {
                return;
            }

            isTakingPicture = true;
            var min = enableLeft ? 0 : 2;
            var max = enableRight ? 4 : 2;
            int rotationAngle = Random.Range(min, max);
            var rotate = possibleRotationValues[rotationAngle];
            if (invertDirection) {
                rotate = -rotate;
            }

            transform.eulerAngles = transform.rotation.eulerAngles + new Vector3(0, 0, rotate);
            StartCoroutine(TakePicture(transform.rotation));
        }

    }

    private IEnumerator TakePicture(Quaternion shotDirection) {
        yield return new WaitForSeconds(pictureDelay);
        Instantiate(flashPrefab, transform.position, shotDirection);
        yield return new WaitForSeconds(.5f);
        isTakingPicture = false;
        transform.eulerAngles = initialEulerAngle;
        yield return null;
    }
}

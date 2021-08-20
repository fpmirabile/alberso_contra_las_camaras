using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Alberso : MonoBehaviour {
    [SerializeField] private bool enableContinuosMovement = true;
    [SerializeField] private float albersoSpeed = 6f;
    [SerializeField] private int albersoBlockMovement = 9;

    private int vidasLeft = 3;
    private void Update() {
        if (Time.timeScale == 0) {
            return;
        }

        if (!enableContinuosMovement) {
            #if UNITY_STANDALONE
            var x = Input.GetKeyDown(KeyCode.A) ? -1 : Input.GetKeyDown(KeyCode.D) ? 1 : 0;
            #endif
            #if UNITY_ANDROID
            if (Input.touches.Length == 0) {
                return;
            }

            var touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began || EventSystem.current.IsPointerOverGameObject(touch.fingerId)) { 
                return; 
            }

            var middleScreen = Screen.width / 2;
            var leftTouch = touch.position.x < middleScreen;
            var rightTouch = touch.position.x > middleScreen;
            var x = leftTouch ? -1 : rightTouch ? 1 : 0;
            #endif
            var currentX = transform.position.x;
            if (x > 0 && currentX <= 8) {
                transform.position = transform.position + new Vector3(albersoBlockMovement, 0, 0);
            } else if (x < 0 && currentX >= -8) {
                transform.position = transform.position - new Vector3(albersoBlockMovement, 0, 0);
            }
        } else {
            var x = Input.GetAxis("Horizontal");
            transform.Translate(new Vector3(x, 0, 0) * albersoSpeed * Time.deltaTime);
        }
    }

    public void QuitarVida() {
        this.vidasLeft--;
        PlayerHit?.Invoke();
        if (vidasLeft <= 0) {
            onPlayerDie?.Invoke();
        }
    }

    public static Action PlayerHit;
    public static Action onPlayerDie;
}

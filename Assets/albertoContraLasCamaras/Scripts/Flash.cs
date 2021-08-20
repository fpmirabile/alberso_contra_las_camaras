using System;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private float flashVelocity = 3f;

    private void Update() {
        var up = transform.parent.up * flashVelocity;
        transform.Translate(up  * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D.tag.Equals("Fin")) {
            var parent = transform.parent.gameObject;
            Debug.Log("Destruyendo");
            Destroy(parent, 1);
            parent.SetActive(false);
            OnFlashEvaded?.Invoke();
            return;
        }

        var alberso = collider2D.GetComponent<Alberso>();
        if (alberso) {
            Debug.Log("Alberso");
            alberso.QuitarVida();
        }
    }

    public static Action OnFlashEvaded;
}

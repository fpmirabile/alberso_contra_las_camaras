using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    public void TogglePauseMenu(bool paused) {
        this.gameObject.SetActive(paused);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenClicker : MonoBehaviour {
    void Update() {
        if (Input.GetButtonDown("Fire1"))
            Clicked();
    }

    private void Clicked() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit)) {
            IClickable clickable = hit.collider.gameObject.GetComponent<IClickable>();
            clickable.OnClick(hit);
        }
    }
}
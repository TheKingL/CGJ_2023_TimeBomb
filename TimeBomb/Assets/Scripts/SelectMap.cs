using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMap : MonoBehaviour
{

    public GameObject dinosaure;
    public GameObject medieval;
    public GameObject futuriste;

    private bool isDino = false;
    private bool isMedieval = true;
    private bool isFuturiste = false;
    void Start() {}

    void Update() {}

    public void BackgroundChangerForward() {
        if(!isDino && !isMedieval && isFuturiste) {
            dinosaure.SetActive(true);
            medieval.SetActive(false);
            futuriste.SetActive(false);
            isDino = true;
            isMedieval = false;
            isFuturiste = false;
        }
        else if(isDino && !isMedieval && !isFuturiste) {
            dinosaure.SetActive(false);
            medieval.SetActive(true);
            futuriste.SetActive(false);
            isDino = false;
            isMedieval = true;
            isFuturiste = false;
        }
        else if(!isDino && isMedieval && !isFuturiste) {
            dinosaure.SetActive(false);
            medieval.SetActive(false);
            futuriste.SetActive(true);
            isDino = false;
            isMedieval = false;
            isFuturiste = true;
        }
    }

    public void BackgroundChangerBackward() {
        if(!isDino && !isMedieval && isFuturiste) {
            dinosaure.SetActive(false);
            medieval.SetActive(true);
            futuriste.SetActive(false);
            isDino = false;
            isMedieval = true;
            isFuturiste = false;
        }
        else if(isDino && !isMedieval && !isFuturiste) {
            dinosaure.SetActive(false);
            medieval.SetActive(false);
            futuriste.SetActive(true);
            isDino = false;
            isMedieval = false;
            isFuturiste = true;
        }
        else if(!isDino && isMedieval && !isFuturiste) {
            dinosaure.SetActive(true);
            medieval.SetActive(false);
            futuriste.SetActive(false);
            isDino = true;
            isMedieval = false;
            isFuturiste = false;
        }
    }

    public bool getDino() {
        return isDino;
    }

    public bool getMedieval() {
        return isMedieval;
    }

    public bool getFuturiste() {
        return isFuturiste;
    }
}

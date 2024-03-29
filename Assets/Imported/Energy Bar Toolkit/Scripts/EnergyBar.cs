/*
* Energy Bar Toolkit by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class EnergyBar : MonoBehaviour {

    // ===========================================================
    // Constants
    // ===========================================================

    // ===========================================================
    // Fields
    // ===========================================================

    public int valueCurrent {
        get { return Mathf.Clamp(_valueCurrent, valueMin, valueMax); }
        set { _valueCurrent = value; }
    }

    [SerializeField]
    [FormerlySerializedAs("valueCurrent")]
    private int _valueCurrent = 50;

    public int valueMin = 0;
    public int valueMax = 100;
    
    public float ValueF {
        get {
            if (valueMax == valueMin) {
                return 0;
            }

            if (!animationEnabled) {
                return Mathf.Clamp((valueCurrent - valueMin) / (float) (valueMax - valueMin), 0, 1);
            } else {
                return Mathf.Clamp(animValueF, 0, 1);
            }
        }
        
        set {
            valueCurrent = Mathf.RoundToInt(value * (valueMax - valueMin) + valueMin);
        }
    }
    
    [HideInInspector]
    public bool animationEnabled;
    [HideInInspector]
    public float animValueF;

    // ===========================================================
    // Constructors (Including Static Constructors)
    // ===========================================================

    // ===========================================================
    // Getters / Setters
    // ===========================================================

    // ===========================================================
    // Methods for/from SuperClass/Interfaces
    // ===========================================================

    protected void Update() {
        if (animationEnabled) {
            valueCurrent = valueMin + (int) (animValueF * (valueMax - valueMin));
        }
        if (animationEnabled)
        {
            valueCurrent = valueMin + (int)(animValueF * (valueMax - valueMin));
        }

        var fsm = Camera.main.gameObject.GetComponent<PlayMakerFSM>();
        var attribute = valueCurrent;
        if (fsm.FsmVariables != null)
        {
            attribute = fsm.FsmVariables.GetFsmInt(this.gameObject.name).Value;
        }
        valueCurrent = attribute;
    }

    // ===========================================================
    // Methods
    // ===========================================================
    
    /*public void SetValueCurrent(int valueCurrent) {
        this.valueCurrent = valueCurrent;
    }*/

    void adjustAwareness(int Awareness) {
        valueCurrent = Awareness;
    }

    void adjustCoolness(int Coolness) {
        valueCurrent = Coolness;
    }

    void adjustHealthiness(int Healthiness) {
        valueCurrent = Healthiness;
    }
    
    public void SetValueMin(int valueMin) {
        this.valueMin = valueMin;
    }
    
    public void SetValueMax(int valueMax) {
        this.valueMax = valueMax;
    }
    
    public void SetValueF(float valueF) {
        ValueF = valueF;
    }

    public void ChangeValueF(float valueF) {
        ValueF += valueF;
        ValueF = Mathf.Clamp01(ValueF);
    }

    public void ChangeValueCurrent(int value) {
        valueCurrent += value;
        ValueF = Mathf.Clamp01(ValueF);
    }
    
    // ===========================================================
    // Static Methods
    // ===========================================================

    // ===========================================================
    // Inner and Anonymous Classes
    // ===========================================================

}

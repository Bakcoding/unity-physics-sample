using UnityEngine;
using UnityEngine.UI;

public class BasicPhysicsUI : MonoBehaviour
{
    [Header("Simulation Object")]
    [SerializeField] Rigidbody rbCube;
    [SerializeField] Rigidbody rbSphere;
    [SerializeField] Rigidbody rbCylinder;
    [SerializeField] Rigidbody rbCapsule;

    [Header("Gravity")]
    [SerializeField] Slider sliderGravityX;
    [SerializeField] TMPro.TextMeshProUGUI txtGravityXAmount;
    [SerializeField] Slider sliderGravityY;
    [SerializeField] TMPro.TextMeshProUGUI txtGravityYAmount;
    [SerializeField] Slider sliderGravityZ;
    [SerializeField] TMPro.TextMeshProUGUI txtGravityZAmount;

    [Header("Rigidbody Values")]
    [SerializeField] TMPro.TextMeshProUGUI txtOptionTitle;
    [SerializeField] Button btnMass;
    [SerializeField] Button btnDrag;
    [SerializeField] Button btnAngularDrag;

    [SerializeField] Slider sliderCubeMass;
    [SerializeField] TMPro.TextMeshProUGUI txtCubeMass;
    [SerializeField] Slider sliderSphereMass;
    [SerializeField] TMPro.TextMeshProUGUI txtSphereMass;
    [SerializeField] Slider sliderCylinerMass;
    [SerializeField] TMPro.TextMeshProUGUI txtCylinderMass;
    [SerializeField] Slider sliderCapsuleMass;
    [SerializeField] TMPro.TextMeshProUGUI txtCapsuleMass;

    [Header("Force")]
    [SerializeField] Toggle toggleDefault;
    [SerializeField] Toggle toggleAddFroce;
    [SerializeField] Toggle toggleAddTorque;
    [SerializeField] Toggle toggleAddExplosionForce;

    [SerializeField] Slider sliderAddForce;
    [SerializeField] Slider sliderAddTorque;
    [SerializeField] Slider sliderAddExplosionForce;

    [SerializeField] TMPro.TextMeshProUGUI txtForce;
    [SerializeField] TMPro.TextMeshProUGUI txtTorque;
    [SerializeField] TMPro.TextMeshProUGUI txtExplosionForce;

    private eOptionType valueOptionType = eOptionType.Mass;

    private const float MIN_GRVITY = -100f;
    private const float MAX_GRVITY = 100f;

    private const float MIN_MASS = 0.5f;
    private const float MAX_MASS = 10f;

    private const float MIN_FORCE = 1f;
    private const float MAX_FORCE = 10f;

    private const float MIN_TORQUE = 1f;
    private const float MAX_TORQUE = 10f;

    private const float MIN_EXPLOSION = 100f;
    private const float MAX_EXPLOSION = 1000f;

    private float forceAmount = 10f;
    private float torqueAmount = 10f;
    private float explosionForceAmount = 100f;

    private void Awake()
    {
        sliderGravityX.minValue = MIN_GRVITY;
        sliderGravityX.maxValue = MAX_GRVITY;
        
        sliderGravityY.minValue = MIN_GRVITY;
        sliderGravityY.maxValue = MAX_GRVITY;
        
        sliderGravityZ.minValue = MIN_GRVITY;
        sliderGravityZ.maxValue = MAX_GRVITY;

        //===================================

        sliderCubeMass.minValue = MIN_MASS;
        sliderCubeMass.maxValue = MAX_MASS;

        sliderSphereMass.minValue = MIN_MASS;
        sliderSphereMass.maxValue = MAX_MASS;

        sliderCylinerMass.minValue = MIN_MASS;
        sliderCylinerMass.maxValue = MAX_MASS;

        sliderCapsuleMass.minValue = MIN_MASS;
        sliderCapsuleMass.maxValue = MAX_MASS;

        sliderAddForce.minValue = MIN_FORCE;
        sliderAddForce.maxValue = MAX_FORCE;

        sliderAddTorque.minValue = MIN_TORQUE;
        sliderAddTorque.maxValue = MAX_TORQUE;

        sliderAddExplosionForce.minValue = MIN_EXPLOSION;
        sliderAddExplosionForce.maxValue = MAX_EXPLOSION;

        sliderGravityX.onValueChanged.AddListener(OnValueChangedGravityX);
        sliderGravityY.onValueChanged.AddListener(OnValueChangedGravityY);
        sliderGravityZ.onValueChanged.AddListener(OnValueChangedGravityZ);

        sliderCubeMass.onValueChanged.AddListener(OnValueChangedCubeMass);
        sliderSphereMass.onValueChanged.AddListener(OnValueChangedSphereMass);
        sliderCylinerMass.onValueChanged.AddListener(OnValueChangedCylinderMass);
        sliderCapsuleMass.onValueChanged.AddListener(OnValueChangedCapsuleMass);

        sliderAddForce.onValueChanged.AddListener(OnValueChangedForce);
        sliderAddTorque.onValueChanged.AddListener(OnValueChangedTorque);
        sliderAddExplosionForce.onValueChanged.AddListener(OnValueChangedExplosionForce);

        toggleDefault.onValueChanged.AddListener(OnStateChangedDefault);
        toggleAddFroce.onValueChanged.AddListener(OnStateChangedAddForce);
        toggleAddTorque.onValueChanged.AddListener(OnStateChangedAddTorque);
        toggleAddExplosionForce.onValueChanged.AddListener(OnStateChangedAddExplosionForce);

        btnMass.onClick.AddListener(OnClick_Mass);
        btnDrag.onClick.AddListener(OnClick_Drag);
        btnAngularDrag.onClick.AddListener(OnClick_AngularDrag);
    }

    private void OnEnable()
    {
        toggleDefault.isOn = true;
        SetOptionType(eOptionType.Mass);
        sliderCubeMass.value = rbCube.mass;
        sliderSphereMass.value = rbSphere.mass;
        sliderCylinerMass.value = rbCylinder.mass;
        sliderCapsuleMass.value = rbCapsule.mass;

        PhysicsTutorial.Instance.ForceAmount = forceAmount;
        PhysicsTutorial.Instance.TorqueAmount = torqueAmount;
        PhysicsTutorial.Instance.ExplosionForceAmount = explosionForceAmount;

        sliderAddForce.value = forceAmount;
        sliderAddTorque.value = torqueAmount;
        sliderAddExplosionForce.value = explosionForceAmount;

        sliderGravityX.value = Physics.gravity.x;
        sliderGravityY.value = Physics.gravity.y;
        sliderGravityZ.value = Physics.gravity.z;

        sliderAddForce.value = (MAX_FORCE - MIN_FORCE) / 2;
        sliderAddTorque.value = (MAX_TORQUE - MIN_TORQUE) / 2;
        sliderAddExplosionForce.value = (MAX_EXPLOSION - MIN_EXPLOSION) / 2;
    }

    private void OnValueChangedGravityX(float val)
    {
        //float tmpVal = Mathf.Floor(val * 10) / 10;
        txtGravityXAmount.text = $"{val:0.#}";
        Vector3 gravity = Physics.gravity;
        gravity.x = val;
        Physics.gravity = gravity;
    }

    private void OnValueChangedGravityY(float val)
    {
        //float tmpVal = Mathf.Floor(val * 10) / 10;
        txtGravityYAmount.text = $"{val:0.#}";
        Vector3 gravity = Physics.gravity;
        gravity.y = val;
        Physics.gravity = gravity;
    }

    private void OnValueChangedGravityZ(float val)
    {
        //float tmpVal = Mathf.Floor(val * 10) / 10;
        txtGravityZAmount.text = $"{val:0.#}";
        Vector3 gravity = Physics.gravity;
        gravity.z = val;
        Physics.gravity = gravity;
    }


    private void OnValueChangedCubeMass(float val)
    {
        txtCubeMass.text = $"{val:0.##}";
        ChangeOptionValue(rbCube, val);
    }

    private void OnValueChangedSphereMass(float val)
    {
        txtSphereMass.text = $"{val:0.##}";
        ChangeOptionValue(rbSphere, val);
    }

    private void OnValueChangedCylinderMass(float val)
    {
        txtCylinderMass.text = $"{val:0.##}";
        ChangeOptionValue(rbCylinder, val);
    }

    private void OnValueChangedCapsuleMass(float val)
    {
        txtCapsuleMass.text = $"{val:0.##}";
        ChangeOptionValue(rbCapsule, val);
    }

    private void OnValueChangedForce(float val)
    {
        txtForce.text = $"{val:0}";
        forceAmount = val;

        PhysicsTutorial.Instance.ForceAmount = forceAmount;
    }

    private void OnValueChangedTorque(float val)
    {
        txtTorque.text = $"{val:0}";
        torqueAmount = val;

        PhysicsTutorial.Instance.TorqueAmount = torqueAmount;
    }

    private void OnValueChangedExplosionForce(float val)
    {
        txtExplosionForce.text = $"{val:0}";
        explosionForceAmount = val;

        PhysicsTutorial.Instance.ExplosionForceAmount = explosionForceAmount;
    }

    private void OnStateChangedDefault(bool isOn)
    {
        if (isOn)
        {
            PhysicsTutorial.Instance.SimulationType = eSimulationType.Basic;
        }
    }

    private void OnStateChangedAddForce(bool isOn)
    {
        if (isOn)
        {
            PhysicsTutorial.Instance.SimulationType = eSimulationType.Force;
        }
    }

    private void OnStateChangedAddTorque(bool isOn)
    {
        if (isOn)
        {
            PhysicsTutorial.Instance.SimulationType = eSimulationType.Torque;
        }
    }

    private void OnStateChangedAddExplosionForce(bool isOn)
    {
        if (isOn)
        {
            PhysicsTutorial.Instance.SimulationType = eSimulationType.Explosion;
        }
    }

    private void OnClick_Mass()
    {
        SetOptionType(eOptionType.Mass);
    }

    private void OnClick_Drag()
    {
        SetOptionType(eOptionType.Drag);
    }

    private void OnClick_AngularDrag()
    {
        SetOptionType(eOptionType.AngularDrag);
    }

    private void ChangeOptionValue(Rigidbody rb, float val)
    {
        if (valueOptionType == eOptionType.Mass)
        {
            rb.mass = val;
        }
        else if (valueOptionType == eOptionType.Drag)
        {
            rb.linearDamping = val;
        }
        else if (valueOptionType == eOptionType.AngularDrag)
        {
            rb.angularDamping = val;
        }
    }

    private void SetSliderOptionMinMaxValue(float min, float max)
    {
        sliderCubeMass.minValue = min;
        sliderCubeMass.maxValue = max;

        sliderSphereMass.minValue = min;
        sliderSphereMass.maxValue = max;

        sliderCylinerMass.minValue = min;
        sliderCylinerMass.maxValue = max;

        sliderCapsuleMass.minValue = min;
        sliderCapsuleMass.maxValue = max;
    }

    private void SetOptionType(eOptionType type)
    {
        valueOptionType = type;
        if (type == eOptionType.Mass)
        {
            SetSliderOptionMinMaxValue(MIN_MASS, MAX_MASS);
            sliderCubeMass.value = rbCube.mass;
            sliderSphereMass.value = rbSphere.mass;
            sliderCylinerMass.value = rbCylinder.mass;
            sliderCapsuleMass.value = rbCapsule.mass;
            txtOptionTitle.text = "Mass";
        }
        else if (type == eOptionType.Drag)
        {
            SetSliderOptionMinMaxValue(0, MAX_MASS);
            sliderCubeMass.value = rbCube.linearDamping;
            sliderSphereMass.value = rbSphere.linearDamping;
            sliderCylinerMass.value = rbCylinder.linearDamping;
            sliderCapsuleMass.value = rbCapsule.linearDamping;
            txtOptionTitle.text = "Drag";
        }
        else if (type == eOptionType.AngularDrag)
        {
            SetSliderOptionMinMaxValue(0, 50);
            sliderCubeMass.value = rbCube.angularDamping;
            sliderSphereMass.value = rbSphere.angularDamping;
            sliderCylinerMass.value = rbCylinder.angularDamping;
            sliderCapsuleMass.value = rbCapsule.angularDamping;
            txtOptionTitle.text = "AngularDrag";
        }
    }

}


public enum eOptionType
{
    Mass, Drag, AngularDrag,
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponScript : MonoBehaviour
{
    //definitions
    public enum WeaponType { GUN, MELEE }

    //global - not used yet
    private InputManager input_manager;
    private GameManager game_manager;

    [Header("Weapon Attributes")]
    public WeaponType weapon_type = WeaponType.GUN;
    [SerializeField] private float throw_speed = 10f; //speed at which the weapon is thrown
    [SerializeField] private float torque_speed = 10f; //speed at which the weapon is spun when thrown

    //make sure the rigid body is linked in the prefab
    [Header("Weapon script - Linked Objects")]
    [SerializeField] private Rigidbody2D rigid_body = null;

    //might need some redoing for enemies that already hold weapons
    public GameObject weapon_holder = null;
    public float pick_up_cooldown = 0.5f;
    public bool pickable = true;
    public bool is_flying = false;

    //private variables

    void Awake()
    {
        game_manager = Object.FindObjectOfType<GameManager>(); // these might be needed later
        input_manager = Object.FindObjectOfType<InputManager>();
        if (game_manager == null || input_manager == null) UnityEngine.SceneManagement.SceneManager.LoadScene("preload");
    }

    void Update()
    {
        is_flying = rigid_body.velocity.magnitude >= 0.01f;
    }

    public abstract void Attack();

    public void ThrowAwayWeapon()
    {
        //set to the centre of the player - accuracy reasons
        rigid_body.position = weapon_holder.transform.position;

        //tweak values above to make it look real
        rigid_body.AddTorque(torque_speed, ForceMode2D.Impulse);

        //calculate the direction in which the weapon will travel
        CameraScript main_camera_script = Camera.main.GetComponent<CameraScript>();
        Vector3 throw_direction = (main_camera_script.real_mouse_position - weapon_holder.transform.position).normalized;
        rigid_body.AddForce(throw_direction * throw_speed, ForceMode2D.Impulse);

        //the weapon has time to travel away from the player
        //otherwise the player would drop and pick up the weapon back instantly
        StartCoroutine(SetPickableToTrue());
    }

    IEnumerator SetPickableToTrue()
    {
        yield return new WaitForSeconds(pick_up_cooldown);
        pickable = true;
    }

    public void SetWeaponHolder(GameObject holder)
    {
        weapon_holder = holder;
        pickable = false;
    }
}

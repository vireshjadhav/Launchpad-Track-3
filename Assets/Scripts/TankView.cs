using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    private TankController tankController;

    private float movement;
    private float rotation;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private MeshRenderer[] childs;

    void Awake()
    {
        GameObject cam = GameObject.Find("Main Camera");
        cam.transform.SetParent(transform);
        cam.transform.position = new Vector3(0.0f, 3.0f, -4.0f);
        cam.transform.rotation = Quaternion.Euler(15.0f, 0.0f, 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if(movement != 0)
            tankController.Move(movement, tankController.GetTankModel().movementSpeed);

        if (rotation != 0)
            tankController.Rotate(rotation, tankController.GetTankModel().rotationSpeeed);
    }

    private  void Movement()
    {
        movement = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Horizontal");
    }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }

    public void ChangeColor(Material color)
    {
        foreach (MeshRenderer child in childs)
        {
            child.material = color;
        }
    }

    public Rigidbody GetRigidbody() => rb;
}

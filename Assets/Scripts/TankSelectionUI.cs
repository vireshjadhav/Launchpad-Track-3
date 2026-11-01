using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankSelectionUI : MonoBehaviour
{
    [SerializeField]
    private Button greenTankButton;

    [SerializeField]
    private Button redTankButton;

    [SerializeField]
    private Button blueTankButton;

    [SerializeField]
    private Button yellowTankButton;

    [SerializeField]
    private TankSpawner tankSpawner;

    // Start is called before the first frame update
    void Start()
    {
        if (greenTankButton != null)
            greenTankButton.onClick.AddListener(GreenTankSelection);

        if (redTankButton != null)
            redTankButton.onClick.AddListener(RedTankSelection);

        if (blueTankButton != null)
            blueTankButton.onClick.AddListener(BlueTankSelection);

        if (yellowTankButton != null)
            yellowTankButton.onClick.AddListener(YellowTankSelection);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GreenTankSelection()
    {
        tankSpawner.CreateTank(TankTypes.GreenTank);
        this.gameObject.SetActive(false);
    }
    private void RedTankSelection()
    {
        tankSpawner.CreateTank(TankTypes.RedTank);
        this.gameObject.SetActive(false);
    }
    private void BlueTankSelection()
    {
        tankSpawner.CreateTank(TankTypes.BlueTank);
        this.gameObject.SetActive(false);
    }
    private void YellowTankSelection()
    {
        tankSpawner.CreateTank(TankTypes.YellowTank);
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (greenTankButton != null)
            greenTankButton.onClick.RemoveListener(GreenTankSelection);

        if (redTankButton != null)
            redTankButton.onClick.RemoveListener(RedTankSelection);

        if (blueTankButton != null)
            blueTankButton.onClick.RemoveListener(BlueTankSelection);

        if (yellowTankButton != null)
            yellowTankButton.onClick.RemoveListener(YellowTankSelection);
    }
}

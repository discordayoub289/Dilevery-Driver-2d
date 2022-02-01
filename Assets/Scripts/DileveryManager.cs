using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DileveryManager : MonoBehaviour
{
    private GlovoController _glovoController;
    private List<GameObject> _allPackages;
    private List<GameObject> _allCustomers;
    public bool hasEnabledPackage = false;
    public bool hasEnabledCustomer = false;

    // Start is called before the first frame update
    void Start()
    {
        _glovoController = GameObject.FindObjectOfType<GlovoController>();
        _allPackages = GetAllPackages();
        _allCustomers = GetAllCustomers();
    }

    // Update is called once per frame
    void Update()
    {

        bool hasPackage = _glovoController._hasPackage;
        
        if(!hasPackage && !hasEnabledPackage) {
            GameObject package = GetRandomFromList(_allPackages);
            package.SetActive(true);
            hasEnabledPackage = true;
        }
        if(hasPackage && !hasEnabledCustomer) {
            GameObject customer = GetRandomFromList(_allCustomers);
            customer.SetActive(true);
            hasEnabledCustomer = true;
        }

    }


    private List<GameObject> GetAllPackages() {
        List<GameObject> packages = new List<GameObject>();
        Transform packagesParent = transform.GetChild(0);
        for (int i = 0; i < packagesParent.childCount; i++) {
            GameObject package = packagesParent.GetChild(i).gameObject;

            packages.Add(package);
        }
        return packages;
    }
    private GameObject GetRandomFromList(List<GameObject> list) {
        return list[Random.Range(0, list.Count)];
    }
    private List<GameObject> GetAllCustomers() {
        List<GameObject> customers = new List<GameObject>();
        Transform customersParent = transform.GetChild(1);
        for (int i = 0; i < customersParent.childCount; i++) {
            GameObject customer = customersParent.GetChild(i).gameObject;

            customers.Add(customer);
        }
        return customers;
    }
}

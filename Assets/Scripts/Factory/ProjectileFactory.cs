using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;
using TMPro;

public class ProjectileFactory : MonoBehaviour
{
    public GameObject projectilePrefab1;
    public GameObject projectilePrefab2;

    public GameObject buttonPanel;
    public GameObject buttonPrefab;

    List<ProjectileType> projectiles;

    // Start is called before the first frame update
    void Start()
    {
        var projectileTypes = Assembly.GetAssembly(typeof(ProjectileType)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(ProjectileType)));

        projectiles = new List<ProjectileType>();

        foreach (var type in projectileTypes)
        {
            var tempType = Activator.CreateInstance(type) as ProjectileType;
            projectiles.Add(tempType);
        }

        ButtonPanel();

    }

    public ProjectileType GetProjectile(string projectileType)
    {
        foreach (ProjectileType projectile in projectiles)
        {
            if (projectile.Name == projectileType)
            {
                //Debug.Log();
                var target = Activator.CreateInstance(projectile.GetType()) as ProjectileType;

                return target;
            }
        }

        return null;
    }

    void ButtonPanel()
    {
        foreach (ProjectileType projectile in projectiles)
        {
            var button = Instantiate(buttonPrefab);
            button.transform.SetParent(buttonPanel.transform);
            button.gameObject.name = projectile.Name + " Button";
            button.GetComponentInChildren<TextMeshProUGUI>().text = projectile.Name;
        }
    }
}

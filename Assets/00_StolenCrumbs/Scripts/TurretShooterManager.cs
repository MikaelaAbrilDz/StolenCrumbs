using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TurretShooterManager : MonoBehaviour
{
    float timeFrequency = 0.1f;
    int maxFrequencies = 30;

    float counter = 0;
    int frequencyCounter = 0;

    public List<Turret> turrets = new List<Turret>();

    void Update()
    {
        counter += Time.deltaTime;
        if (counter > timeFrequency)
        {
            counter = 0;
            frequencyCounter += 1;
            for (int i = 1; i < maxFrequencies; i++)
            {
                if (frequencyCounter % i == 0)
                {
                    foreach (Turret turret in turrets)
                    {
                        if (turret != null && turret.turretData.fireRate == i) turret.Shoot();
                    }
                }
            }
        }
    }
}

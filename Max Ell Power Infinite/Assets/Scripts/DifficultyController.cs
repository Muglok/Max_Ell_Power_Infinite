using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public static DifficultyController sharedInstance;

    public float scaleInc = .25f;
    public float velocityInc = 0.22f;
    public float secZoneInc = 1.375f;
    public float limZoneInc = 1.05f;

    private int raisedLevel = 0;
    private int actualLevels = 0;

    // Start is called before the first frame update
    void Start()
    {
        sharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImplementsMoreDifficulty(int difficulty)
    {
        actualLevels = difficulty / 5;
        
        //lvl-1
        if (actualLevels > 1 && raisedLevel == 0)
        {
            raisedLevel++;

            FireWallController.sharedInstance.velocity += 0.22f;
        }

        //lvl-2
        if (actualLevels > 2 && raisedLevel ==  1)
        {
            raisedLevel++;

            FireWallController.sharedInstance.velocity += 0.22f;

            FireWallController.sharedInstance.securityZone += 1.375f;
            FireWallController.sharedInstance.limitSeparation -= 1.05f;
        }

        //lvl-3
        if (actualLevels > 3 && raisedLevel == 2)
        {
            raisedLevel++;

            FireWallController.sharedInstance.securityZone += 1.375f;
            FireWallController.sharedInstance.limitSeparation -= 1.05f;
        }

        //lvl-4
        if (actualLevels > 4 && raisedLevel == 3)
        {
            raisedLevel++;

            //Time.timeScale += scaleInc;

            LevelGenerator.sharedInstance.actualMinRange = 3;
            LevelGenerator.sharedInstance.actualMaxRange = 8;
        }

        //lvl-5
        if (actualLevels > 5 && raisedLevel == 4)
        {
            raisedLevel++;

            FireWallController.sharedInstance.velocity += 0.22f;

            FireWallController.sharedInstance.securityZone += 1.375f;
            FireWallController.sharedInstance.limitSeparation -= 1.05f;
        }

        //lvl-6
        if (actualLevels > 6 && raisedLevel == 5)
        {
            raisedLevel++;

            FireWallController.sharedInstance.velocity += 0.22f;

            LevelGenerator.sharedInstance.actualMinRange = 5;
            LevelGenerator.sharedInstance.actualMaxRange = 11;
        }

        //lvl-7
        if (actualLevels > 7 && raisedLevel == 6)
        {
            raisedLevel++;

            FireWallController.sharedInstance.velocity += 0.22f;
        }

        //lvl-8
        if (actualLevels > 8 && raisedLevel == 7)
        {
            raisedLevel++;

            FireWallController.sharedInstance.securityZone += 1.375f;
            FireWallController.sharedInstance.limitSeparation -= 1.05f;
            //Time.timeScale += scaleInc;

            LevelGenerator.sharedInstance.actualMinRange = 6;
            LevelGenerator.sharedInstance.actualMaxRange = 13;
        }

        //lvl-9
        if (actualLevels > 9 && raisedLevel == 8)
        {
            raisedLevel++;

            FireWallController.sharedInstance.velocity += 0.22f;

            FireWallController.sharedInstance.securityZone += 1.375f;
            FireWallController.sharedInstance.limitSeparation -= 1.05f;
        }

        //lvl-10
        if (actualLevels > 10 && raisedLevel == 9)
        {
            raisedLevel++;

            FireWallController.sharedInstance.velocity += 0.22f;

            LevelGenerator.sharedInstance.actualMinRange = 8;
            LevelGenerator.sharedInstance.actualMaxRange = 14;
        }

        //lvl-11
        if (actualLevels > 11 && raisedLevel == 10)
        {
            raisedLevel++;

            FireWallController.sharedInstance.securityZone += 1.375f;
            FireWallController.sharedInstance.limitSeparation -= 1.05f;
        }

        //lvl-12
        if (actualLevels > 12 && raisedLevel == 11)
        {
            raisedLevel++;

            FireWallController.sharedInstance.velocity += 0.22f;
        }

        //lvl-13
        if (actualLevels > 13 && raisedLevel == 12)
        {
            raisedLevel++;

            Time.timeScale += scaleInc;
        }

        //lvl-14
        if (actualLevels > 14 && raisedLevel == 13)
        {
            raisedLevel++;

            FireWallController.sharedInstance.velocity += 0.22f;

            LevelGenerator.sharedInstance.actualMinRange = 11;
            LevelGenerator.sharedInstance.actualMaxRange = 17;
        }

        //lvl-15
        if (actualLevels > 15 && raisedLevel == 14)
        {
            raisedLevel++;

            FireWallController.sharedInstance.velocity += 0.22f;

            FireWallController.sharedInstance.securityZone += 1.375f;
            FireWallController.sharedInstance.limitSeparation -= 1.05f;
        }

        //lvl-16
        if (actualLevels > 16 && raisedLevel == 15)
        {
            raisedLevel++;

            //Time.timeScale += scaleInc;
        }

        //lvl-17
        if (actualLevels > 17 && raisedLevel == 16)
        {
            raisedLevel++;

            FireWallController.sharedInstance.velocity = 4.2f;

            FireWallController.sharedInstance.securityZone = -5f;
            FireWallController.sharedInstance.limitSeparation = 11.6f;
        }

        //lvl-18+
        /*if (difficulty > 90 && difficulty % 25== actualLevels -1)
        {
            raisedLevel++;

            if (Time.timeScale < 35)
            {
                Time.timeScale += scaleInc;
            }
        }*/
    }
}

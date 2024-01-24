


[System.Serializable]
public class PlayerData {


    public int health;
    public bool RedArm;
    public bool PurplePower;
    public bool legs;
    public float[] position;
    public float[] lastSpawnPoint;

    public PlayerData (PlayerMovement playerMovement, LifeManager playerLife, ColorAura playerAura)
    {

        legs = playerMovement.hasLegs;

        position = new float[2];

        position[0] = playerMovement.transform.position.x;
        position[1] = playerMovement.transform.position.y;

        health = playerLife.life;

        lastSpawnPoint = new float[2];

        lastSpawnPoint[0] = playerLife.LastCheckPoint.x;
        lastSpawnPoint[1] = playerLife.LastCheckPoint.y;

        RedArm = playerAura.hasRedArm;
        PurplePower = playerAura.hasPurplePower;

    }


}

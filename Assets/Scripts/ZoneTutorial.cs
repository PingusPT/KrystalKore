
using UnityEngine;

public class ZoneTutorial : MonoBehaviour
{

    [SerializeField] float transitionMusicDurantion = 1f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && gameObject.tag == "TutorialZone")
        {
            
            WorldMusicScript.intance.ChangeTrack(WorldMusicScript.intance.TutorialMusicInicio, transitionMusicDurantion);

        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "CoveyorZone")
        {

            WorldMusicScript.intance.ChangeTrack(WorldMusicScript.intance.treadmillMusicInicio, transitionMusicDurantion);

        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "FallZone")
        {

            WorldMusicScript.intance.ChangeTrack(WorldMusicScript.intance.WaterMusicInicio, transitionMusicDurantion);

        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "BombZone")
        {

            WorldMusicScript.intance.ChangeTrack(WorldMusicScript.intance.BombMusicInicio, transitionMusicDurantion);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            WorldMusicScript.intance.ChangeTrack(WorldMusicScript.intance.GeneralMusic, transitionMusicDurantion);
            
        }
    }
}

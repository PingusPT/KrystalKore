using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTutorial : MonoBehaviour
{
    [SerializeField] AudioClip MusicaTutorial, MusicaGeral;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //MusicaGeral = collision.GetComponent<WorldMusicScript>().GetCurrentAudio();

            WorldMusicScript.intance.ChangeTrack(MusicaTutorial);
            

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            WorldMusicScript.intance.ChangeTrack(MusicaGeral);

        }
    }
}

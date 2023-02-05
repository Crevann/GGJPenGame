using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundPeople : MonoBehaviour {
    [SerializeField] private SpriteRenderer[] people;

    [SerializeField] private Sprite[] uniquePeopleSprites;
    [SerializeField] private Sprite[] peopleSprites;
    [SerializeField][Range(0, 100)] private int uniquePeopleChance;

    //References
    private Animation anim;
    private void Awake() {
        anim = GetComponent<Animation>();
    }

    public void OpenStadium() {
        GeneratePeople();
        anim.Play();
    }

    public void CloseStadium() {
        anim.Rewind();
        anim.Play();
        anim.Sample();
        anim.Stop();
    }
    private void GeneratePeople() {
        List<Sprite> uniquePeopleSpritesSpawn = uniquePeopleSprites.ToList<Sprite>(); //Unique sprites can't be generate twice
        for (int i = 0; i < people.Length; i++) {
            int random = Random.Range(0, 100);
            if (random <= uniquePeopleChance && uniquePeopleSpritesSpawn.Count > 0) {
                int randomIndex = Random.Range(0, uniquePeopleSpritesSpawn.Count);
                people[i].sprite = uniquePeopleSpritesSpawn[randomIndex];
                uniquePeopleSpritesSpawn.Remove(uniquePeopleSpritesSpawn[randomIndex]);
                people[i].flipX = false;
            }
            else {
                people[i].sprite = peopleSprites[Random.Range(0, peopleSprites.Length)];
                people[i].flipX = Random.Range(0, 2) < 1 ? false : true;
            }
        }
    }

#if UNITY_EDITOR
    private void OnGUI() {
        if (GUI.Button(new Rect(600, 0, 100, 30), "Generate")) {
            CloseStadium();
        }
    }
#endif
}

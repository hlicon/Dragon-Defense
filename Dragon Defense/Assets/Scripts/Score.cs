using UnityEngine;
using System.Collections;


public class ScoreUpdate : MonoBehaviour {
    private int score;

    public int scoreUpdates(int newScore) {
        score += newScore;
        return score;
    }
}

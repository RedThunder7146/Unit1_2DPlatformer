using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class LogicScript : MonoBehaviour
{
    public int bullets;
    public int playerScore;
    public Text scoreText;
    public int health;
    public Text healthBar;
    public Text gameOverScreen;
    public Text bulletsFired;
    public Text MagCount;
    public int magCount;
    [ContextMenu("Increase Score")]
    public void addScore(int ScoreToAdd)
    {
        playerScore += ScoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void healthDepletion(int healthToAdd)
    {
        health -= healthToAdd;
        healthBar.text = health.ToString();
        if (health <= 0)
        {
            EnableText();
        }
        else
        {
            gameOverScreen.enabled = false;
        }
    }


    public void EnableText()
    {
        gameOverScreen.enabled = true;

    }

    public void BulletsFired(int Bullets)
    {
        bullets -= Bullets;
        bulletsFired.text = bullets.ToString();
    }
    public void MagazineCount(int mag)
    {
        magCount -= mag;
        MagCount.text = magCount.ToString();
    }
}
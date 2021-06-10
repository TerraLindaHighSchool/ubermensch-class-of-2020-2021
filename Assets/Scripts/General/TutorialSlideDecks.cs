using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tutorial Slide Deck", menuName = "Assets/Resources/SlideDecks", order = 1)]
public class TutorialSlideDecks : ScriptableObject
{
    public string title;
    public string description;
    public int currentSlide = 0;
    public Sprite[] slides;
    
    public Sprite nextSlide()
    {
        Debug.Log(currentSlide);
        currentSlide += 1;
        Debug.Log(currentSlide);
        if (currentSlide < slides.Length)
        {
            return slides[currentSlide];
        }
        else
        {
            return null;
        }
    }

    public Sprite startSlides()
    {
        currentSlide = 0;
        return slides[currentSlide];
    }
}

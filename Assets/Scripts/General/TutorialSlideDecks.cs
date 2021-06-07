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
        currentSlide++;
        if(currentSlide < slides.Length)
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
        return slides[0];
    }
}

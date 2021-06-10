using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public TutorialSlideDecks[] MasterList;
    public GameObject slideCanvas;
    public GameObject slideText;
    private int activeSlideshow;

    public void tutorialLoader(int slideshowNumber)
    {
        if(slideshowNumber < MasterList.Length)
        {
            slideText.GetComponentInChildren<Image>().sprite = MasterList[slideshowNumber].startSlides();
            activeSlideshow = slideshowNumber;
            slideCanvas.SetActive(true);
            Debug.Log("Slideshow #" + activeSlideshow + " has begun");
        }
        else
        {
            slideCanvas.SetActive(false);
            Debug.Log("Slideshow failed to load pls fix");
        }
    }

    public void advanceSlide()
    {
        Sprite nextSlide = MasterList[activeSlideshow].nextSlide();
        if (nextSlide != null)
        {
            slideText.GetComponentInChildren<Image>().sprite = nextSlide;
            Debug.Log("Slideshow #" + activeSlideshow + " Slide #" + MasterList[activeSlideshow].currentSlide);
        }
        else
        {
            slideCanvas.SetActive(false);
            Debug.Log("Slideshow has no remaining slides and has been terminated");

            if (activeSlideshow == 3)
            {
                tutorialLoader(4);
            }
            else if (activeSlideshow == 4)
            {
                tutorialLoader(5);
            }
            else if (activeSlideshow == 5)
            {
                tutorialLoader(6);
            }
            else if (activeSlideshow == 6)
            {
                tutorialLoader(7);
            }
        }
    }
    
    public void endShow()
    {
        slideCanvas.SetActive(false);
    }

    public int[] slidePos()
    {
        return new int[] { activeSlideshow, MasterList[activeSlideshow].currentSlide };
    }


    
}

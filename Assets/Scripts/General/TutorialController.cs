using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public TutorialSlideDecks[] MasterList;
    public GameObject slideCanvas;
    private int activeSlideshow;

    public void tutorialLoader(int slideshowNumber)
    {
        if(slideshowNumber < MasterList.Length)
        {
            slideCanvas.GetComponentInChildren<Image>().sprite = MasterList[slideshowNumber].startSlides();
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
        if (MasterList[activeSlideshow].nextSlide() !=null)
        {
            slideCanvas.GetComponentInChildren<Image>().sprite = MasterList[activeSlideshow].nextSlide();
            Debug.Log("Slideshow #" + activeSlideshow + " Slide #" + MasterList[activeSlideshow].currentSlide);
        }
        else
        {
            slideCanvas.SetActive(false);
            Debug.Log("Slideshow has no remaining slides and has been terminated");
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

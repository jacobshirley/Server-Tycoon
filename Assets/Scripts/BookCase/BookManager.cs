using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour {
    public Button backBtn;
    public Canvas shelf;
    public GameObject book, nextBtn, prevBtn, leftPg, rightPg;

    private int pageNum, maxPages;


    private void Start()
    {
        pageNum = 0;
        maxPages = leftPg.transform.childCount - 1;
        Debug.Log(maxPages);
        prevBtn.SetActive(false);
        leftPg.transform.GetChild(0).gameObject.SetActive(true);
        rightPg.transform.GetChild(0).gameObject.SetActive(true);
        if (maxPages > 1)
        {
            nextBtn.SetActive(true);
        }
        else
        {
            nextBtn.SetActive(false);
        }
    }

    public void ToBookshelf()
    {
        book.SetActive(false);
        shelf.gameObject.SetActive(true);
    }

    public void NextPage()
    {
        leftPg.transform.GetChild(pageNum).gameObject.SetActive(false);
        rightPg.transform.GetChild(pageNum).gameObject.SetActive(false);
        pageNum++;
        leftPg.transform.GetChild(pageNum).gameObject.SetActive(true);
        rightPg.transform.GetChild(pageNum).gameObject.SetActive(true);
        prevBtn.SetActive(true);
        if (pageNum == maxPages-1)
        {
            nextBtn.SetActive(false);
        }
    }

    public void PrevPage()
    {
        leftPg.transform.GetChild(pageNum).gameObject.SetActive(false);
        rightPg.transform.GetChild(pageNum).gameObject.SetActive(false);
        pageNum--;
        leftPg.transform.GetChild(pageNum).gameObject.SetActive(true);
        rightPg.transform.GetChild(pageNum).gameObject.SetActive(true);
        nextBtn.SetActive(true);
        if (pageNum == 0)
        {
            prevBtn.SetActive(false);
        }
    }
	
}

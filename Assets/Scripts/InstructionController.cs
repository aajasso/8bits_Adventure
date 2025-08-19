
using System.Collections;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    public TypewriterEffect instruction1;
    public TypewriterEffect instruction2;
    public TypewriterEffect instruction3;
    void OnEnable()
    {
        Time.timeScale = 1f;

        instruction1.Play(instruction1.fullText);
        StartCoroutine(Chain());
    }

    IEnumerator Chain()
    {
        yield return new WaitUntil(() => instruction1.IsFinished);
        yield return new WaitForSecondsRealtime(0.3f);
        
        instruction2.Play(instruction2.fullText);

        yield return new WaitUntil(() => instruction2.IsFinished);
        yield return new WaitForSecondsRealtime(0.3f);

        // Start the third instruction
        instruction3.Play(instruction3.fullText);

        yield return new WaitUntil(() => instruction3.IsFinished);
        yield return new WaitForSecondsRealtime(0.3f);

        // Replace text1's content with a new message
        instruction1.Play(" Lets gooooo !!! ");
        
    }
}


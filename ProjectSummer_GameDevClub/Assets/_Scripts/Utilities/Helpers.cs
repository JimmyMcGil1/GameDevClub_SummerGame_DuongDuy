using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A static class for general helpful methods
/// </summary>
public static class Helpers 
{
    /// <summary>
    /// Destroy all child objects of this transform (Unintentionally evil sounding).
    /// Use it like so:
    /// <code>
    /// transform.DestroyChildren();
    /// </code>
    /// </summary>
    public static void DestroyChildren(this Transform t) {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }
    public static IEnumerator TextApper(Text _text)
    {
        Color color = _text.color;
        while (color.a < 1f)
        {
            color.a += 0.05f;
            _text.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        color.a = 1f;
        _text.color = color;
    }
    public static IEnumerator TextDisappear(Text _text)
    {
        Color color = _text.color;
        while (color.a > 0f)
        {
            color.a -= 0.05f;
            _text.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        color.a = 0f;
        _text.color = color;
    }
}

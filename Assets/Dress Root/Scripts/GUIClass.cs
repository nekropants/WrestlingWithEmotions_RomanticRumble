using System.Collections;
using UnityEngine;

namespace Dance { 
 public class GUIClass : MonoBehaviour
{
    private static readonly string kPrecacheFontGlyphsString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_+=~`[]{}|\\:;\"'<>,.?/ ";

    public Font[] fonts;
    // utility struct used in font caching
    struct CacheFont
    {
        public Font theFont;
        public int size;
        public FontStyle style;
    };

    void Awake()
    {
        // gather custom fonts that we will be using
        CacheFont[] myCustomFonts = RetrieveMyCustomFonts();

        if (null != myCustomFonts)
        {
            for (int fontIndex = 0; fontIndex < myCustomFonts.Length; ++fontIndex)
            {
                StartCoroutine(PrecacheFontGlyphs(
                    myCustomFonts[fontIndex].theFont,
                    myCustomFonts[fontIndex].size,
                    myCustomFonts[fontIndex].style,
                    kPrecacheFontGlyphsString));
            }
        }

        return;
    }

    // Precache the font glyphs for the given font data.
    // Intended to run asynchronously inside of a coroutine.
    IEnumerator PrecacheFontGlyphs(Font theFont, int fontSize, FontStyle style, string glyphs)
    {
        for (int index = 0; (index < glyphs.Length); ++index)
        {
            theFont.RequestCharactersInTexture(
                glyphs[index].ToString(),
                fontSize, style);
            yield return null;
        }

        yield break;
    }



    CacheFont[] RetrieveMyCustomFonts()
    {
        CacheFont[] myCustomFonts = new CacheFont[fonts.Length];

        for (int i = 0; i < fonts.Length; i++)
        {
           // myCustomFonts[i] = new CacheFont();
            myCustomFonts[i].theFont = fonts[i];
            myCustomFonts[i].size = fonts[i].fontSize;
        }

        return myCustomFonts;
    }
    void OnGUI()
    {
        // now that dynamic font glyphs have been precached,
        // there won’t be a terrible hitch the first time this is called
    }
};
}
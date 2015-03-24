# Getting Started with Colours

![Colours](https://raw.githubusercontent.com/akamud/Colours/master/images/colours.png)

Colours is a port of the Colours Library for iOS made by [**Ben Gordon**](https://github.com/bennyguitar). You can find that project [**here**](https://github.com/bennyguitar/Colours). 
This is also based on the Java port by [**Matthew York**](https://github.com/MatthewYork) found [**here**](https://github.com/MatthewYork/Colours).

I just started this project as a way to have this on Xamarin Platforms, right now I'm focusing on the Xamarin.Android port. I plan to port it to Xamarin.Forms later. Since I don't own neither a Xamarin.iOS license or a Mac, I don't have any plans to port this to Xamarin.iOS. If you can contribute to that, I'll happily accept pull requests.

## Android Sample

The Android Sample contains various examples of how to use Colours in your project. You can also see the Color Palette available.

![Android Sample](https://raw.githubusercontent.com/akamud/Colours/master/images/androidsample_screenshot_compressed.png)

## Using Predefined Colors
Colours works exactly like the predefined Android colors. In fact, the Colour class is a subclass of android.graphics.Color, so you can actually use the <code>Colour</code> class where you normally use <code>Color</code> to gain access to the cool new methods of the Colour Library without losing any methods in the <code>Color</code>  class. 

###XML

To use your **HUGE** new palette of colors in XML, reference a color just as you would a color in a local Color.xml resource file:

```xml
  <View
    .
    .
    .
    android:background="@color/seafoamColor" />
```

Huzzah! Colours automagically integrates all of its colors to your project, just as if you had defined them yourself. (You can tell all your friends that you made them. We won't tell!)

###Code

Let's say, however, that you would like to set the color of something in code. Colours has you covered. Every single color available in XML is also available as a static method, much like the android system colors. To retrieve a predefined color's int representation, simply call it's corresponding method:

```C#
int seashellColor = Colour.SeashellColor();
```

## Color Spaces

Android comes pre-baked with RGB and HSV color space methods. However, this may not be enough. This library adds CMYK, which is normally used for printing, and CIE_LAB, a color space meant for modeling an equal space between each color. You can access these methods like so:

```C#
float[] cmyk = Colour.ColorToCMYK(inputColor);
int color = Colour.CMYKToColor(cmyk);
float[] cie_lab = Colour.ColorToCIELab(inputColor);
int color = Colour.CIELabToColor(cie_lab);
```

## Color Helper Methods

Beyond giving you a list of a ton of colors with no effort, this category also gives you some methods that allow different color manipulations and translations. Here's how you use these:

**Generating white or black that contrasts with a Color**

A lot of times you may want to put text on top of a view that is a certain color, and you want to be sure that it will look good on top of it. With this method you will return either white or black, depending on the how well each of them contrast on top of it. Here's how you use this:

```C#
int contrastingColor = Colour.BlackOrWhiteContrastingColor (inputColor);
```

**Generating a complementary color**

This method will create a color int that is the exact opposite color from another color int on the color wheel. The same saturation and brightness are preserved, only the hue is changed.

```C#
int complementaryColor = Colour.ComplementaryColor(inputColor);
```

## Distance between 2 Colors

Detecting a difference in two colors is not as trivial as it sounds. One's first instinct is to go for a difference in RGB values, leaving you with a sum of the differences of each point. It looks great! Until you actually start comparing colors. Why do these two reds have a different distance than these two blues *in real life* vs computationally? Human visual perception is next in the line of things between a color and your brain. Some colors are just perceived to have larger variants inside of their respective areas than others, so we need a way to model this human variable to colors. Enter CIELAB. This color formulation is supposed to be this model. So now we need to standardize a unit of distance between any two colors that works independent of how humans visually perceive that distance. Enter CIE76,94,2000. These are methods that use user-tested data and other mathematically and statistically significant correlations to output this info. You can read the wiki articles below to get a better understanding historically of how we moved to newer and better color distance formulas, and what their respective pros/cons are.

**Finding Distance**

```C#
double distance = Colour.DistanceBetweenColorsWithFormula(colorA, colorB, ColorDistanceFormulaCIE94);
bool isNoticablySimilar = distance < threshold;
```

**Resources**

* [Color Difference](http://en.wikipedia.org/wiki/Color_difference)
* [Just Noticeable Difference](http://en.wikipedia.org/wiki/Just_noticeable_difference)
* [CIELAB Specification](http://en.wikipedia.org/wiki/CIELAB)

## Generating Color Schemes ##

You can create a 5-color scheme based off of a color using the following method. It takes in a color int and one of the ColorSchemeTypes defined in Colours. It returns an int[] of 4 new colors to create a pretty nice color scheme that complements the root color you passed in.

```C#
int[] complementaryColors = Colour.ColorSchemeOfType(inputColor, ColorScheme.ColorSchemeComplementary);
```

**ColorSchemeTypes**

* ColorSchemeAnalagous
* ColorSchemeMonochromatic
* ColorSchemeTriad
* ColorSchemeComplementary

Here are the different examples starting with a color scheme based off of `Colour.seafoamColor()`.

**ColorSchemeAnalagous**

![Analagous](https://raw.github.com/bennyguitar/Colours-for-iOS/master/Screenshots/analagous.png)

**ColorSchemeMonochromatic**

![Monochromatic](https://raw.github.com/bennyguitar/Colours-for-iOS/master/Screenshots/monochromatic.png)

**ColorSchemeTriad**

![Triad](https://raw.github.com/bennyguitar/Colours-for-iOS/master/Screenshots/triad.png)

**ColorSchemeComplementary**

![Complementary](https://raw.github.com/bennyguitar/Colours-for-iOS/master/Screenshots/complementary.png)

## Credits

* [**Ben Gordon**](https://github.com/bennyguitar) - Author of the original iOS version of Colours
* [**Matthew York**](https://github.com/MatthewYork) - Author of Colours for Android 

## License
[MIT License](https://github.com/akamud/Colours/blob/master/LICENSE)
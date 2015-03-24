using Android.Graphics;
using System;

namespace Colours.Android
{
    public static class Colour
    {
        //Color Scheme Enumeration (for color scheme generation)
        public enum ColorScheme
        {
            ColorSchemeAnalagous, ColorSchemeMonochromatic, ColorSchemeTriad, ColorSchemeComplementary
        }

        public enum ColorDistanceFormula
        {
            ColorDistanceFormulaCIE76, ColorDistanceFormulaCIE94, ColorDistanceFormulaCIE2000
        }

        // ///////////////////////////////////
        // 4 Color Scheme from Color
        // ///////////////////////////////////

        /**
         * Creates an int[] of 4 Colors that complement the Color.
         *
         * @param type ColorSchemeAnalagous, ColorSchemeMonochromatic,
         *             ColorSchemeTriad, ColorSchemeComplementary
         * @return ArrayList<Integer>
         */

        public static int[] ColorSchemeOfType(Color color, ColorScheme type)
        {
            float[] hsv = new float[3];
            Color.ColorToHSV(color, hsv);

            switch (type)
            {
                case ColorScheme.ColorSchemeAnalagous:
                    return Colour.AnalagousColors(hsv);

                case ColorScheme.ColorSchemeMonochromatic:
                    return Colour.MonochromaticColors(hsv);

                case ColorScheme.ColorSchemeTriad:
                    return Colour.TriadColors(hsv);

                case ColorScheme.ColorSchemeComplementary:
                    return Colour.ComplementaryColors(hsv);

                default:
                    return null;
            }
        }

        public static int[] AnalagousColors(float[] hsv)
        {
            float[] CA1 = {Colour.AddDegrees(hsv[0], 15),
                (float) (hsv[1] - 0.05), (float) (hsv[2] - 0.05)};
            float[] CA2 = {Colour.AddDegrees(hsv[0], 30),
                (float) (hsv[1] - 0.05), (float) (hsv[2] - 0.1)};
            float[] CB1 = {Colour.AddDegrees(hsv[0], -15),
                (float) (hsv[1] - 0.05), (float) (hsv[2] - 0.05)};
            float[] CB2 = {Colour.AddDegrees(hsv[0], -30),
                (float) (hsv[1] - 0.05), (float) (hsv[2] - 0.1)};

            return new int[]{Color.HSVToColor(CA1), Color.HSVToColor(CA2),
                Color.HSVToColor(CB1), Color.HSVToColor(CB2)};
        }

        public static int[] MonochromaticColors(float[] hsv)
        {
            float[] CA1 = { hsv[0], (float)(hsv[1]), (float)(hsv[2] / 2) };
            float[] CA2 = { hsv[0], (float)(hsv[1] / 2), (float)(hsv[2] / 3) };
            float[] CB1 = { hsv[0], (float)(hsv[1] / 3), (float)(hsv[2] * 2 / 3) };
            float[] CB2 = { hsv[0], (float)(hsv[1]), (float)(hsv[2] * 4 / 5) };

            return new int[]{Color.HSVToColor(CA1), Color.HSVToColor(CA2),
                Color.HSVToColor(CB1), Color.HSVToColor(CB2)};
        }

        public static int[] TriadColors(float[] hsv)
        {
            float[] CA1 = {Colour.AddDegrees(hsv[0], 120), (float) (hsv[1]),
                (float) (hsv[2])};
            float[] CA2 = {Colour.AddDegrees(hsv[0], 120),
                (float) (hsv[1] * 7 / 6), (float) (hsv[2] - 0.05)};
            float[] CB1 = {Colour.AddDegrees(hsv[0], 240), (float) (hsv[1]),
                (float) (hsv[2])};
            float[] CB2 = {Colour.AddDegrees(hsv[0], 240),
                (float) (hsv[1] * 7 / 6), (float) (hsv[2] - 0.05)};

            return new int[]{Color.HSVToColor(CA1), Color.HSVToColor(CA2),
                Color.HSVToColor(CB1), Color.HSVToColor(CB2)};
        }

        public static int[] ComplementaryColors(float[] hsv)
        {
            float[] CA1 = { hsv[0], (float)(hsv[1] * 5 / 7), (float)(hsv[2]) };
            float[] CA2 = { hsv[0], (float)(hsv[1]), (float)(hsv[2] * 4 / 5) };
            float[] CB1 = {Colour.AddDegrees(hsv[0], 180), (float) (hsv[1]),
                (float) (hsv[2])};
            float[] CB2 = {Colour.AddDegrees(hsv[0], 180),
                (float) (hsv[1] * 5 / 7), (float) (hsv[2])};

            return new int[]{Color.HSVToColor(CA1), Color.HSVToColor(CA2),
                Color.HSVToColor(CB1), Color.HSVToColor(CB2)};
        }

        public static float AddDegrees(float addDeg, float staticDeg)
        {
            staticDeg += addDeg;
            if (staticDeg > 360)
            {
                float offset = staticDeg - 360;
                return offset;
            }
            else if (staticDeg < 0)
            {
                return -1 * staticDeg;
            }
            else
            {
                return staticDeg;
            }
        }

        /**
         * Returns Black or white, depending on which color would contrast best with the provided color.
         *
         * @param color (Color)
         * @return int
         */

        public static int BlackOrWhiteContrastingColor(int color)
        {
            int[] rgbaArray = new int[] { Color.GetRedComponent(color), Color.GetGreenComponent(color), Color.GetBlueComponent(color) };
            double a = 1 - ((0.00299 * (double)rgbaArray[0]) + (0.00587 * (double)rgbaArray[1]) + (0.00114 * (double)rgbaArray[2]));
            return a < 0.5 ? Color.Black : Color.White;
        }

        /**
         * This method will create a color instance that is the exact opposite color from another color on the color wheel. The same saturation and brightness are preserved, just the hue is changed.
         *
         * @param color (Color)
         * @return int
         */

        public static int ComplementaryColor(Color color)
        {
            float[] hsv = new float[3];
            Color.ColorToHSV(color, hsv);
            float newH = Colour.AddDegrees(180, hsv[0]);
            hsv[0] = newH;

            return Color.HSVToColor(hsv);
        }

        // CMYK

        /**
         * Color to cMYK.
         *
         * @param color the color int
         * @return float [ ]
         */

        public static float[] ColorToCMYK(int color)
        {
            float r = Color.GetRedComponent(color);
            float g = Color.GetGreenComponent(color);
            float b = Color.GetBlueComponent(color);
            float c = 1 - r / 255;
            float m = 1 - g / 255;
            float y = 1 - b / 255;
            float k = Math.Min(1, Math.Min(c, Math.Min(m, y)));
            if (k == 1)
            {
                c = 0;
                m = 0;
                y = 0;
            }
            else
            {
                c = (c - k) / (1 - k);
                m = (m - k) / (1 - k);
                y = (y - k) / (1 - k);
            }

            return new float[] { c, m, y, k };
        }

        /**
         * CMYK to color.
         *
         * @param cmyk the cmyk array
         * @return color
         */

        public static int CMYKToColor(float[] cmyk)
        {
            float c = cmyk[0] * (1 - cmyk[3]) + cmyk[3];
            float m = cmyk[1] * (1 - cmyk[3]) + cmyk[3];
            float y = cmyk[2] * (1 - cmyk[3]) + cmyk[3];
            return Color.Rgb((int)((1 - c) * 255), (int)((1 - m) * 255), (int)((1 - y) * 255));
        }

        /**
         * Color to cIE _ lAB.
         *
         * @param color the color int
         * @return double[]
         */

        public static double[] ColorToCIELab(int color)
        {
            // Convert Color to XYZ format first
            double r = Color.GetRedComponent(color) / 255.0;
            double g = Color.GetGreenComponent(color) / 255.0;
            double b = Color.GetBlueComponent(color) / 255.0;

            // Create deltaRGB
            r = (r > 0.04045) ? Math.Pow((r + 0.055) / 1.055, 2.40) : (r / 12.92);
            g = (g > 0.04045) ? Math.Pow((g + 0.055) / 1.055, 2.40) : (g / 12.92);
            b = (b > 0.04045) ? Math.Pow((b + 0.055) / 1.055, 2.40) : (b / 12.92);

            // Create XYZ
            double X = r * 41.24 + g * 35.76 + b * 18.05;
            double Y = r * 21.26 + g * 71.52 + b * 7.22;
            double Z = r * 1.93 + g * 11.92 + b * 95.05;

            // Convert XYZ to L*a*b*
            X = X / 95.047;
            Y = Y / 100.000;
            Z = Z / 108.883;
            X = (X > Math.Pow((6.0 / 29.0), 3.0)) ? Math.Pow(X, 1.0 / 3.0) : (1 / 3) * Math.Pow((29.0 / 6.0), 2.0) * X + 4 / 29.0;
            Y = (Y > Math.Pow((6.0 / 29.0), 3.0)) ? Math.Pow(Y, 1.0 / 3.0) : (1 / 3) * Math.Pow((29.0 / 6.0), 2.0) * Y + 4 / 29.0;
            Z = (Z > Math.Pow((6.0 / 29.0), 3.0)) ? Math.Pow(Z, 1.0 / 3.0) : (1 / 3) * Math.Pow((29.0 / 6.0), 2.0) * Z + 4 / 29.0;
            double CIE_L = 116 * Y - 16;
            double CIE_a = 500 * (X - Y);
            double CIE_b = 200 * (Y - Z);
            return new double[] { CIE_L, CIE_a, CIE_b };
        }

        /**
         * CIE _ lab to color.
         *
         * @param cie_lab the double[]
         * @return color
         */

        public static int CIELabToColor(double[] cieLab)
        {
            double L = cieLab[0];
            double A = cieLab[1];
            double B = cieLab[2];
            double Y = (L + 16.0) / 116.0;
            double X = A / 500 + Y;
            double Z = Y - B / 200;
            X = (Math.Pow(X, 3.0) > 0.008856) ? Math.Pow(X, 3.0) : (X - 4 / 29.0) / 7.787;
            Y = (Math.Pow(Y, 3.0) > 0.008856) ? Math.Pow(Y, 3.0) : (Y - 4 / 29.0) / 7.787;
            Z = (Math.Pow(Z, 3.0) > 0.008856) ? Math.Pow(Z, 3.0) : (Z - 4 / 29.0) / 7.787;
            X = X * .95047;
            Y = Y * 1.00000;
            Z = Z * 1.08883;

            // Convert XYZ to RGB
            double R = X * 3.2406 + Y * -1.5372 + Z * -0.4986;
            double G = X * -0.9689 + Y * 1.8758 + Z * 0.0415;
            double _B = X * 0.0557 + Y * -0.2040 + Z * 1.0570;
            R = (R > 0.0031308) ? 1.055 * (Math.Pow(R, (1 / 2.4))) - 0.055 : R * 12.92;
            G = (G > 0.0031308) ? 1.055 * (Math.Pow(G, (1 / 2.4))) - 0.055 : G * 12.92;
            _B = (_B > 0.0031308) ? 1.055 * (Math.Pow(_B, (1 / 2.4))) - 0.055 : _B * 12.92;
            return Color.Rgb((int)(R * 255), (int)(G * 255), (int)(_B * 255));
        }

        public static double DistanceBetweenColors(int colorA, int colorB)
        {
            return DistanceBetweenColorsWithFormula(colorA, colorB, ColorDistanceFormula.ColorDistanceFormulaCIE94);
        }

        public static double DistanceBetweenColorsWithFormula(int colorA, int colorB, ColorDistanceFormula formula)
        {
            double[] lab1 = Colour.ColorToCIELab(colorA);
            double[] lab2 = Colour.ColorToCIELab(colorB);
            double L1 = lab1[0];
            double A1 = lab1[1];
            double B1 = lab1[2];
            double L2 = lab2[0];
            double A2 = lab2[1];
            double B2 = lab2[2];

            // CIE76 first
            if (formula == ColorDistanceFormula.ColorDistanceFormulaCIE76)
            {
                double distance = Math.Sqrt(Math.Pow((L1 - L2), 2) + Math.Pow((A1 - A2), 2) + Math.Pow((B1 - B2), 2));
                return distance;
            }

            // More Common Variables
            double kL = 1;
            double kC = 1;
            double kH = 1;
            double k1 = 0.045;
            double k2 = 0.015;
            double deltaL = L1 - L2;
            double C1 = Math.Sqrt((A1 * A1) + (B1 * B1));
            double C2 = Math.Sqrt((A2 * A2) + (B2 * B2));
            double deltaC = C1 - C2;
            double deltaH = Math.Sqrt(Math.Pow((A1 - A2), 2.0) + Math.Pow((B1 - B2), 2.0) - Math.Pow(deltaC, 2.0));
            double sL = 1;
            double sC = 1 + k1 * (Math.Sqrt((A1 * A1) + (B1 * B1)));
            double sH = 1 + k2 * (Math.Sqrt((A1 * A1) + (B1 * B1)));

            // CIE94
            if (formula == ColorDistanceFormula.ColorDistanceFormulaCIE94)
            {
                return Math.Sqrt(Math.Pow((deltaL / (kL * sL)), 2.0) + Math.Pow((deltaC / (kC * sC)), 2.0) + Math.Pow((deltaH / (kH * sH)), 2.0));
            }

            // CIE2000
            // More variables
            double deltaLPrime = L2 - L1;
            double meanL = (L1 + L2) / 2;
            double meanC = (C1 + C2) / 2;
            double aPrime1 = A1 + A1 / 2 * (1 - Math.Sqrt(Math.Pow(meanC, 7.0) / (Math.Pow(meanC, 7.0) + Math.Pow(25.0, 7.0))));
            double aPrime2 = A2 + A2 / 2 * (1 - Math.Sqrt(Math.Pow(meanC, 7.0) / (Math.Pow(meanC, 7.0) + Math.Pow(25.0, 7.0))));
            double cPrime1 = Math.Sqrt((aPrime1 * aPrime1) + (B1 * B1));
            double cPrime2 = Math.Sqrt((aPrime2 * aPrime2) + (B2 * B2));
            double cMeanPrime = (cPrime1 + cPrime2) / 2;
            double deltaCPrime = cPrime1 - cPrime2;
            double hPrime1 = Math.Atan2(B1, aPrime1);
            double hPrime2 = Math.Atan2(B2, aPrime2);
            hPrime1 = hPrime1 % RAD(360.0);
            hPrime2 = hPrime2 % RAD(360.0);
            double deltahPrime = 0;
            if (Math.Abs(hPrime1 - hPrime2) <= RAD(180.0))
            {
                deltahPrime = hPrime2 - hPrime1;
            }
            else
            {
                deltahPrime = (hPrime2 <= hPrime1) ? hPrime2 - hPrime1 + RAD(360.0) : hPrime2 - hPrime1 - RAD(360.0);
            }
            double deltaHPrime = 2 * Math.Sqrt(cPrime1 * cPrime2) * Math.Sin(deltahPrime / 2);
            double meanHPrime = (Math.Abs(hPrime1 - hPrime2) <= RAD(180.0)) ? (hPrime1 + hPrime2) / 2 : (hPrime1 + hPrime2 + RAD(360.0)) / 2;
            double T = 1 - 0.17 * Math.Cos(meanHPrime - RAD(30.0)) + 0.24 * Math.Cos(2 * meanHPrime) + 0.32 * Math.Cos(3 * meanHPrime + RAD(6.0)) - 0.20 * Math.Cos(4 * meanHPrime - RAD(63.0));
            sL = 1 + (0.015 * Math.Pow((meanL - 50), 2)) / Math.Sqrt(20 + Math.Pow((meanL - 50), 2));
            sC = 1 + 0.045 * cMeanPrime;
            sH = 1 + 0.015 * cMeanPrime * T;
            double Rt = -2 * Math.Sqrt(Math.Pow(cMeanPrime, 7) / (Math.Pow(cMeanPrime, 7) + Math.Pow(25.0, 7))) * Math.Sin(RAD(60.0) * Math.Exp(-1 * Math.Pow((meanHPrime - RAD(275.0)) / RAD(25.0), 2)));

            // Finally return CIE2000 distance
            return Math.Sqrt(Math.Pow((deltaLPrime / (kL * sL)), 2) + Math.Pow((deltaCPrime / (kC * sC)), 2) + Math.Pow((deltaHPrime / (kH * sH)), Rt * (deltaC / (kC * sC)) * (deltaHPrime / (kH * sH))));
        }

        private static double RAD(double degree)
        {
            return degree * Math.PI / 180;
        }

        // Predefined Colors
        // System Colors
        public static int InfoBlueColor()
        {
            return Color.Rgb(47, 112, 225);
        }

        public static int SuccessColor()
        {
            return Color.Rgb(83, 215, 106);
        }

        public static int WarningColor()
        {
            return Color.Rgb(221, 170, 59);
        }

        public static int DangerColor()
        {
            return Color.Rgb(229, 0, 15);
        }

        // Whites
        public static int AntiqueWhiteColor()
        {
            return Color.Rgb(250, 235, 215);
        }

        public static int OldLaceColor()
        {
            return Color.Rgb(253, 245, 230);
        }

        public static int IvoryColor()
        {
            return Color.Rgb(255, 255, 240);
        }

        public static int SeashellColor()
        {
            return Color.Rgb(255, 245, 238);
        }

        public static int GhostWhiteColor()
        {
            return Color.Rgb(248, 248, 255);
        }

        public static int SnowColor()
        {
            return Color.Rgb(255, 250, 250);
        }

        public static int LinenColor()
        {
            return Color.Rgb(250, 240, 230);
        }

        // Grays
        public static int Black25PercentColor()
        {
            return Color.Rgb(64, 64, 64);
        }

        public static int Black50PercentColor()
        {
            return Color.Rgb(128, 128, 128);
        }

        public static int Black75PercentColor()
        {
            return Color.Rgb(192, 192, 192);
        }

        public static int WarmGrayColor()
        {
            return Color.Rgb(133, 117, 112);
        }

        public static int CoolGrayColor()
        {
            return Color.Rgb(118, 122, 133);
        }

        public static int CharcoalColor()
        {
            return Color.Rgb(34, 34, 34);
        }

        // Blues
        public static int TealColor()
        {
            return Color.Rgb(28, 160, 170);
        }

        public static int SteelBlueColor()
        {
            return Color.Rgb(103, 153, 170);
        }

        public static int RobinEggColor()
        {
            return Color.Rgb(141, 218, 247);
        }

        public static int PastelBlueColor()
        {
            return Color.Rgb(99, 161, 247);
        }

        public static int TurquoiseColor()
        {
            return Color.Rgb(112, 219, 219);
        }

        public static int SkyBlueColor()
        {
            return Color.Rgb(0, 178, 238);
        }

        public static int IndigoColor()
        {
            return Color.Rgb(13, 79, 139);
        }

        public static int DenimColor()
        {
            return Color.Rgb(67, 114, 170);
        }

        public static int BlueberryColor()
        {
            return Color.Rgb(89, 113, 173);
        }

        public static int CornflowerColor()
        {
            return Color.Rgb(100, 149, 237);
        }

        public static int BabyBlueColor()
        {
            return Color.Rgb(190, 220, 230);
        }

        public static int MidnightBlueColor()
        {
            return Color.Rgb(13, 26, 35);
        }

        public static int FadedBlueColor()
        {
            return Color.Rgb(23, 137, 155);
        }

        public static int IcebergColor()
        {
            return Color.Rgb(200, 213, 219);
        }

        public static int WaveColor()
        {
            return Color.Rgb(102, 169, 251);
        }

        // Greens
        public static int EmeraldColor()
        {
            return Color.Rgb(1, 152, 117);
        }

        public static int GrassColor()
        {
            return Color.Rgb(99, 214, 74);
        }

        public static int PastelGreenColor()
        {
            return Color.Rgb(126, 242, 124);
        }

        public static int SeafoamColor()
        {
            return Color.Rgb(77, 226, 140);
        }

        public static int PaleGreenColor()
        {
            return Color.Rgb(176, 226, 172);
        }

        public static int CactusGreenColor()
        {
            return Color.Rgb(99, 111, 87);
        }

        public static int ChartreuseColor()
        {
            return Color.Rgb(69, 139, 0);
        }

        public static int HollyGreenColor()
        {
            return Color.Rgb(32, 87, 14);
        }

        public static int OliveColor()
        {
            return Color.Rgb(91, 114, 34);
        }

        public static int OliveDrabColor()
        {
            return Color.Rgb(107, 142, 35);
        }

        public static int MoneyGreenColor()
        {
            return Color.Rgb(134, 198, 124);
        }

        public static int HoneydewColor()
        {
            return Color.Rgb(216, 255, 231);
        }

        public static int LimeColor()
        {
            return Color.Rgb(56, 237, 56);
        }

        public static int CardTableColor()
        {
            return Color.Rgb(87, 121, 107);
        }

        // Reds
        public static int SalmonColor()
        {
            return Color.Rgb(233, 87, 95);
        }

        public static int BrickRedColor()
        {
            return Color.Rgb(151, 27, 16);
        }

        public static int EasterPinkColor()
        {
            return Color.Rgb(241, 167, 162);
        }

        public static int GrapefruitColor()
        {
            return Color.Rgb(228, 31, 54);
        }

        public static int PinkColor()
        {
            return Color.Rgb(255, 95, 154);
        }

        public static int IndianRedColor()
        {
            return Color.Rgb(205, 92, 92);
        }

        public static int StrawberryColor()
        {
            return Color.Rgb(190, 38, 37);
        }

        public static int CoralColor()
        {
            return Color.Rgb(240, 128, 128);
        }

        public static int MaroonColor()
        {
            return Color.Rgb(80, 4, 28);
        }

        public static int WatermelonColor()
        {
            return Color.Rgb(242, 71, 63);
        }

        public static int TomatoColor()
        {
            return Color.Rgb(255, 99, 71);
        }

        public static int PinkLipstickColor()
        {
            return Color.Rgb(255, 105, 180);
        }

        public static int PaleRoseColor()
        {
            return Color.Rgb(255, 228, 225);
        }

        public static int CrimsonColor()
        {
            return Color.Rgb(187, 18, 36);
        }

        // Purples
        public static int EggplantColor()
        {
            return Color.Rgb(105, 5, 98);
        }

        public static int PastelPurpleColor()
        {
            return Color.Rgb(207, 100, 235);
        }

        public static int PalePurpleColor()
        {
            return Color.Rgb(229, 180, 235);
        }

        public static int CoolPurpleColor()
        {
            return Color.Rgb(140, 93, 228);
        }

        public static int VioletColor()
        {
            return Color.Rgb(191, 95, 255);
        }

        public static int PlumColor()
        {
            return Color.Rgb(139, 102, 139);
        }

        public static int LavenderColor()
        {
            return Color.Rgb(204, 153, 204);
        }

        public static int RaspberryColor()
        {
            return Color.Rgb(135, 38, 87);
        }

        public static int FuschiaColor()
        {
            return Color.Rgb(255, 20, 147);
        }

        public static int grapeColor()
        {
            return Color.Rgb(54, 11, 88);
        }

        public static int PeriwinkleColor()
        {
            return Color.Rgb(135, 159, 237);
        }

        public static int OrchidColor()
        {
            return Color.Rgb(218, 112, 214);
        }

        // Yellows
        public static int GoldenrodColor()
        {
            return Color.Rgb(215, 170, 51);
        }

        public static int YellowGreenColor()
        {
            return Color.Rgb(192, 242, 39);
        }

        public static int BananaColor()
        {
            return Color.Rgb(229, 227, 58);
        }

        public static int MustardColor()
        {
            return Color.Rgb(205, 171, 45);
        }

        public static int ButtermilkColor()
        {
            return Color.Rgb(254, 241, 181);
        }

        public static int GoldColor()
        {
            return Color.Rgb(139, 117, 18);
        }

        public static int CreamColor()
        {
            return Color.Rgb(240, 226, 187);
        }

        public static int LightCreamColor()
        {
            return Color.Rgb(240, 238, 215);
        }

        public static int WheatColor()
        {
            return Color.Rgb(240, 238, 215);
        }

        public static int BeigeColor()
        {
            return Color.Rgb(245, 245, 220);
        }

        // Oranges
        public static int PeachColor()
        {
            return Color.Rgb(242, 187, 97);
        }

        public static int BurntOrangeColor()
        {
            return Color.Rgb(184, 102, 37);
        }

        public static int PastelOrangeColor()
        {
            return Color.Rgb(248, 197, 143);
        }

        public static int CantaloupeColor()
        {
            return Color.Rgb(250, 154, 79);
        }

        public static int CarrotColor()
        {
            return Color.Rgb(237, 145, 33);
        }

        public static int MandarinColor()
        {
            return Color.Rgb(247, 145, 55);
        }

        // Browns
        public static int ChiliPowderColor()
        {
            return Color.Rgb(199, 63, 23);
        }

        public static int BurntSiennaColor()
        {
            return Color.Rgb(138, 54, 15);
        }

        public static int ChocolateColor()
        {
            return Color.Rgb(94, 38, 5);
        }

        public static int CoffeeColor()
        {
            return Color.Rgb(141, 60, 15);
        }

        public static int CinnamonColor()
        {
            return Color.Rgb(123, 63, 9);
        }

        public static int AlmondColor()
        {
            return Color.Rgb(196, 142, 72);
        }

        public static int EggshellColor()
        {
            return Color.Rgb(252, 230, 201);
        }

        public static int SandColor()
        {
            return Color.Rgb(222, 182, 151);
        }

        public static int MudColor()
        {
            return Color.Rgb(70, 45, 29);
        }

        public static int SiennaColor()
        {
            return Color.Rgb(160, 82, 45);
        }

        public static int DustColor()
        {
            return Color.Rgb(236, 214, 197);
        }

        // All Holo Colors in Android

        public static int HoloBlueLightColor()
        {
            return Color.ParseColor("#ff33b5e5");
        }

        public static int HoloGreenLightColor()
        {
            return Color.ParseColor("#ff99cc00");
        }

        public static int HoloRedLightColor()
        {
            return Color.ParseColor("#ffff4444");
        }

        public static int HoloBlueDarkColor()
        {
            return Color.ParseColor("#ff0099cc");
        }

        public static int HoloGreenDarkColor()
        {
            return Color.ParseColor("#ff669900");
        }

        public static int HoloRedDarkColor()
        {
            return Color.ParseColor("#ffcc0000");
        }

        public static int HoloPurpleColor()
        {
            return Color.ParseColor("#ffaa66cc");
        }

        public static int HoloOrangeLightColor()
        {
            return Color.ParseColor("#ffffbb33");
        }

        public static int HoloOrangeDarkColor()
        {
            return Color.ParseColor("#ffff8800");
        }

        public static int HoloBlueBrightColor()
        {
            return Color.ParseColor("#ff00ddff");
        }

        // Holo Background colors

        public static int BackgroundDarkColor()
        {
            return Color.ParseColor("#ff000000");
        }

        public static int BackgroundLightColor()
        {
            return Color.ParseColor("#ffffffff");
        }

        public static int BrightForegroundDarkColor()
        {
            return Color.ParseColor("#ffffffff");
        }

        public static int BrightForegroundLightColor()
        {
            return Color.ParseColor("#ff000000");
        }

        public static int BrightForegroundDarkDisabledColor()
        {
            return Color.ParseColor("#80ffffff");
        }

        public static int BackgroundHoloDarkColor()
        {
            return Color.ParseColor("#ff000000");
        }

        public static int BackgroundHoloLightColor()
        {
            return Color.ParseColor("#fff3f3f3");
        }

        public static int BrightForegroundHoloDarkColor()
        {
            return Color.ParseColor("#fff3f3f3");
        }

        public static int BrightForegroundHoloLightColor()
        {
            return Color.ParseColor("#ff000000");
        }

        public static int BrightForegroundDisabledHoloDarkColor()
        {
            return Color.ParseColor("#ff4c4c4c");
        }

        public static int BrightForegroundDisabledHoloLightColor()
        {
            return Color.ParseColor("#ffb2b2b2");
        }

        public static int DimForegroundHoloDarkColor()
        {
            return Color.ParseColor("#bebebe");
        }

        public static int DimForegroundDisabledHoloDarkColor()
        {
            return Color.ParseColor("#80bebebe");
        }

        public static int HintForegroundHoloDarkColor()
        {
            return Color.ParseColor("#808080");
        }

        public static int DimForegroundHoloLightColor()
        {
            return Color.ParseColor("#323232");
        }

        public static int DimForegroundDisabledHoloLightColor()
        {
            return Color.ParseColor("#80323232");
        }

        public static int HintForegroundHoloLightColor()
        {
            return Color.ParseColor("#808080");
        }

        public static int HighlightedTextHoloDarkColor()
        {
            return Color.ParseColor("#6633b5e5");
        }

        public static int HighlightedTextHoloLightColor()
        {
            return Color.ParseColor("#ff00ddff");
        }
    }
}
using System;
using OpenAiIntegrationLibrary.Models;

namespace OpenAiIntegrationLibrary.Helpers;

public static class EnumConverter
{
    public static string ImageSizeEnumToRequest(OpenAiImageSize imageSize)
    {
        switch (imageSize)
        {
            case OpenAiImageSize.TwoFiveSix:
                return "256x256";
            case OpenAiImageSize.FiveOneTwo:
                return "512x512";
            case OpenAiImageSize.OneZeroTwoFour:
                return "1024x1024";
            default:
                throw new ArgumentOutOfRangeException(nameof(imageSize), imageSize, null);
        }
    }
}
using System;
using System.Collections.Generic;

public enum AudioEncoder
{
    OPUS,
    AAC,
    MP3
}

public static class EncoderInfo
{
    private static readonly Dictionary<AudioEncoder, (string Library, string Name, string Bitrate)> encoderInfo = new Dictionary<AudioEncoder, (string Library, string Name, string Bitrate)>
    {
        { AudioEncoder.OPUS, ("libopus", "OPUS", "48K") },
        { AudioEncoder.AAC, ("libaac", "AAC", "128k") },
        { AudioEncoder.MP3, ("mp3", "MP3", "128k") },
    };

    public static string GetLibrary(AudioEncoder encoder)
    {
        return encoderInfo.ContainsKey(encoder) ? encoderInfo[encoder].Library : null;
    }

    public static string GetName(AudioEncoder encoder)
    {
        return encoderInfo.ContainsKey(encoder) ? encoderInfo[encoder].Name : null;
    }
    public static string GetBitrate(AudioEncoder encoder)
    {
        return encoderInfo.ContainsKey(encoder) ? encoderInfo[encoder].Bitrate : null;
    }
}
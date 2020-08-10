namespace Anivision.Core
{
    /// <summary>
    /// Enum for types of colorblindness
    /// </summary>
    public enum ColorblindType
    {
        Protanopia,
        Protanomaly,
        Deuteranopia,
        Deuteranomaly,
        Tritanopia,
        Tritanomaly,
        Achromatopsia,
        Achromatomaly,
        Custom
    }
    
    /// <summary>
    /// Enum for animals that the user can switch to
    /// </summary>
    public enum Animal
    {
        Human,
        Bee,
        Tarsier
    }
    
    /// <summary>
    /// Enum for vision effects
    /// </summary>
    public enum VisionEffect
    {
        Colorblindness,
        UV,
        MaterialSwap
    }
}
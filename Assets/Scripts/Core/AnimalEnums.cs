namespace Anivision.Core
{
    /// <summary>
    /// Enum for types of colorblindness
    /// </summary>
    public enum Colorblindness
    {
        None,
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
    /// Enum for vision effects that are applied to renderer's materials
    /// </summary>
    public enum MaterialVisionEffect
    {
        Colorblind,
        UV,
        MaterialSwap
    }
}
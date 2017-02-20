namespace Tablator.Infrastructure.Enumerations
{
    /// <summary>
    /// List of available settings for a guitar part
    /// </summary>
    public enum GuitarSettingsEnum
    {
        /// <summary>
        /// Guitar tuning ("standard", or details like "DADGBE")
        /// </summary>
        Tuning = 1,
        /// <summary>
        /// Position of capodastre (like "5")
        /// </summary>
        Capodastre = 2,
        /// <summary>
        /// Used chords in the tablature
        /// </summary>
        Chords = 3
    }
}
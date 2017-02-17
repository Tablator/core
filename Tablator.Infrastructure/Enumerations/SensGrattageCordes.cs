namespace Tablator.Infrastructure.Enumerations
{
    /// <summary>
    /// Sens de grattage des cordes
    /// </summary>
    /// <remarks>Utilisé pour indiquer dans quel sens gratter un accord par exemple</remarks>
    public enum SensGrattageCordes
    {
        /// <summary>
        /// En descente, des graves vers les aigus
        /// </summary>
        Down = 1,
        /// <summary>
        /// En remontée, des aigus vers les graves
        /// </summary>
        Up = 2
    }
}
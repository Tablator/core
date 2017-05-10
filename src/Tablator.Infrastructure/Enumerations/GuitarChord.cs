namespace Tablator.Infrastructure.Enumerations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Obsolete("Please use file storage chord system", false)]
    public enum GuitarChordEnum
    {
        [Display(Name = "A minor", Description = "|0|2|2|1|0|0", ShortName = "Am", GroupName = "A")]
        Am,
        [Display(Name = "A major", Description = "|0|2|2|2|0|0", ShortName = "A", GroupName = "A")]
        A,
        [Display(Name = "C major", Description = "|3|2|1|0|0|0", ShortName = "C", GroupName = "C")]
        C,
        [Display(Name = "G major", Description = "3|2|0|0|3|3|0", ShortName = "G", GroupName = "G")]
        G,
        [Display(Name = "F major", Description = "1|3|3|2|1|1|1", ShortName = "F", GroupName = "F")]
        F
    }

    [Obsolete("Please use file storage chord system", false)]
    public enum GuitarPowerChordEnum
    {
        [Display(Name = "B major", Description = "|2|4|4|||0", ShortName = "B", GroupName = "B")]
        B,
        [Display(Name = "D5", Description = "|5|7|7|||0", ShortName = "D5", GroupName = "D")]
        D5,
        [Display(Name = "C5", Description = "|3|5|5|||0", ShortName = "C5", GroupName = "C")]
        C5
    }
}
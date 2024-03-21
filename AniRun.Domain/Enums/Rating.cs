using System.ComponentModel.DataAnnotations;

namespace AniRun.Domain.Enums;

public enum Rating
{
    [Display(Name = "G")]
    G,
    [Display(Name = "PG")]
    PG,
    [Display(Name = "PG-13")]
    PG13,
    [Display(Name = "R-17")]
    R17,
    [Display(Name = "R+")]
    RPlus
}
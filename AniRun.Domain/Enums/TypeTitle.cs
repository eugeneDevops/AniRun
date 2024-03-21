using System.ComponentModel.DataAnnotations;

namespace AniRun.Domain.Enums;

public enum TypeTitle
{
    [Display(Name = "Фильм")]
    Film,
    [Display(Name = "TV Сериал")]
    Serial
}
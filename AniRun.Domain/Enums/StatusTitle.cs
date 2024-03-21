using System.ComponentModel.DataAnnotations;

namespace AniRun.Domain.Enums;

public enum StatusTitle
{
    [Display(Name = "Онгоинг")]
    Going,
    [Display(Name = "Вышел")]
    Released,
    [Display(Name = "Анонс")]
    Announcement
}
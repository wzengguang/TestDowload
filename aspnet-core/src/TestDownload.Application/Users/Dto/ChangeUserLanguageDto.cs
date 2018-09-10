using System.ComponentModel.DataAnnotations;

namespace TestDownload.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
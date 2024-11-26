using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CadastralManagement.Models
{
    public class CadastralObject
    {
        [Required]
        public required int Id { get; set; }

        [DisplayName("Кадастровый номер")]
        [Required(ErrorMessage = "Поле \"{0}\" обязательно")]
        [RegularExpression("^\\d{2}:\\d{2}:\\d{6,7}(:\\d{2,})?$", ErrorMessage = "Введите {0} в формате АА:ВВ:CCCCСCC:КК")]
        [StringLength(20, ErrorMessage = "Максимальная длина равна {1}")]
        public required string Number { get; set; }

        [DisplayName("Адрес")]
        [Required(ErrorMessage = "Поле \"{0}\" обязательно")]
        [StringLength(100, ErrorMessage = "Максимальная длина равна {1}")]
        public required string Address { get; set; }

        [DisplayName("Площадь, кв. м.")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Range(0, (double)Decimal.MaxValue, ErrorMessage = "Площадь не может быть меньше нуля")]

        public decimal Area { get; set; }

        [DisplayName("Краткое описание")]
        [StringLength(100, ErrorMessage = "Максимальная длина равна {1}")]
        public string? ShortDescription { get; set; }

        [DisplayName("ФИО владельца")]
        [StringLength(100, ErrorMessage = "Максимальная длина равна {1}")]
        public string? OwnerName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAPI.Service.Dto
{
    public class CityEventDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Título é obrigatório.")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Descrição é obrigatório.")]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Data é obrigatório.")]
        public DateTime DateHourEvent { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Local é obrigatório.")]
        [MaxLength(100)]
        public string Local { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Endereço é obrigatório.")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Preço é obrigatório.")]
        [MaxLength(10)]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Local é obrigatório.")]
        [MaxLength(100)]
        public bool Status { get; set; }
    }
}

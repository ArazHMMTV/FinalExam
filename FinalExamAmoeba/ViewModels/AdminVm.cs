using System.ComponentModel.DataAnnotations;

namespace FinalExamAmoeba.ViewModels
{
    public class AdminVm
    {
        [DataType(DataType.Password)]
        public int Paswword { get; set; }
        [Required]
        public string UserName { get; set; }

        public bool IsAdmin { get; set; }
    }
}

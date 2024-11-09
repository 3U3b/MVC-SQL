using System.ComponentModel.DataAnnotations;

namespace MVCwithSQLITE.Models
{
    public class Leader
    {
        //如果一個類別的屬性名為 Id 或 [ClassName]Id（例如 ProductId 或 OrderId），EF Core 會自動認定這個屬性為主鍵
        //[Key]  // 明確指定 Id 為主鍵(非必要
        [Display(Name = "班代學號")]
        [Required(ErrorMessage = "LeaderId is required.")]
        public string LeaderId { get; set; }
        [Display(Name = "班代姓名")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string? Email { get; set; } //string? => can be null in defult
        public string? Phone { get; set; }
    }
}

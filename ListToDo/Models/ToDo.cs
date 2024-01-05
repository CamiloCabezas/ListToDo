using System.ComponentModel.DataAnnotations;

namespace ListToDo.Models
{
    public class ToDoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="The field {0} is mandatory")]
        [MaxLength(100)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int id_usuario { get; set; }
        public bool Done {  get; set; }
    }
}

namespace api.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
        public DateTime Data { get; set; }
        public int UsuarioId { get; set; }
    }
}
namespace backend.Models;
public class Contacto
{
    public int IDContacto { get; set; }
    public int? IDUsuarioC { get; set; }
    public virtual Usuario? UsuarioC { get; set; }

    public int? IDUsuarioP { get; set; }
    public virtual Usuario? UsuarioP{ get; set; }



}

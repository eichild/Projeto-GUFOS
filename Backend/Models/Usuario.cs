using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("USUARIO")]
    public partial class Usuario
    {
        public Usuario()
        {
            Presenca = new HashSet<Presenca>();
        }

        [Key]
        [Column("USUARIO_ID")]
        public int UsuarioId { get; set; }
        [Required]
        [Column("NOME")]
        [StringLength(255)]
        public string Nome { get; set; }
        [Required]
        [Column("EMAIL")]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [Column("SENHA")]
        [StringLength(100)]
        public string Senha { get; set; }
        [Column("TIPO_USUARIO_ID")]
        public int? TipoUsuarioId { get; set; }

        [ForeignKey(nameof(TipoUsuarioId))]
        [InverseProperty("Usuario")]
        public virtual TipoUsuario TipoUsuario { get; set; }
        [InverseProperty("Usuario")]
        public virtual ICollection<Presenca> Presenca { get; set; }
    }
}

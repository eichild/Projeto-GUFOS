using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("LOCALIZACAO")]
    public partial class Localizacao
    {
        public Localizacao()
        {
            Evento = new HashSet<Evento>();
        }

        [Key]
        [Column("LOCALIZACAO_ID")]
        public int LocalizacaoId { get; set; }
        [Required]
        [Column("CNPJ")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Required]
        [Column("RAZAO_SOCIAL")]
        [StringLength(70)]
        public string RazaoSocial { get; set; }
        [Column("ENDERECO")]
        [StringLength(100)]
        public string Endereco { get; set; }

        [InverseProperty("Localizacao")]
        public virtual ICollection<Evento> Evento { get; set; }
    }
}

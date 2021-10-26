using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SisProdutos.Data.Dtos
{
    public class CadastrarUsuarioDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        
        
        [Required]
        public string ClientName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Cep { get; set; }


        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}

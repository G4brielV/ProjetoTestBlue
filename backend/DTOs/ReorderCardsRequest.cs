using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoTestBlue.DTOs
{
    public class ReorderCardsRequest
    {
        public int ListId { get; set; } 
        public List<CardOrderDto> NewOrder { get; set; }
    }
}
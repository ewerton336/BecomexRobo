namespace ApiBecomexRobo.Model
{
    public class Pulso
    {
        /*
        /// 1 - Rotação para -90º
        /// 2 - Rotação para -45º
        /// 3 - Em Repouso
        /// 4 - Rotação para 45º
        /// 5 - Rotação para 90º
        /// 6 - Rotação para 135º
        /// 7 - Rotação para 180º
        */
        public int StatusPulso { get; set; }
      
        public string DescricaoStatusPulso
        {
            get
            {
                return StatusPulso switch
                {
                    1 => "Rotação para -90º",
                    2 => "Rotação para -45º",
                    3 => "Em Repouso",
                    4 => "Rotação para 45º",
                    5 => "Rotação para 90º",
                    6 => "Rotação para 135º",
                    7 => "Rotação para 180º",
                    _ => "Indefinido",
                };
            }

            private set
            {
                DescricaoStatusPulso = value;
            }
        }
        public Pulso()
        {
            StatusPulso = 3;
        }
      
    }
}

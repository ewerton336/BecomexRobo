namespace ApiBecomexRobo.Model
{
    public class Rotacao
    {
        /// <summary>
        /// Status da Rotação (escolher um dos 5 parâmetros)
        /// <param>
        /// 1 - Rotação -90º
        /// 2 - Rotação -45º
        /// 3 - Em Repouso
        /// 4 - Rotação 45º
        /// 5 - Rotação 90º
        /// </param>
        /// </summary>
        public int StatusRotacao { get; set; }
       

        public string DescricaoRotacao
        {
            get

            {
                return StatusRotacao switch
                {
                    1 => "Rotação -90º",
                    2 => "Rotação -45º",
                    3 => "Em Repouso",
                    4 => "Rotação 45º",
                    5 => "Rotação 90º",
                    _ => "Indefinido",
                };
            }

            private set
            {
                DescricaoRotacao = value;
            }
        }

     public Rotacao()
        {
            StatusRotacao = 3;
        }

    }
}

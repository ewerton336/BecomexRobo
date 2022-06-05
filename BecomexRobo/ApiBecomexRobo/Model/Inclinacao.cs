namespace ApiBecomexRobo.Model
{
    public class Inclinacao

    {
        /// <summary>
        /// Status da inclinação (escolher um dos 3 parâmetros)
        /// <param>
        /// 1 - Para Cima
        /// 2 - Em Repouso
        /// 3 - Para baixo
        /// </param>
        /// </summary>
        ///
        public int StatusInclinacao { get; set; }

        public string DescricaoInclinacao
        {
            get
            {
                return StatusInclinacao switch
                {
                    1 => "Para Cima",
                    2 => "Em Repouso",
                    3 => "Para baixo",
                    _ => "Indefinido"
                };
            }

            private set
            {
                DescricaoInclinacao = value;
            }
        }

        public Inclinacao()
        {
             StatusInclinacao = 2;
        }
    }
}

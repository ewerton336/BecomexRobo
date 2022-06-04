namespace ApiBecomexRobo.Model
{
    public class Cotovelo
    {
        /// <summary>
        /// Status do Cotovelo (escolher um dos 4 parâmetros)
        /// <param>
        /// 1 - Em Repouso
        /// 2 - Levemente Contraído
        /// 3 - Contraído
        /// 4 - Fortemente Contraído
        /// </param>
        /// </summary>
        public int StatusCotovelo { get; set; }

        public string DescricaoStatusCotovelo
        {
            get

            {
                return StatusCotovelo switch
                {
                    1 => "Em Repouso",
                    2 => "Levemente Contraído",
                    3 => "Contraído",
                    4 => "Fortemente Contraído",
                    _ => "Indefinido",
                };
            }
            private set
            {
                DescricaoStatusCotovelo = value;
            }
        }

        public Cotovelo()
        {
            StatusCotovelo = 1;
        }
    }
}

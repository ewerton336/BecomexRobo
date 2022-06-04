using ApiBecomexRobo.Model;
namespace ApiBecomexRobo.BLL
{
    public class ValicadaoAcoes
    {
        public Robo FabricarRobo()
        {
            return new Robo()
            {

            };
        }

        public bool ValidarMovimentacao(int movimentacaoAtual, int movimentacaoNova)
        {
            if (movimentacaoAtual - movimentacaoNova == 1 || movimentacaoAtual - movimentacaoNova == -1)
            {
                return true;
            }
            else return false;

        }

        //verificar se o cotovelo está fortemente contraído para mover o pulso
        //Status fortemente contraído = 4
        public bool ValidarContracaoCotovelo(int statusCotovelo)
        {
            if (statusCotovelo == 4) return true;
            else return false;
        }

        //verificar se a inclinação da cabeça está para baixo
        //inclinação para baixo = 3
        public bool ValidarInclinacaoParaBaixo (int statusInclinacao)
        {
            if (statusInclinacao == 3)
                return true;
            else return false;
        }


    }
}

using ApiBecomexRobo.Model;
namespace ApiBecomexRobo.BLL
{
    public class ValicadaoAcoes
    {
        public bool ValidarMovimentacao(int movimentacaoAtual, int movimentacaoNova)
        {
            if (movimentacaoAtual - movimentacaoNova == 1 || movimentacaoAtual - movimentacaoNova == -1)
            {
                return true;
            }
            else return false;

        }
    }
}

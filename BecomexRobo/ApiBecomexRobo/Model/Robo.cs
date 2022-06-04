namespace ApiBecomexRobo.Model
{
    public class Robo
    {
        public Cabeca Cabeca { get; set; } = new Cabeca();
        public Braco BracoEsquerdo { get; set; } = new Braco();
        public Braco BracoDireito { get; set; } = new Braco();
    }
}

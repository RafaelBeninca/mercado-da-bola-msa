namespace TeamService.Servicos
{
    public class ServClube
    {
        private readonly DataContext _dataContext;

        private ServClube(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void InserirClube(Clube novoClube)
        {

        }

        public void ValidarClube(Clube clube)
        {
            if (string.IsNullOrWhiteSpace(clube.Nome))
            {
                throw new Exception("Falta informar o nome do clube");
            }

            if (string.IsNullOrWhiteSpace(clube.Nacionalidade))
            {
                throw new Exception("Falta informar a nacionalidade do clube");
            }

            if (clube.DataFundacao == default || clube.DataFundacao > DateTime.Today)
            {
                throw new Exception("Data de nascimento inválida.");
            }
        }
    }
}

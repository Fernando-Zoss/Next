using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarConteudo();
                        break;
                    case "2":
                        InserirConteudo();
                        break;
                    case "3":
                        AtualizarConteudo();
                        break;
                    case "4":
                        StatusConteudo();
                        break;
                    case "5":
                        VisualizarConteudo();
                        break;
                    case "6":
                        Sobre();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.Clear();
            Console.WriteLine(" -------------------------------------------------------------------");
            Console.WriteLine("| >> Obrigado por utilizar o serviço NEXT >> ");
            Console.WriteLine("| Esperamos que tenha gostado da Experiência ;) Até Breve !!!");
            Console.WriteLine(" -------------------------------------------------------------------");
            Console.Write("| >> ");
            Console.ReadLine();
        }

        private static void StatusConteudo()
        {
            Console.Clear();
            Console.WriteLine("| Marcar como assistido: ");
            Console.WriteLine("|");
            Console.WriteLine("|-------------------------------------------------------------------");

            Console.Write("| Digite o id da mídia: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Status(indiceSerie);
            Console.Clear();
            Console.WriteLine(" ------------------------------------------------------------------ ");
            Console.WriteLine("|                   Status mídia alterado !!!");

        }

        private static void VisualizarConteudo()
        {
            Console.Clear();
            Console.WriteLine("| >> Visualizar por ID: ");
            Console.WriteLine("| ");
            Console.WriteLine("|-------------------------------------------------------------------");
            Console.Write("| Digite o id da mídia: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("| >> Visualizar por ID: ");
            Console.WriteLine("| ");
            Console.WriteLine("|-------------------------------------------------------------------");
            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);

        }

        private static void AtualizarConteudo()
        {
            Console.Clear();
            Console.WriteLine("| >> Atualizar mídia: ");
            Console.WriteLine("| ");
            Console.WriteLine("| Necessário o ID da mídia, verifique na opção 1 do menu principal");
            Console.WriteLine(" ------------------------------------------------------------------ ");
            Console.Write("| Digite o ID da mídia que deseja alterar: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            Console.WriteLine("| ");
            Console.WriteLine("| Escolha uma categoria: ");

            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
            // https://docs.microsoft.com/pt-br/dotne3t/api/system.enum.getname?view=netcore-3.1
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("| {0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.WriteLine("|");
            Console.Write("| >> ");
            int entradaGenero = int.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("| >> Atualizar mídia: ");
            Console.WriteLine("| ");
            Console.WriteLine(" ------------------------------------------------------------------ ");

            Console.Write("| Título: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("| Ano: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("| Descrição: ");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Atualiza(indiceSerie, atualizaSerie);

            Console.Clear();
            Console.WriteLine(" ------------------------------------------------------------------ ");
            Console.WriteLine("|                   Mídia atualizada com sucesso !!!");

        }

        private static void ListarConteudo()
        {
            Console.Clear();
            Console.WriteLine("|>> Lista de mídias: ");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("| ");
                Console.WriteLine("| Sua lista de mídias vazia, adicione algo antes.");
                Console.WriteLine("| ");

                return;
            }

            Console.WriteLine("|");
            Console.WriteLine(" ------------------------------------------------------------------ ");

            foreach (var serie in lista)
            {
                var alterandoStatus = serie.retornaStatus();

                Console.WriteLine("| ID {0} : {1} {2}", serie.retornaId(), serie.retornaTitulo(), (alterandoStatus ? " << Assistido >> " : " << Pendente >> "));
            }
        }

        private static void InserirConteudo()
        {
            Console.Clear();
            Console.WriteLine("| >> Inserir mídia:");
            Console.WriteLine("|-------------------------------------------------------------------");
            Console.WriteLine("| Indique a categoria da mídia conforme as opções abaixo:");
            Console.WriteLine("|");


            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
            // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"| {i} >> {Enum.GetName(typeof(Genero), i)}");
            }

            Console.WriteLine("|-------------------------------------------------------------------");
            Console.Write("| >> ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Clear();
            Console.WriteLine("| >> Inserir mídia:");
            Console.WriteLine("|-------------------------------------------------------------------");
            Console.WriteLine("| ");

            Console.Write("| Título da mídia: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("| Ano da mídia: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("| Descrição da mídia: ");
            string entradaDescricao = Console.ReadLine();

            Serie novoConteudo = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novoConteudo);

            Console.Clear();
            Console.WriteLine(" ------------------------------------------------------------------ ");
            Console.WriteLine("|                   Mídia incluída com sucesso !!!                ");

        }

        private static void Sobre()
        {
            Console.Clear();
            Console.WriteLine(" ---------------------------->> NEXT >>--------------------------- ");
            Console.WriteLine("|                                                                  |");
            Console.WriteLine("| A NEXT foi desenvolvida para que você organize suas listas de    |");
            Console.WriteLine("| filmes, séries e animes que está assistindo, uma ferramenta de   |");
            Console.WriteLine("| fácil uso para que você não se perca ou esqueça quais são os     |");
            Console.WriteLine("| títulos que deseja acompahar.                                    |");
            Console.WriteLine(" ----------------------------------------------------------------- ");

        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine(" ------------------------------------------------------------------ ");
            Console.WriteLine("|                             >> NEXT >>");
            Console.WriteLine("|");
            Console.WriteLine("| Informe a opção desejada:");
            Console.WriteLine("| 1 >> Listar todas mídias;");
            Console.WriteLine("| 2 >> Inserir;");
            Console.WriteLine("| 3 >> Atualizar;");
            Console.WriteLine("| 4 >> Marcar como assistido;");
            Console.WriteLine("| 5 >> Visualizar mídia pelo ID;");
            Console.WriteLine("| 6 >> Sobre o NEXT :)");
            Console.WriteLine("| X >> Sair;");
            Console.WriteLine(" -------------------------------------------------------------------");

            Console.Write("| >> ");
            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}

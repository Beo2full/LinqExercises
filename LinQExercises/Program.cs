using System;
using System.Linq;

namespace LinQExercises
{
    class Program
    {
        static void Main(string[] args)
        {
            simpleSelect();
            innerJoinSelect();
            groupDataByBusyProgrammers();
            groupDataByStylishProgrammers();
        }

        private static void innerJoinSelect()
        {
            // Multiselect == INNER JOIN

            //Sentencia LINQ más simple que hace lo equivalente a un INNER JOIN en SQL
            var listaPedidosClientes = from c in DataLists.ListaClientes    // Lista de clientes
                                       from p in DataLists.ListaPedidos     // Lista de pedidos
                                       where p.IdCliente == c.Id            // Filtro: ID del pedido == ID del cliente
                                       select new { NumPedido = p.Id, FechaPedido = p.FechaPedido, NombreCliente = c.Nombre };
            // Sentencia LINQ más parecida al INNER JOIN en SQL
            var listaPedidosClientesAlt = from c in DataLists.ListaClientes    // Lista de clientes
                                          join p in DataLists.ListaPedidos on c.Id equals p.IdCliente // Equivale al INNER JOIN TABLA2 ON TABLA1.ID_PK = TABLA2.ID_FK
                                          select new { NumPedido = p.Id, FechaPedido = p.FechaPedido, NombreCliente = c.Nombre };

            // Recorremos la consulta y mostramos el resultado
            foreach (var pedidoCliente in listaPedidosClientes)
            {
                Console.WriteLine(String.Format("El pedido {0} enviado en {1} pertenece a {2}",
                    pedidoCliente.NumPedido, pedidoCliente.FechaPedido, pedidoCliente.NombreCliente));
            }

            System.Console.ReadKey();
        }

        private static void groupDataByBusyProgrammers()
        {
            // Group BY

            var agrupacion = from p in DataLists.ListaPedidos
                             group p by p.IdCliente into grupo
                             select grupo;

            foreach (var grupo in agrupacion)
            {
                var cliente = from p in DataLists.ListaClientes where p.Id == grupo.Key select p.Nombre;
                Console.WriteLine("Cliente: {0}", cliente.First());
                
                foreach (var objetoAgrupado in grupo)
                    Console.Write("\t\tPedido nº " + objetoAgrupado.Id + ": " + objetoAgrupado.FechaPedido + "]" + Environment.NewLine);
            }
            System.Console.ReadKey();
        }

        private static void groupDataByStylishProgrammers()
        {
            var agrupacion_bonita = from p in DataLists.ListaPedidos
                                    join c in DataLists.ListaClientes on p.IdCliente equals c.Id
                                    group p by new { p.IdCliente, c.Nombre } into grupo select grupo;

            foreach (var grupo in agrupacion_bonita)
            {
                Console.WriteLine("Nombre Cliente: {0} (ID: {1})", grupo.Key.Nombre, grupo.Key.IdCliente);

                foreach (var objetoAgrupado in grupo)
                    Console.Write("\t\tPedido nº " + objetoAgrupado.Id + ": " + objetoAgrupado.FechaPedido + "]" + Environment.NewLine);
            }
            System.Console.ReadKey();
            //            select c.nombre, p.idcliente, count(p.id)
            //from pedidos p
            //inner
            //join clientes c on c.id = p.idcliente
            //group by p.idcliente, c.nombre
        }

        private static void simpleSelect()
        {
            var listaClientes = from c in DataLists.ListaClientes
                                select c;

            foreach (var cliente in listaClientes)
            {
                System.Console.WriteLine(cliente.Nombre);
            }
            System.Console.ReadKey();
        }
    }
}

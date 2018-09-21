using System;
using System.Collections.Generic;
using System.Linq;

namespace LinQExercises
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<IEnumerable<Producto>> writeProducts = (lista_elementos) =>
            {
                foreach (Producto producto in lista_elementos)
                {
                    Console.WriteLine(String.Format("Producto {0}: {1} ({2} EUR)",
                            producto.Id, producto.Descripcion, producto.Precio));
                }
            };
            Action<string, char> separateByIssue = (text, separador) =>
            {
                string border = new string(separador, 10);
                System.Console.WriteLine(border + " " + text + " " + border);
                System.Console.WriteLine("");
            };
            separateByIssue("Ejercicios W3Resource", '*');
            var excerciesW3 = new ExcerciesW3(separateByIssue);
            excerciesW3.thirty();
            excerciesW3.tewntyNine();
            excerciesW3.eleven();
            //Ejercicios de Web Daniel Garcia

            separateByIssue("Tienda con LinQ",'*');
            separateByIssue("Lista de Clientes",'-');
            simpleSelect();
            countDistinct();
            separateByIssue("Pedidos según Clientes",'-');
            innerJoinSelect();
            separateByIssue("Precio de los Pedidos según Clientes", '-');

            separateByIssue("Pedidos Agrupados según Clientes",'-');
            groupDataByBusyProgrammers();
            separateByIssue("Pedidos Agrupados Elegantemente según clientes",'-');
            groupDataByStylishProgrammers();
            separateByIssue("Pedidos según Posición",'-');
            skipAndTakeResults(writeProducts);
            separateByIssue("Pedidos según condiciones", '-');
            conditionalSkipAndTakeResults(writeProducts);
        }

        private static void conditionalSkipAndTakeResults(Action<IEnumerable<Producto>>  writeProducts)
        {
            Console.WriteLine("------ Lista de producto desde el primer producto de más de 3 euros-------");
            var consultaMenosTresEurops = DataLists.ListaProductos.SkipWhile(producto => producto.Precio < 3.00f);
            writeProducts(consultaMenosTresEurops);
            Console.ReadKey();
        }

        private static void skipAndTakeResults(Action<IEnumerable<Producto>> writeProducts)
        {

            System.Console.WriteLine("----- Cinco primeros productos --------");
            var cinco_productos = DataLists.ListaProductos.Take(5);
            writeProducts(cinco_productos);
            System.Console.WriteLine("----- Productos a partir del octavo --------");
            var productos_desde_octavo = DataLists.ListaProductos.Skip(8);
            writeProducts(productos_desde_octavo);
            Console.ReadKey();
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
                    Console.Write("\t\tPedido nº " + objetoAgrupado.Id + ": " + objetoAgrupado.FechaPedido + Environment.NewLine);
                    
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
                    Console.Write("\t\tPedido nº " + objetoAgrupado.Id + ": " + objetoAgrupado.FechaPedido + Environment.NewLine);
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

        private static void countDistinct()
        {
            var countPedidosByClientes = from pedido in DataLists.ListaPedidos select pedido.IdCliente;
            Console.WriteLine(string.Format("Existe un total de {0} clientes distintos que han realizado pedidos.",
                countPedidosByClientes.Distinct().Count()));
            Console.ReadKey();
        }
    }
}
